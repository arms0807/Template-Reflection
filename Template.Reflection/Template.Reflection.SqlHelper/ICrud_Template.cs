using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Reflection.SqlHelper
{
    public interface ICrud_Template<T> where T : class
    {
        public T Read(int id);
        public ICollection<T> All();
        public T Insert(T obj);
        public bool Delete<T>(int id);
        public T Update(T obj);
    }
}
