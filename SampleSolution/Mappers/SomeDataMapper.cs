using System;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;

namespace SampleSolution.Mappers
{
    public static class SomeDataMapper
    {
        public static Data.Contexts.Models.SomeData SomeAggregateToPersistanceModel(SomeAggregate aggregate)
        {
            return new Data.Contexts.Models.SomeData
            {
                Id = aggregate.Id,
                FirstName = aggregate.FirstName,
                MiddleName = aggregate.MiddleName,
                LastName = aggregate.LastName,
                Title = aggregate.Title,
                Color = aggregate.Color.Value,
                CreationDate = aggregate.CreationDate,
                FacebookUrl = aggregate.FacebookUrl.Value,
                BusinessUserId = aggregate.BusinessUserId
            };
        }

        public static Data.Contexts.Models.SomeData UpdateSomeDataCommandToPersistanceModel(UpdateSomeDataCommand updateSomeDataCommand)
        {
            return new Data.Contexts.Models.SomeData
            {
                Id = updateSomeDataCommand.SomeDataId,
                FirstName = updateSomeDataCommand.FirstName,
                MiddleName = updateSomeDataCommand.MiddleName,
                LastName = updateSomeDataCommand.LastName,
                Title = updateSomeDataCommand.Title,
                Color = updateSomeDataCommand.Color.Value,
                CreationDate = updateSomeDataCommand.CreationDate,
                FacebookUrl = updateSomeDataCommand.FacebookUrl.Value
            };
        }

        public static SomeAggregate PersistanceModelToAggregateRoot(Data.Contexts.Models.SomeData persistanceModel)
        {
            return SomeAggregate.Create(
                persistanceModel.Id,
                persistanceModel.FirstName,
                persistanceModel.MiddleName,
                persistanceModel.LastName,
                persistanceModel.Title,
                new Color(persistanceModel.Color),
                persistanceModel.CreationDate,
                new FacebookUrl(persistanceModel.FacebookUrl),
                persistanceModel.BusinessUserId);
        }
    }
}
