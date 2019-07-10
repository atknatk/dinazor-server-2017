using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dinazor.Core.Interfaces.Databases;

namespace Dinazor.Core.Database.Entity.Crm
{
    [Table("Client", Schema = "crm")]
    public class Client : ISoftDelete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<MacAddress> MacAddresses { get; set; }
        //if multiple hdd then logical or the serial numbers
        public string HddSerialNo { get; set; }
        public string BiosVersion { get; set; }
        // must be the same username in the dinazor.User
        public string Username { get; set; }
        // must be the same password in the dinazor.User
        public string Password { get; set; }
        //logical operations of all above
        public string ClientIdentifier { get; set; }
    }
}
