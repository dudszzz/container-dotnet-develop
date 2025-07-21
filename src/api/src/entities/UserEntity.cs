using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.src.entities
{
  public class UserEntity
  {
    [Column("id")]
    public string Id { get; init; } = Guid.NewGuid().ToString();

    [Required]
    [Column("email")]
    public string? Email { get; set; }

    [Required]
    [Column("password")]
    public string? Password { get; set; }

    [Column("date_create")]
    public DateTime DateCreate { get; set; }

    [Column("date_update")]    
    public DateTime DateUpdate { get; set; }

    public ICollection<TaskEntity>? Tasks { get; set; } = new Collection<TaskEntity>();
  
  }
}