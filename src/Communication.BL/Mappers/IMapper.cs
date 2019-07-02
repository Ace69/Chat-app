using Communication.BL.Models;
using Communication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.BL.Mappers
{
    public interface IMapper<E,M> where E : EntityBase where M : ModelBase
    {
        M EntityToModel(E entity);

        E ModelToEntity(M model);

        ICollection<E> MapList(ICollection<M> models);

        ICollection<M> MapList(ICollection<E> entities);
    }
}
