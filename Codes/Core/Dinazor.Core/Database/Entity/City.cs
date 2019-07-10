using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using Dinazor.Core.Interfaces.Databases;
using Newtonsoft.Json;

namespace Dinazor.Core.Database.Entity
{
    //[IgnoreOnUpdate]
    [Table("City", Schema = "dinazor")]
    public class City : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Town> TownList { get; set; }
    }
}
 