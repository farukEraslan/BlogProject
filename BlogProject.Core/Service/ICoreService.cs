using BlogProject.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Core.Service
{
    public interface ICoreService<T> where T : CoreEntity
    {
        public bool Add(T item);
        public bool Add(List<T> items);
        public bool Remove(T item);
        public bool Remove(Guid id);
        public bool RemoveAll(Expression<Func<T, bool>> predicate); // Belli bir LINQ ifadesine göre filtreleyip silmek için yazılan servis metodu. Metodun içine LINQ ifadesi verilecektir.
        public bool Update(T item);
        public T GetById(Guid id);
        public T GetByDefault(Expression<Func<T, bool>> predicate); // FirstOrDefault'a benzer bir metot oluşturur.
        public List<T> GetActive();
        public List<T> GetDefault(Expression<Func<T, bool>> predicate);
        public List<T> GetAll();
        public bool Activate(Guid Id); // Aktifleştirmek için kullanılacak metot.
        public bool Any(Expression<Func<T, bool>> predicate); // LINQ ifadesi ile var mı diye sorgulama yapacağımız metot.
        public int Save(); // DB'de manipülasyon işleminden sonra 1 veya daha fazla satır eklendiğinde bize kaç satırın etkilendiğini döndürecek metot
    }
}
