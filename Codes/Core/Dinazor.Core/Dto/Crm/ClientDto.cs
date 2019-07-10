using System; 
using System.Linq.Expressions; 
using Dinazor.Core.Database.Entity.Crm;
using Dinazor.Core.Dto.Abstracts;

namespace Dinazor.Core.Dto.Crm
{
    public class ClientDto : UniqueDto<Client, ClientDto>
    {
        public string HddSerialNo { get; set; }
        public string BiosVersion { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientIdentifier { get; set; }

        public override Client ToEntity()
        {
            return new Client()
            {
                Id = Id,
                HddSerialNo = HddSerialNo,
                BiosVersion = BiosVersion,
                Username = Username,
                Password = Password,
                ClientIdentifier = ClientIdentifier
            };
        }

        public override Expression<Func<Client, ClientDto>> Select()
        {
            return l => new ClientDto()
            {
                Id = l.Id,
                HddSerialNo = l.HddSerialNo,
                BiosVersion = l.BiosVersion,
                Username = l.Username,
                Password = l.Password,
                ClientIdentifier = l.ClientIdentifier
            };
        }

        public override Expression<Func<Client, bool>> IsExist()
        {
            return l => l.ClientIdentifier== ClientIdentifier&& !l.IsDeleted;
        }



    }
}
