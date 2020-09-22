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
                var temp = new Crud_Template<User>().Read(1);
                var temp_list = new Crud_Template<Company>().All();
                //new Attributes().ShowAttributes(temp);
                temp.ShowAttributes();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
