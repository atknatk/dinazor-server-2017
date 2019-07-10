using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dinazor.Core.Database.Attributes;
using Dinazor.Core.Interfaces.Databases;
using Newtonsoft.Json;

namespace Dinazor.Core.Database.Entity.User
{
    [Table("User", Schema = "dinazor")]
    public class User : ISoftDelete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool IsDeleted { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }

        [JsonIgnore]
        public virtual ICollection<RelUserUserGroup> RelUserUserGroupList{ get; set; }
    }
}
