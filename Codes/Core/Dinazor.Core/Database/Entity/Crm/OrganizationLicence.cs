
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dinazor.Core.Interfaces.Databases;

namespace Dinazor.Core.Database.Entity.Crm
{
    [Table("OrganizationLicence", Schema = "crm")]
    public class OrganizationLicence :IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        //day
        public int Duration { get; set; }
        public bool IsInUse { get; set; }
        public DateTime ExpiresAt { get; set; }


        public long IdLicenceKey { get; set; }
        [ForeignKey("IdLicenceKey")]
        public virtual LicenceKey LicenceKey { get; set; }

        public long IdModule { get; set; }
        [ForeignKey("IdModule")]
        public virtual ModuleEnum ModuleEnum { get; set; }

        public long IdOrganization { get; set; }
        [ForeignKey("IdOrganization")]
        public virtual Organization Organization { get; set; }

        public long IdClient { get; set; }
        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; }
    }
}
