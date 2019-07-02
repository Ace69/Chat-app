using Communication.BL.Mappers;
using Communication.BL.Models;
using Communication.DAL;
using Communication.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Communication.BL.Repositories
{
    public class GenericRepository<E,M> : IRepository<E,M> where E : EntityBase where M : ModelBase
    {
        public CommunicationDbContext dbContext;
        public DbSet<E> dbSet;
        public IMapper<E,M> mapper;

        public GenericRepository(CommunicationDbContext dbContext , IMapper<E, M> mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            dbSet = dbContext.Set<E>();
        }

        public void Delete(M model)
        {
            dbContext.RemoveEntities<E>();
            dbSet.Remove(mapper.ModelToEntity(model));
            dbContext.SaveChanges();
        }

        public M GetById(Guid id)
        {
            return mapper.EntityToModel(dbSet.Find(id));
        }

        public void Insert(M model)
        {
            dbContext.RemoveEntities<E>();
            dbSet.Add(mapper.ModelToEntity(model));
            dbContext.SaveChanges();
        }

        public void Update(M model)
        {
            dbContext.RemoveEntities<E>();
            dbSet.Update(mapper.ModelToEntity(model));
            dbContext.SaveChanges();
        }
        public void UpdateNoSave(M model)
        {
            dbContext.RemoveEntities<E>();
            dbSet.Update(mapper.ModelToEntity(model));
        }

        public EntityEntry InsertNoSave(M model)
        {
            var entry = dbSet.Add(mapper.ModelToEntity(model));
            dbContext.SaveChanges();
            return entry;
        }

    }
}
