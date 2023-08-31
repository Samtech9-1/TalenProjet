using System.ComponentModel.DataAnnotations;

namespace TalenProjet.Shared
{
    public class UserChangePasseword
    {
        [Required, StringLength(5, MinimumLength = 3)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "Les mots de passes ne sonts pas identiques")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
