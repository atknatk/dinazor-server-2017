using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dinazor.Core.Interfaces.Databases;

namespace Dinazor.Core.Database.Entity.Crm
{
    [Table("MacAddress", Schema = "crm")]
    public class MacAddress : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Mac { get; set; }

        public long IdClient { get; set; }
        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; }
    }
}
