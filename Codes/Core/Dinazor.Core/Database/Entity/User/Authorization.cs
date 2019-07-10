using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dinazor.Core.Interfaces.Databases;

namespace Dinazor.Core.Database.Entity.User
{
    [Table("Authorization", Schema = "dinazor")]
    public class Authorization : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long IdRoleGroup { get; set; }
        [ForeignKey("IdRoleGroup")]
        public virtual RoleGroup RoleGroup { get; set; }

        public long IdUserGroup { get; set; }
        [ForeignKey("IdUserGroup")]
        public virtual UserGroup UserGroup { get; set; }
    }
}
