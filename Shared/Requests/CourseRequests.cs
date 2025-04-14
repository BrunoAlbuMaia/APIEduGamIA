using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace Shared.Requests
{
    public class CourseRequests
    {
    }
    public class CoursePostRequests
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Username { get; private set; }

        public void setUsername(string username) { Username = username; }
    }
    public class CoursePostImageRequests
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
