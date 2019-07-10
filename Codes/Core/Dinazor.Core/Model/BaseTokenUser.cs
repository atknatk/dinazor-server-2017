using System.Collections.Generic; 
using Dinazor.Core.Dto;
using Dinazor.Core.Dto.Crm;

namespace Dinazor.Core.Model
{
    public class BaseTokenUser
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //for licence
        public ClientDto Client { get; set; }

    }

    public class TokenUser : BaseTokenUser
    {
        public TokenUser()
        {
            OrganizationLicence = new List<OrganizationLicenceDto>();
            CustomAttributes = new Dictionary<string, object>();
        }

        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public bool IsLicenceValidated { get; set; }
        public List<OrganizationLicenceDto> OrganizationLicence { get; set; }

        public List<long> RoleList { get; set; }

        public Dictionary<string,object> CustomAttributes { get; set; }
    }

}
