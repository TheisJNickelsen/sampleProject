using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SampleSolution.Data.Contexts.MongoDb.Models
{
    public class UserStream
    {
        [BsonId]
        public ObjectId InternalId { get; set; }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
    }
}
