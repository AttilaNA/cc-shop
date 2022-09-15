using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IDao<T>
    {
        void Add(T item);
        void Remove(int id);

        T Get(int id);
        
        T Get(string name);
        IEnumerable<T> GetAll();
    }
}
