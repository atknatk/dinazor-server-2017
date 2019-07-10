using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dinazor.Core.Interfaces.Databases;
using Newtonsoft.Json;

namespace Dinazor.Core.Database.Entity.User
{
    [Table("RelUserUserGroup", Schema = "dinazor")]
    public class RelUserUserGroup : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }

        public long IdUserGroup { get; set; }
        [ForeignKey("IdUserGroup")]
        public virtual UserGroup UserGroup { get; set; }
        
    }
}
