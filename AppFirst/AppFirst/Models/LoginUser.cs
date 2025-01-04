using System.ComponentModel.DataAnnotations;

namespace AppFirst.Models;

public class LoginUser
{
    [Key]
    [Required]
    public int IdUser { get; set; }

    [Required]
    public int IdLoginType { get; set; }

    public string LoginType { get; set; }

    [Key]
    [Required]
    public DateTime Date { get; set; } = default;

    public override bool Equals(object obj)
    {
        return (IdUser == (obj as LoginUser).IdUser) && (Date == (obj as LoginUser).Date);
    }
}
