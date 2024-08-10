using AllinLobby.Bussiness.Abstract;
using AllinLobby.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Bussiness.Concrete
{
    public class GenericManager<T>(IRepository<T> _repository) : IGenericService<T> where T : class
    {
        public int TCount()
        {
            return _repository.Count();
        }

        public void TCreate(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _repository.Create(entity);
        }

        public void TDelete(int id)
        {
            _repository.Delete(id);
        }

        public int TFilteredCount(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _repository.FilteredCount(predicate);
        }

        public T TGetByFiltered(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _repository.GetByFiltered(predicate);
        }

        public T TGetById(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity;
        }

        public List<T> TGetFilteredList(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _repository.GetFilteredList(predicate);
        }

        public List<T> TGetList()
        {
            return _repository.GetList();
        }

        public void TUpdate(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _repository.Update(entity);
        }
    }
}