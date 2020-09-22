using System;
using Template.Reflection.Model;

namespace Template.Reflection.Attribute
{
    /// <summary>
    /// Show all the attributes in the specific type.
    /// </summary>
    public static class Attributes
    {
        /// <summary>
        /// None static method, it's a normal method to call Show Attributes method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        //public void showattributes<t>(t obj) where t : basemodel
        //{
        //    var type = typeof(t);
        //    foreach (var prop in type.getproperties())
        //    {
        //        console.writeline($"{prop.name} has value {prop.getvalue(obj)} where type is {prop.propertytype}");
        //    }
        //}

        /// <summary>
        /// Extension Method, which is easier to be called with a generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static void ShowAttributes<T>(this T obj) where T : BaseModel
        {
            var type = typeof(T);
            foreach (var prop in type.GetProperties())
            {
                Console.WriteLine($"{prop.Name} has value {prop.GetValue(obj)} where type is {prop.PropertyType}");
            }
        }
    }
}
