using System.ComponentModel;

namespace TCRS.Shared.Enums
{

    //Source: https://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp
    /*
     * usage EnumType myLocal = EnumType.V1;
     * print(myLocal.ToDescriptionString()); 
     */
    public static class EnumExtensions
    {

        public static string ToDescriptionString(this CitizenCitationTypes val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static string ToDescriptionString(this VehicleCitationTypes val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
        public static string ToDescriptionString(this CitationTypes val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
