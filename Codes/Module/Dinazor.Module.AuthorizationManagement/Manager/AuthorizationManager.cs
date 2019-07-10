
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Generator;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Interfaces.Token;
using Dinazor.Core.Model;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Dinazor.Core.Common.Constants;
using Dinazor.Core.Dto.Crm;
using Dinazor.Core.Identity;
using Dinazor.Core.Interfaces.User;
using Dinazor.Core.IoC;
using Dinazor.Core.Observer;
using Dinazor.Core.RestClient;
using log4net;

namespace Dinazor.Module.AuthorizationManagement.Manager
{
    public class AuthorizationManager : ITokenManager
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IAuthorizationOperation _authorizationOperation;
        private readonly ITokenStore _tokenStorer;

        public AuthorizationManager(IAuthorizationOperation authorizationOperation, ITokenStore tokenStore)
        {
            _authorizationOperation = authorizationOperation;
            _tokenStorer = tokenStore;
        }

        public async Task<DinazorResult<TokenUser>> Login(BaseTokenUser user)
        {
            if (user == null)
            {
                var res = new DinazorResult<TokenUser>();
                res.Status = ResultStatus.NoSuchObject;
                return res;
            }

            //firstly check the user whether alredy logged in or not
            var alreadyLoggedInResult = IsUserAlreadyLoggedIn(user.Username,user.Password);
            if (!alreadyLoggedInResult.IsSuccess)
            {
                return new DinazorResult<TokenUser>();
            }
            if (alreadyLoggedInResult.Data)
            {
                _log.Info("user is already logged in");
                //user already logged in. get the user
                return GetUserByUsername(user.Username);
            }

            // LICENCE CONTROL
            /*   bool anyCommunicationProblem = false;
               DinazorRestClient dinazorRestClient = new DinazorRestClient();
               DinazorResult<List<OrganizationLicenceDto>> licenceResult = new DinazorResult<List<OrganizationLicenceDto>>();
               try
               {
                   licenceResult = await dinazorRestClient.Post(user.Client);
               }
               catch (Exception e)
               {
                   _log.Error("licence manager communication problem",e);
                   anyCommunicationProblem = true;
                   licenceResult.Success();
               }

               if (!licenceResult.IsSuccess)
               {
                   var licenceErrorResult = new DinazorResult<TokenUser> { Status = licenceResult.Status };
                   return licenceErrorResult;
               }

               //validate the licence
               if (!anyCommunicationProblem && licenceResult.Data.Count <= 0)
               {
                   var noLicenceResult = new DinazorResult<TokenUser> { Status = ResultStatus.NoLicence };
                   return noLicenceResult;
               }*/
            if (true)
            {
                // try to login
                var result = await _authorizationOperation.Login(user);
                if (result.IsSuccess)
                {
                    //generate the token
                    var token = TokenGenerator.GenerateUniqueId();
                    //control the token if it already exists
                    while (_tokenStorer.IsTokenExists(token).Status == ResultStatus.Success)
                    {
                        token = TokenGenerator.GenerateUniqueId();
                    }
                    var tokenUser = result.Data;
                    tokenUser.Token = token;

                    //set the licence information
                    tokenUser.Client = user.Client;
                    tokenUser.IsLicenceValidated = true;
                    // tokenUser.OrganizationLicence = licenceResult.Data;

                    // get the roles according to licenced modules
                    var userManager = IocManager.Instance.Resolve<IUserManager>();
                    var roleListResult = await userManager.GetUserByIdWithRoles(tokenUser.Id);
                    if (roleListResult.IsSuccess)
                    {
                        tokenUser.RoleList = roleListResult.Data;
                    }
                    else
                    {
                        _log.Error("error while getting the role list");
                    }
                    //store the user
                    _tokenStorer.StoreTheToken(token, tokenUser);

                    DinazorPrincipal.AuthenticateUser(token);

                    LoginSubscriber.Broadcast(tokenUser);

                    result.Data = tokenUser;
                    return result;
                }
                return result;
            }
            /*       else if (anyCommunicationProblem)
                   {

                   }*/
            return null;
        }

        public DinazorResult LogOut(string token)
        {
            return _tokenStorer.DeleteTheToken(token);
        }

        public DinazorResult<TokenUser> GetUserByToken(string token)
        {
            var result = new DinazorResult<TokenUser>();
            if (!string.IsNullOrEmpty(token))
            {
                if (token == DinazorConstant.DinazorInternalCommunicationToken)
                {
                    result.Data = new TokenUser();
                    result.Data.Token = "dinazor";
                    result.Data.Id = 1;
                    result.Data.Username = "dino";
                    result.Data.Password = "dinazorpass";
                    result.Data.IsAuthenticated = true;
                    result.Success();
                    return result;
                }
                result = _tokenStorer.GetUserByToken(token);
            }
            return result;
        }

        public DinazorResult IsAuthorized(string token, long role)
        {
            var result = new DinazorResult();
            if (token == DinazorConstant.DinazorInternalCommunicationToken)
            {
                result.Success();
                return result;
            }

            var userResult = GetUserByToken(token);
            if (userResult.IsSuccess)
            {
                if (userResult.Data != null)
                {
                    var roleList = userResult.Data.RoleList;
                    foreach (var item in roleList)
                    {
                        if (item == role)
                        {
                            result.Success();
                            return result;
                        }
                    }
                    result.Status = ResultStatus.Unauthorized;
                }
                else
                {
                    result.Status = ResultStatus.SessionNotValid;
                }
            }
            else
            {
                result.Status = userResult.Status;
                return result;
            }
            return result;
        }

        private DinazorResult<bool> IsUserAlreadyLoggedIn(string username,string password)
        {
            return _tokenStorer.IsUserAlreadyLoggedIn(username,password);
        }

        private DinazorResult<TokenUser> GetUserByUsername(string username)
        {
            return _tokenStorer.GetUserByUsername(username);
        }
    }
}
