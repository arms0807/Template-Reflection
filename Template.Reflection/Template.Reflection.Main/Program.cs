using System;
using Template.Reflection.Model;
using Template.Reflection.SqlHelper;
using Template.Reflection.Attribute;

namespace Template.Reflection.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var temp = new Crud_Template<User>().Read(2);
                var temp2 = new Crud_Template<Company>().Read(4);
                var temp_list = new Crud_Template<Company>().All();
                temp.ShowAttributes();
                //Console.WriteLine(new Crud_Template<Company>().Delete<Company>(15));
                var user  = new Crud_Template<Company>().Insert(temp2);
                var update = new Crud_Template<Company>().Update(new Company { 
                                                        Id = 1,
                                                        Name = "Bob",
                                                        CreateTime = DateTime.Now,
                                                    });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
