using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using Dinazor.Core.Interfaces.Databases;
using Newtonsoft.Json;

namespace Dinazor.Core.Database.Entity.User
{
    [Table("RoleGroup", Schema = "dinazor")]
    public class RoleGroup : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<RelRoleRoleGroup> RelRoleRoleGroup { get; set; }
      /*  [JsonIgnore]
        public virtual ICollection<Authorization> AuthorizationList { get; set; }*/
    }
}
