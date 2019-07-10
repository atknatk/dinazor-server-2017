using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using Dinazor.Core.Interfaces.Databases;

namespace Dinazor.Core.Database.Entity
{
    [Table("Town", Schema = "dinazor")]
    public class Town : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }

        public long IdCity { get; set; }
        [ForeignKey("IdCity")]
        public virtual City City { get; set; } 
    }
}
