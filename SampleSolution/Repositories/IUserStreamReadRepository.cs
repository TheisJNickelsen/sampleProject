using SampleSolution.Models.ElasticSearch;
using System.Collections.Generic;

namespace SampleSolution.Repositories
{
    public interface IUserStreamReadRepository
    {
        List<UserStream> GetByQuery(string query, int limit);
    }
}
