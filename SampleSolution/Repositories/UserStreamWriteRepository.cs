using Elasticsearch.Net;
using Nest;
using SampleSolution.Domain.Events.Events;
using SampleSolution.Mappers;
using System;

namespace SampleSolution.Repositories
{
    public class UserStreamWriteRepository : IUserStreamWriteRepository
    {
        private readonly IElasticClient _elasticClient;

        public UserStreamWriteRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        }

        public void Add(UserCreatedEvent userCreatedEvent)
        {
            try
            {
                var userStream = ApplicationUserMapper.UserCreatedEventToStream(userCreatedEvent);

                var resp = _elasticClient.Index(userStream, i => i
                    .Refresh(Refresh.True)
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
