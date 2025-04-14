using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CoursesEntity
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string?description { get;set; }
        public string? image { get; set; }
        public bool? isActive { get; set; }
        public DateTime? create_at { get; set; }
        public DateTime? update_at { get; set; }
        public string? user_create { get; set; }
        public string? user_update { get; set; }
    }
}
