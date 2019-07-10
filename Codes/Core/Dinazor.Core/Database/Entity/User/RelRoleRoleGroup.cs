using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dinazor.Core.Interfaces.Databases;
using Newtonsoft.Json;

namespace Dinazor.Core.Database.Entity.User
{
    [Table("RelRoleRoleGroup", Schema = "dinazor")]
    public class RelRoleRoleGroup : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long IdRole { get; set; }
        [ForeignKey("IdRole")]
        public virtual Role Role { get; set; }

        public long IdRoleGroup { get; set; }
        [ForeignKey("IdRoleGroup")]
        public virtual RoleGroup RoleGroup { get; set; }

    }
}
