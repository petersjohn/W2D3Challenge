using CustomerRepo.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRepo.REPOs
{
    public class CustomerRepo
    {
        private List<Customer> _customerList = new List<Customer>();

        //create method

        public bool CreateCustomer(Customer content)
        {
            _customerList.Add(content);
            return _customerList.Contains(content);
        }


        //Read
        public List<Customer> GetCustomerList()
        {
            return _customerList;
        }


        public Customer GetCustomerByID(int customerID)
        {
            foreach (var customer in _customerList)
            {
                if (customer.CustomerID == customerID)
                {
                    return customer;
                }
            }
            return null;
        }

        //update

        public bool UpdateCustomer(int memberID, string lastName, DateTime birthDate, DateTime enrollDate)
        {
            Customer content = GetCustomerByID(memberID);
            if (content != null)
            {
                content.CustomerID = memberID;
                content.LastName = lastName;
                content.BirthDate = birthDate;
                content.EnrollDate = enrollDate;

                return true;
            }
            return false;
        }
        //delete
        public bool DeleteCustomer(Customer customer)
        {
            return _customerList.Remove(customer);
        }

        public bool DeleteCustomerByID(int memberID)
        {
            Customer customer = GetCustomerByID(memberID);

            return DeleteCustomer(customer);
        }
    }
}