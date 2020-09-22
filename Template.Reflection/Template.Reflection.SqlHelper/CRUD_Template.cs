using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Template.Reflection.Model;

namespace Template.Reflection.SqlHelper
{
    public class Crud_Template<T> : ICrud_Template<T> where T : BaseModel
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

        public bool Delete<T>(int id)
        {
            var type = typeof(T);
            var sql = $"DELETE FROM [{type.Name}] WHERE [Id] = {id}";
            using(var conn = new SqlConnection(cstring))
            {
                var s = new SqlCommand(sql, conn);
                conn.Open();
                var num = s.ExecuteNonQuery();
                if(num == 0)
                {
                    return false;
                }
                Console.WriteLine($"Affect {num} rows");
                return true;
            }
        }

        public T Insert(T obj)
        {
            var type = typeof(T);
            var props = type.GetProperties().Where(name => name.Name != "Id");
            var col = string.Join(",", props.Select(x => $"[{x.Name}]"));
            var val = string.Join(",", props.Select(c => $"@{c.Name}"));
            var sql = $"INSERT INTO [{type.Name}]({col}) VALUES {val}";
            var param = props.Select(x => new SqlParameter($"@{x.Name}", x.GetValue(obj) ?? DBNull.Value)).ToArray();
            using (var conn = new SqlConnection(cstring))
            {
                var s = new SqlCommand(sql, conn);
                s.Parameters.AddRange(param);
                conn.Open();
                var num = s.ExecuteNonQuery();
                if(num == 0)
                {
                    throw new Exception("Something went wrong when you insert the data");
                }
            }
            return obj;
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

        public T Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
