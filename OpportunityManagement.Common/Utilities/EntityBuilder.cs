using Microsoft.Xrm.Sdk;
using System;

namespace OpportunityManagement.Common.Utilities
{
    public static class EntityExtensions
    {
        public static Entity With<TReturnValue>(this Entity subject, Func<Entity, TReturnValue> func)
        {
            func(subject);
            return subject;
        }
    }

    public class EntityBuilder
    {
        private readonly Entity _entity;

        public EntityBuilder()
        {
            _entity = new Entity();
        }

        public Entity Build()
        {
            return _entity;
        }

        public EntityBuilder Create(string logicalName)
        {
            _entity.With(e => e.LogicalName = logicalName);
            return this;
        }

        public EntityBuilder WithAttribute(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            _entity.With(e => e[key] = value);
            return this;
        }

        public EntityBuilder WithId(Guid id)
        {
            _entity.With(e => e.Id = id);
            return this;
        }
    }
}