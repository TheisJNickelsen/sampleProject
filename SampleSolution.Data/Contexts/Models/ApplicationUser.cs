using Microsoft.AspNetCore.Identity;

namespace SampleSolution.Data.Contexts.Models
{

    public class ApplicationUser : IdentityUser
    {
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
