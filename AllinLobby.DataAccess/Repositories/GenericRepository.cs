using AllinLobby.DataAccess.Abstract;
using AllinLobby.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AllinLobby.DataAccess.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AllinLobbyContext _context;

        public GenericRepository(AllinLobbyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public DbSet<T> Table => _context.Set<T>();

        public int Count()
        {
            return Table.Count();
        }

        public void Create(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity to create cannot be null.");

            Table.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Table.Find(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), $"Entity with id {id} not found.");

            Table.Remove(entity);
            _context.SaveChanges();
        }

        public int FilteredCount(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            return Table.Count(predicate);
        }

        public T GetByFiltered(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            return Table.FirstOrDefault(predicate);
        }

        public T GetById(int id)
        {
            var entity = Table.Find(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), $"Entity with id {id} not found.");

            return entity;
        }

        public List<T> GetFilteredList(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            return Table.Where(predicate).ToList();
        }

        public List<T> GetList()
        {
            return Table.ToList();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity to update cannot be null.");

            Table.Update(entity);
            _context.SaveChanges();
        }
    }
}
