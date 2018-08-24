using Nest;
using SampleSolution.Models.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleSolution.Repositories
{
    public class UserStreamReadRepository : IUserStreamReadRepository
    {
        private readonly IElasticClient _elasticClient;

        public UserStreamReadRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        }

        public List<UserStream> GetByQuery(string query, int limit)
        {
            try
            {
                var searchResponse = _elasticClient.Search<UserStream>(s => s
                    .From(0)
                    .Size(limit)
                    .Query(q => q
                        .Bool(b => b
                            .Should(
                                mu => mu.MatchPhrasePrefix(m => m
                                    .Field(f => f.FullName)
                                    .Slop(3)
                                    .Query(query)),
                                mu => mu.MatchPhrasePrefix(m => m
                                    .Field(f => f.Email.Value)
                                    .Query(query)))
                            )));
                return searchResponse.Documents.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}