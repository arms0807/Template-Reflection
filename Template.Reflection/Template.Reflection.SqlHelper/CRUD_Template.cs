using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Template.Reflection.SqlHelper
{
    public class Crud_Template<T> : ICrud_Template<T> where T : class
    {
        private readonly string cstring = @"Data Source=DESKTOP-PSSM12B\ANTRA;Initial Catalog=Customer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public ICollection<T> All()
        {
            var type = typeof(T);
            var col = string.Join(",", type.GetProperties().Select(x => $"[{x.Name}]"));
            var sql = $"SELECT {col} FROM [{type.Name}]";
            var list = new List<T>();
            using (var conn = new SqlConnection(cstring))
            {
                var s = new SqlCommand(sql, conn);
                conn.Open();
                var exec = s.ExecuteReader();
                while (exec.Read())
                {
                    var oModel = Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    {
                        prop.SetValue(oModel, exec[prop.Name] == DBNull.Value ? null : exec[prop.Name]);
                    }
                    list.Add((T)oModel);
                }
            }
            return list;
        }

        public T Read(int id)
        {
            var type = typeof(T);
            var col = string.Join(",", type.GetProperties().Select(x => $"[{x.Name}]"));
            var sql = $"SELECT {col} FROM [{type.Name}] WHERE id = {id}";
            using (var conn = new SqlConnection(cstring))
            {
                var s = new SqlCommand(sql, conn);
                conn.Open();
                var exec = s.ExecuteReader();
                if(exec.Read())
                {
                    var oModel = Activator.CreateInstance(type);
                    foreach(var prop in type.GetProperties())
                    {
                        prop.SetValue(oModel, exec[prop.Name] == DBNull.Value ? null : exec[prop.Name]);
                    }
                    return (T)oModel;
                }
            }
            return default(T);
        }
    }
}
