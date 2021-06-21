using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRepo.POCOs
{
    public class Customer
    {
        public Customer() { }

        public Customer(int customerID, string lastName, DateTime birthDate, DateTime enrollDate)
        {
            CustomerID = customerID;
            LastName = lastName;
            BirthDate = birthDate;
            EnrollDate = enrollDate;
        }
        public int CustomerID { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime EnrollDate { get; set; }

        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - BirthDate.Year;
                if (DateTime.Now.DayOfYear < BirthDate.DayOfYear)
                {
                    age = age - 1;
                    return age;
                }
                else
                {
                    return age;
                }
            }
        }

        public int EnrollmentYears
        {
            get
            {
                int yearsOfEnrollment = DateTime.Now.Year - EnrollDate.Year;
                if (DateTime.Now.DayOfYear < EnrollDate.DayOfYear)
                {
                    yearsOfEnrollment = yearsOfEnrollment - 1;
                    return yearsOfEnrollment;
                }
                return yearsOfEnrollment;
            }
        }

       
        private List<Customer> Customers { get; set; } = new List<Customer>();
        

    }
}
