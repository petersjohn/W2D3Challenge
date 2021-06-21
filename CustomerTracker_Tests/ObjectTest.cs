using CustomerRepo.POCOs;
using CustomerRepo.REPOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CustomerTracker_Tests
{
    [TestClass]
    public class ObjectTest
    {
        private readonly CustomerRepo.REPOs.CustomerRepo _repo = new CustomerRepo.REPOs.CustomerRepo();
        //POCO Testing

        [TestMethod]
        public void SetMemberID_ShouldSetToCorrectString()
        {
            Customer customer = new Customer
            {
                CustomerID = 1234
            };

            int expected = 1234;
            int actual = customer.CustomerID;

            Assert.AreEqual(expected, actual);

        }

        //Repo Method Testing
        [TestMethod]
        public void CustomerObjectCreation()
        {
            //arrange
            bool expected = true;

            int memberID = 1234;
            string lastName = "Test";
            DateTime birthDate = DateTime.ParseExact("05/16/1986", "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime enrollDate = DateTime.ParseExact("01/18/2015", "MM/dd/yyyy", CultureInfo.InvariantCulture);

            //Act
            Customer customer = new Customer(memberID, lastName, birthDate, enrollDate);
            bool actual = _repo.CreateCustomer(customer);
            //act & assert
            Assert.AreEqual(actual, expected);


        }
        [TestMethod]
        public void ContentList()//test the Create method
        {
            //arrange
            bool expected = true;
            bool actual = false;


            int memberID = 1234;
            string lastName = "Test";
            DateTime birthDate = DateTime.ParseExact("05/16/1986", "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime enrollDate = DateTime.ParseExact("01/18/2015", "MM/dd/yyyy", CultureInfo.InvariantCulture);

            //act
            Customer customer = new Customer(memberID, lastName, birthDate, enrollDate);
            _repo.CreateCustomer(customer);
            List<Customer> customers = _repo.GetCustomerList();
            if (customers.Count > 0)
            {
                actual = true;
            }

            //assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestUpdateCustomerLastName()
        {   //Arrange
            TestHelperMethod(); //1234
            int memberID = 1234;
            Customer customer = _repo.GetCustomerByID(memberID);
            string oldLastName = customer.LastName;
            string newLastName = "Tirey";

            //Act

            _repo.UpdateCustomerLastName(memberID,newLastName);
            Console.WriteLine(oldLastName);
            Console.WriteLine(customer.LastName);

            //Assert
            Assert.AreNotEqual(oldLastName, customer.LastName);

        }

        [TestMethod]
        public void TestUpdateDOB()
        {    //Arrange
            TestHelperMethod();
            DateTime dob = DateTime.ParseExact("05/16/1985", "MM/dd/yyyy", CultureInfo.InvariantCulture);

            //Act
            Customer customer = _repo.GetCustomerByID(1234);
            Console.WriteLine(customer.Age);
            int oldAge = customer.Age;
            _repo.UpdateCustomerDOB(customer.CustomerID, dob);
            
            int newAge = customer.Age;
            
            Console.WriteLine(customer.Age);
            //Assert
            Assert.AreNotEqual(oldAge, newAge);

            

        }
        public void TestHelperMethod()
        {
           int memberID = 1234;
           string lastName = "Test";
           DateTime birthDate = DateTime.ParseExact("05/16/1986", "MM/dd/yyyy", CultureInfo.InvariantCulture);
           DateTime enrollDate = DateTime.ParseExact("01/18/2015", "MM/dd/yyyy", CultureInfo.InvariantCulture);
           Customer customer = new Customer(memberID, lastName, birthDate, enrollDate);
           _repo.CreateCustomer(customer);
            
        }
            





    }









}



