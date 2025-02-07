using System.ComponentModel.DataAnnotations;

namespace AppFirst.Models;

public class LoginType
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(255)]
    public string Description { get; set; }

    public override bool Equals(object obj)
    {
        return Id == (obj as LoginType).Id;
    }
}
