using AllinLobby.DataAccess.Abstract;
using AllinLobby.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DataAccess.Repositories
{
    public class GenericRepository<T>(AllinLobbyContext _context) : IRepository<T> where T : class
    {
        public DbSet<T> Table => _context.Set<T>();

        public int Count()
        {
            return Table.Count();
        }

        public void Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Table.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Table.Find(id);
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Table.Remove(entity);
            _context.SaveChanges();
        }

        public int FilteredCount(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return Table.Where(predicate).Count();
        }

        public T GetByFiltered(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return Table.Where(predicate).FirstOrDefault();
        }

        public T GetById(int id)
        {
            var entity = Table.Find(id);
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity;
        }

        public List<T> GetFilteredList(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return Table.Where(predicate).ToList();
        }

        public List<T> GetList()
        {
            return Table.ToList();
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Table.Update(entity);
            _context.SaveChanges();
        }
    }
}
