using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.src.entities
{
  public class TaskEntity
  {
    [Column("id")]
    public string Id { get; init; } = Guid.NewGuid().ToString();

    [Required]
    [Column("title")]
    public string? Title { get; set; }

    [Required]
    [Column("description")]
    public string? Description { get; set; }

    [Column("date_create")]
    public DateTime DateCreate { get; set; }

    [Column("date_update")]
    public DateTime DateUpdate { get; set; }

    [ForeignKey("userId")]
    [Column("user_id")]
    public string? UserId { get; init; }

    public UserEntity? User { get; set; }

  }
}