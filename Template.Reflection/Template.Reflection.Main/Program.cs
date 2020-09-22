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
                var temp2 = new Crud_Template<Company>().Read(3);
                var temp_list = new Crud_Template<Company>().All();
                temp.ShowAttributes();
                //Console.WriteLine(new Crud_Template<Company>().Delete<Company>(15));
                new Crud_Template<User>().Insert(temp);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
