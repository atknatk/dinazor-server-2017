using System;
using Dinazor.Core.Interfaces.Token;
using Dinazor.Core.IoC;
using Dinazor.Core.Model;

namespace Dinazor.Core.Identity
{

    [Serializable]
    public sealed class DinazorIdentity : System.Security.Principal.IIdentity
    {

        private static DinazorIdentity _dinazorIdentity;
        public string AuthenticationType => "Dinazor";
        public bool IsAuthenticated { get; private set; }

        public string Name { get; private set; }
        public string Token { get; private set; } = string.Empty;
        public string ClientId { get; private set; } = string.Empty;
        public TokenUser TokenUser { get; private set; }


        internal static DinazorIdentity UnauthenticatedIdentity()
        {
            return new DinazorIdentity();
        }

        internal static DinazorIdentity GetIdentity(string userName, string password)
        {
            return AuthenticateandAuthorizeMe(userName, password);
        }

        internal static DinazorIdentity GetIdentity(string token)
        {
            return AuthenticateMe(token);
        }

        private DinazorIdentity()
        {
            
        }

        private static DinazorIdentity AuthenticateandAuthorizeMe(string userName, string password)
        {
            _dinazorIdentity = new DinazorIdentity();
            try
            {
                var tokenManager = IocManager.Instance.Resolve<ITokenManager>();
                var tokenData = tokenManager.Login(new BaseTokenUser()
                {
                    Username = userName,
                    Password = password
                });
                if (tokenData.Result.IsSuccess)
                {
                    return AuthenticateMe(tokenData.Result.Data);
                }
                if (_dinazorIdentity.TokenUser == null)
                {
                    _dinazorIdentity.TokenUser = new TokenUser();
                }
                _dinazorIdentity.TokenUser.Token = string.Empty;
                _dinazorIdentity.TokenUser.Username = string.Empty;
                _dinazorIdentity.TokenUser.IsAuthenticated = false;
                return (_dinazorIdentity);
            }
            catch (Exception exce)
            {
                throw new Exception("Error while retrieving the authorization details. Please contact administrator.", exce);
            }
        }
         
        private static DinazorIdentity AuthenticateMe(TokenUser tokenUser)
        {
            _dinazorIdentity = new DinazorIdentity();
            _dinazorIdentity.TokenUser = new TokenUser();

            try
            {
                if (tokenUser != null)
                {
                    _dinazorIdentity.TokenUser = tokenUser;
                    _dinazorIdentity.IsAuthenticated = true;
                }
                else
                {
                    _dinazorIdentity.TokenUser.Token = string.Empty;
                    _dinazorIdentity.TokenUser.Username = string.Empty;
                    _dinazorIdentity.TokenUser.IsAuthenticated = false;
                }
                return (_dinazorIdentity);
            }
            catch (Exception exce)
            {
                throw new Exception("Error while retrieving the authorization details. Please contact administrator.", exce);
            }
        }

        private static DinazorIdentity AuthenticateMe(string token)
        {
            _dinazorIdentity = new DinazorIdentity();
            try
            {
                var tokenData = IocManager.Instance.Resolve<ITokenManager>().GetUserByToken(token);
                return AuthenticateMe(tokenData.Data);
            }

            catch (Exception ex)
            {
                throw new Exception("Error while retrieving the authorization details. Please contact administrator.", ex);
            }
        }
         
    }
}