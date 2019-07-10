using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dinazor.Core.Interfaces.Databases;

namespace Dinazor.Core.Database.Entity.Crm
{
    [Table("Organization", Schema = "crm")]
    public class Organization : ISoftDelete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Vdn { get; set; }

        public virtual ICollection<LicencePool> LicencePools { get; set; }
    }
}

