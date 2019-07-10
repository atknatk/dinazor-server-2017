using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dinazor.Core.Interfaces.Databases;

namespace Dinazor.Core.Database.Entity.Configuration
{
    [Table("Configuration", Schema = "dinazor")]
    public class Configuration : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
