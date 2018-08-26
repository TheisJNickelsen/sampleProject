﻿using SampleSolution.Common.Domain.Aggregates;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;
using System;

namespace SampleSolution.Domain.Aggregates
{
    public class SomeAggregate : AggregateRoot
    {
        //For Automapper
        protected SomeAggregate()
        {
        }
        public string FirstName { get; protected set; }
        public string MiddleName { get; protected set; }
        public string LastName { get; protected set; }
        public string Title { get; protected set; }
        public Color Color { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public FacebookUrl FacebookUrl { get; protected set; }
        public Guid BusinessUserId { get; protected set; }

        public static SomeAggregate Create(Guid id, 
            string firstName, 
            string middleName, 
            string lastName, 
            string title, 
            Color color, 
            DateTime creationDate,
            FacebookUrl facebookUrl,
            Guid businessUserId)
        {
            return new SomeAggregate
            {
                Id = id,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Title = title,
                Color = color,
                CreationDate = creationDate,
                FacebookUrl = facebookUrl,
                BusinessUserId = businessUserId
            };
        }

        public static SomeAggregate Create(CreateSomeDataCommand createCommand, Guid businessUserId)
        {
            return new SomeAggregate
            {
                Id = createCommand.Id,
                FirstName = createCommand.FirstName,
                MiddleName = createCommand.MiddleName,
                LastName = createCommand.LastName,
                Title = createCommand.Title,
                Color = createCommand.Color,
                CreationDate = createCommand.CreationDate,
                FacebookUrl = createCommand.FacebookUrl,
                BusinessUserId = businessUserId
            };
        }

        public static SomeAggregate Create(SomeAggregate someAggregate, Guid businessUserId)
        {
            return new SomeAggregate
            {
                Id = someAggregate.Id,
                FirstName = someAggregate.FirstName,
                MiddleName = someAggregate.MiddleName,
                LastName = someAggregate.LastName,
                Title = someAggregate.Title,
                Color = someAggregate.Color,
                CreationDate = someAggregate.CreationDate,
                FacebookUrl = someAggregate.FacebookUrl,
                BusinessUserId = businessUserId
            };
        }

        public void ChangeFields(UpdateSomeDataCommand updateCommand)
        {
            if (updateCommand != null)
            {
                Color = updateCommand.Color;
                CreationDate = updateCommand.CreationDate;
                FacebookUrl = updateCommand.FacebookUrl;
                FirstName = updateCommand.FirstName;
                LastName = updateCommand.LastName;
                MiddleName = updateCommand.MiddleName;
                Title = updateCommand.Title;
            }
        }

        public SomeAggregate SendTo(Guid businessUserId)
        {
            return SomeAggregate.Create(Guid.NewGuid(), 
                FirstName,
                MiddleName,
                LastName,
                Title,
                Color,
                CreationDate,
                FacebookUrl,
                businessUserId);
        }
    }
}
