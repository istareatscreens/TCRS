namespace TCRS.Shared.Objects.EmployeeLookup
{
    public class Employee
    {
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }

        public string GetEmployeeName()
        {
            return first_name + " " + last_name;
        }

    }
}
