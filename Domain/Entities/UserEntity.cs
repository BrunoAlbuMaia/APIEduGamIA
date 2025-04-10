using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitys
{
    public class UserEntity
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Password { get; set; }

        [MaxLength(14)]
        public string? CPF { get; set; }

        [Required]
        public DateTime DtCreated { get; set; }


        public string FilialId { get; set; } // Relacionamento com a filial

        public bool IsActive { get; set; }

        public string? Role { get; set; }
    }
}
