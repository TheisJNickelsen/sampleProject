using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleSolution.Data.Contexts.Models
{
    public class SomeData
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string FacebookUrl { get; set; }
        public DateTime CreationDate { get; set; }

        public Guid BusinessUserId { get; set; }
    }
}
