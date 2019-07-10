using System;
using System.Collections.Concurrent;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Interfaces.Token;
using Dinazor.Core.Model;

namespace Dinazor.Module.AuthorizationManagement.TokenStore
{
    public class MemoryTokenStorer : ITokenStore
    {
        private volatile ConcurrentDictionary<string, TokenUser> _tokenUsers;

        public MemoryTokenStorer()
        {
            _tokenUsers = new ConcurrentDictionary<string, TokenUser>();
        }

        public DinazorResult StoreTheToken(string token, TokenUser user)
        {
            var result = IsTokenExists(token);
            if (result.Status != ResultStatus.Success)
            {
                _tokenUsers[token] = user;
                result.Status = ResultStatus.Success;
            }
            return result;
        }

        public DinazorResult DeleteTheToken(string token)
        {
            var result = IsTokenExists(token);
            if (result.Status == ResultStatus.Success)
            {
                TokenUser temp;
                bool tryToDelete = _tokenUsers.TryRemove(token, out temp);
                if (tryToDelete)
                {
                    result.Success();
                }
            }
            else
            {
                result.Status = ResultStatus.NoSuchObject;
            }
            return result;
        }

        public DinazorResult IsTokenExists(string token)
        {
            var result = new DinazorResult();
            if (string.IsNullOrEmpty(token))
            {
                result.Status = ResultStatus.MissingRequiredParamater;
                return result;
            }
            if (_tokenUsers.ContainsKey(token))
            {
                result.Status = ResultStatus.Success;
                return result;
            }
            return result;
        }

        public DinazorResult<TokenUser> GetUserByToken(string token)
        {
            var result = new DinazorResult<TokenUser>();
            if (!string.IsNullOrEmpty(token) && IsTokenExists(token).IsSuccess)
            {
                result.Data = _tokenUsers[token];
                result.Data.IsAuthenticated = true;
                result.Success();
            }
            return result;
        }

        public DinazorResult<bool> IsUserAlreadyLoggedIn(string username, string password)
        {
            var result = new DinazorResult<bool>();
            result.Data = false;
            if (_tokenUsers.IsEmpty)
            {
                result.Data = false;
                result.Success();
                return result;
            }

            foreach (var item in _tokenUsers)
            {
                if (item.Value.Username == username && item.Value.Password == password)
                {
                    result.Data = true;
                    result.Success();
                    return result;
                }
                else
                {
                    result.Data = false;
                    result.Success();
                }
            }
            return result;
        }

        public DinazorResult<TokenUser> GetUserByUsername(string username)
        {
            var result = new DinazorResult<TokenUser>();
            foreach (var item in _tokenUsers)
            {
                if (item.Value.Username == username)
                {
                    result.Data = item.Value;
                    result.Success();
                    return result;
                }
            }
            return result;
        }
    }
}