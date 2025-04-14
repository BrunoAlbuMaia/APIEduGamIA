using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entitys
{

    public class UsersComplEntity
    {
        public int id { get; set; }

        public int user_id { get; set; }

        public string email { get; set; } = null!;
        public string? username { get; set; }
        
       
        public string password_hash { get;  set; } = null!;

        public string? preferences { get; set; }

        public DateTime? lastLogin { get; set; }

        public string? role { get; set; } 


    }
    public class UsersEntity
    {
        public int id { get; set; }

        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;

        public bool isActive { get; set; } = true;

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        // Relacionamento 1:1
        public UsersComplEntity? UserCompl { get; set; }
    }
}
