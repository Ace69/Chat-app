using Communication.BL.Models;
using Communication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.BL.Mappers
{
    public abstract class AbstractMapper<E, M> : IMapper<E, M> where E : EntityBase where M : ModelBase
    {
        public abstract M EntityToModel(E entity);
        public abstract E ModelToEntity(M model);

        public ICollection<M> MapList(ICollection<E> entities)
        {
            if (entities == null)
            {
                return null;
            }

            ICollection<M> col = new List<M>();
            foreach (var entity in entities)
            {
                col.Add(this.EntityToModel(entity));
            }
            return col;
        }

        public ICollection<E> MapList(ICollection<M> models)
        {
            if(models == null)
            {
                return null;
            }

            ICollection<E> col = new List<E>();
            foreach (var model in models)
            {
                col.Add(this.ModelToEntity(model));
            }
            return col;
        }
    }
}
