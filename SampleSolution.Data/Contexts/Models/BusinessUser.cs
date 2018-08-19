using System;
using System.Collections.Generic;

namespace SampleSolution.Data.Contexts.Models
{
    public class BusinessUser
    {
        public Guid Id { get; set; }
        public string IdentityId { get; set; }
        public ApplicationUser Identity { get; set; }  // navigation property
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }

        public virtual ICollection<SomeData> SomeData { get; set; }
    }
}
