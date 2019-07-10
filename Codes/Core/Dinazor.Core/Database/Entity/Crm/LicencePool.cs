using System.ComponentModel.DataAnnotations.Schema;
using Dinazor.Core.Interfaces.Databases;
using System.ComponentModel.DataAnnotations;

namespace Dinazor.Core.Database.Entity.Crm
{
    [Table("LicencePool", Schema = "crm")]
    public class LicencePool : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int AvailableQuantity { get; set; }
        public int QuantitySold { get; set; }

        public long IdOrganization { get; set; }
        [ForeignKey("IdOrganization")]
        public virtual Organization Organization { get; set; }

        public long IdModule { get; set; }
        [ForeignKey("IdModule")]
        public virtual ModuleEnum ModuleEnum { get; set; }
    }
}
