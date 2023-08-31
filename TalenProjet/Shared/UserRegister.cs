using System.ComponentModel.DataAnnotations;

namespace TalenProjet.Shared
{
    public class UserRegister
    {
        [Required(ErrorMessage ="Email invalide"), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "le mdp doit avoir minimum 3 carractères et maximum 5 carractères"), StringLength(5, MinimumLength = 3)]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Les mots de passes ne sonts pas identiques")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
