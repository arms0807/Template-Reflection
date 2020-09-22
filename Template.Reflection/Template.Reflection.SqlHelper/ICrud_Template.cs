using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Reflection.SqlHelper
{
    public interface ICrud_Template<T> where T : class
    {
        public T Read(int id);
        public ICollection<T> All();
    }
}
