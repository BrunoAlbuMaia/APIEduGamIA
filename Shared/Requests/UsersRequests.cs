using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{

    public class UsersRequests
    {
        [Required(ErrorMessage = "O primeiro nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O primeiro nome deve ter entre 2 e 100 caracteres.")]
        public string firstName { get; set; } = null!;

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O sobrenome deve ter entre 2 e 100 caracteres.")]
        public string lastName { get; set; } = null!;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string email { get; set; } = null!;

        [StringLength(50, ErrorMessage = "O nome de usuário deve ter no máximo 50 caracteres.")]
        public string? username { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
        ErrorMessage = "A senha deve ter no mínimo 6 caracteres e conter ao menos uma letra maiúscula, uma minúscula, um número e um caractere especial.")]
        public string passwordHash { get; set; } = null!;


        [DataType(DataType.Text)]
        public string? preferences { get; set; }

        [StringLength(20, ErrorMessage = "O campo role deve ter no máximo 20 caracteres.")]
        public string? role { get; set; }
    }


    public class UsersPutRequests
    {
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public string email { get; set; } = null!;
        public string? username { get; set; }
        public string? role { get; set; }
    }
}
