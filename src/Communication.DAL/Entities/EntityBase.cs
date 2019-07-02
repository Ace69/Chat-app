using System;
using ObjectDesign.Communication.DAL.Interfaces;

namespace Communication.DAL.Entities
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }
}
