using Communication.BL.Models;
using Communication.DAL.Entities;
using System;

namespace Communication.BL.Repositories
{
    public interface IRepository<E,M> where E : EntityBase where M : ModelBase

    {
        M GetById(Guid id);

        void Insert(M entity);

        void Delete(M entity);

        void Update(M entity);

    }
}
