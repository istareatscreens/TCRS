using System;
using System.Collections.Generic;
using System.Globalization;

namespace TCRS.Shared.Helper
{
    public static class ObjectPrinter
    {
        public static IEnumerable<KeyValuePair<string, string>> PropertiesOfType(object obj)
        {

            var result = new List<KeyValuePair<string, string>>();


            foreach (var prop in obj.GetType().GetProperties())
            {

                if (prop.PropertyType == typeof(string))
                {
                    result.Add(new KeyValuePair<string, string>(prop.Name, (string)prop.GetValue(obj)));
                }
                else if (prop.PropertyType == typeof(double))
                {
                    result.Add(new KeyValuePair<string, string>(prop.Name, $"{(double)prop.GetValue(obj)}"));
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    result.Add(new KeyValuePair<string, string>(prop.Name,
                        ((DateTime)prop.GetValue(obj)).ToString("D", CultureInfo.CreateSpecificCulture("en-US"))
                        ));
                }
                else if (prop.PropertyType == typeof(int))
                {
                    result.Add(new KeyValuePair<string, string>(prop.Name, $"{(int)prop.GetValue(obj)}"));
                }
                else if (prop.PropertyType == typeof(bool))
                {
                    result.Add(new KeyValuePair<string, string>(prop.Name, $"{(bool)prop.GetValue(obj)}"));
                }

            }

            return result;

        }
    }
}
