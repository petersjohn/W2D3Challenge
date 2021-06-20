using CustomerRepo.POCOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTracker
{
    class ProgramUI
    {
        private bool _isRunning = true;

        private readonly CustomerRepo.REPOs.CustomerRepo _repo = new CustomerRepo.REPOs.CustomerRepo();

        public void Start()
        {
            RunMenu();
        }

        private void RunMenu()
        {
            while (_isRunning)
            {
                string userInput = GetMenuSelection();
                OpenMenuItem(userInput);
            }
        }

        
        private string GetMenuSelection()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Komodo Insurance Customer Tracking System!\n" +
                "Please Select a menu option from the list: \n" +
                "1. View All Current Members \n" +
                "2. View Member by ID\n" +
                "3. Create New Member\n" +
                "4. Update Member Information\n" +
                "5. Delete a Member Record\n" +
                "6. Exit Application");
            string userInput = Console.ReadLine();
            return userInput;
        }
        private void OpenMenuItem(string userInput)
        {
            Console.Clear();
            switch (userInput)
            {
                case "1":
                    DisplayAllMembers();
                    break;
                case "2":
                    GetMemberByID();
                    break;
                case "3":
                    CreateNewMember();
                    break;
                case "4":
                    RunUpdateMenu();
                    break;
                case "5":
                    DeleteMember();
                    break;
                case "6":
                    //exit
                    _isRunning = false;
                    return;
                default:
                    Console.WriteLine("Please enter a valid selection 1-4.");
                    GetMenuSelection();
                    return;
            }
        }



        private void RunUpdateMenu()
        {
            string userInput = GetUpdateMenu();
            OpenUpdateSelection(userInput);
        }
        private string GetUpdateMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select from the options below:\n" +
                "1. Update Member Last Name\n" +
                "2. Update Member Age \n" +
                "3. Update Member Enrollment Date\n" +
                "4. Cancel and Return to Main Menu ");
                
            string userInput = Console.ReadLine();
            return (userInput);

         }

        private void OpenUpdateSelection(string userInput)
        {
            Console.Clear();
            switch (userInput)
            {
                case "1":
                    UpdateMemberName();
                    break;
                case "2":
                    UpdateMemberAge();
                    break;
                case "3":
                    UpdateMemberEnrollDate();
                    break;
                case "4":
                    ReturnToMenu();
                    break;
                default:
                    Console.WriteLine("Please Enter a Valid Selection.");
                    return;
            }
        }

        private void DeleteMember()
        {
            Console.Clear();
            Console.WriteLine("Enter the ID that you wish to remove");
            int memberID = int.Parse(Console.ReadLine());
            if (_repo.DeleteCustomerByID(memberID))
            {
                Console.WriteLine("Delete Successful");
                ReturnToMenu();
            }
            else
            {
                Console.WriteLine("Delete Failed");
                ReturnToMenu();
            }


        }
        private void CreateNewMember()
        {
            Console.Clear();
            Console.WriteLine("Enter a Member ID: ");
            int memberID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the Member Last Name: ");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter the Member DOB (mm/dd/yyyy):");
            string userInputBirthDate = Console.ReadLine();
            DateTime birthDate = DateTime.ParseExact(userInputBirthDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            Console.WriteLine("Enter the Enrollment Date in Member Plan: ");
            string userInputEnrollDate = Console.ReadLine();
            DateTime enrollDate = DateTime.ParseExact(userInputEnrollDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            Customer customer = new Customer(memberID, lastName, birthDate, enrollDate);

            if (_repo.CreateCustomer(customer))
            {
                Console.WriteLine("New Member Record Created!");
                ReturnToMenu();
            }
            else
            {
                Console.WriteLine("Something went wrong, new member record not created, sorry yo.");
                ReturnToMenu();
            }

        }
        private void UpdateMemberEnrollDate()
        {
            Console.Clear();
            Console.WriteLine("Enter the member ID");
            int memberID = int.Parse(Console.ReadLine());

            Customer customer = _repo.GetCustomerByID(memberID);
            if (customer == null)
            {
                Console.WriteLine("Customer Not Found");
                return;
            }
            Console.WriteLine("Enter the new member enrollment date (Format mm/dd/yyyy)");
            string userInputEnrollDate = Console.ReadLine();
            DateTime enrollDate = DateTime.ParseExact(userInputEnrollDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (_repo.UpdateCustomer(memberID, customer.LastName, customer.BirthDate, enrollDate))
            {
                Console.WriteLine("Update Successful");
                ReturnToMenu();
            }
            else
            {
                Console.WriteLine("Update Failed");
                ReturnToMenu();
            }
        }

        private void UpdateMemberAge()
        {
            Console.Clear();
            Console.WriteLine("Enter the member ID");
            int memberID = int.Parse(Console.ReadLine());

            Customer customer = _repo.GetCustomerByID(memberID);
            if (customer == null)
            {
                Console.WriteLine("Customer Not Found");
                return;
            }
            Console.WriteLine("Enter the member birth date (Format mm/dd/yyyy)");
            string userInputBirthDate = Console.ReadLine();
            DateTime birthDate = DateTime.ParseExact(userInputBirthDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if(_repo.UpdateCustomer(memberID, customer.LastName, birthDate, customer.EnrollDate))
            {
                Console.WriteLine("Update Successful");
                ReturnToMenu();
            }
            else
            {
                Console.WriteLine("Update Failed");
                ReturnToMenu();
            }


           
        }

        private void UpdateMemberName()
        {
            Console.Clear();
            Console.WriteLine("Enter the member ID: ");
            int memberID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new last name: ");
            string lastName = Console.ReadLine();

            Customer customer = _repo.GetCustomerByID(memberID);
            if(customer != null)
            {
                if(_repo.UpdateCustomer(customer.CustomerID, lastName, customer.BirthDate, customer.EnrollDate))
                {
                    Console.WriteLine("Update Successful.");
                    ReturnToMenu();
                }
                else
                {
                    Console.WriteLine("Update Failed");
                    ReturnToMenu();
                }
                
            }
            else
            {
                Console.WriteLine("Update Failed, please enter a valid member ID");
                ReturnToMenu();
            }

           
            
        }

        private void DisplayAllMembers()
        {
            List<Customer> customer = _repo.GetCustomerList();

            foreach (Customer item in customer)
            {
                DisplayCustomer(item);
            }
            ReturnToMenu();
        }

      

        private void DisplayCustomer(Customer item)
        {
            Console.WriteLine($"Member ID: {item.CustomerID}\n" +
                $"Member Last Name: {item.LastName}\n" +
                $"Member Age: {item.Age}\n" +
                $"Member Length of Patronage: {item.EnrollmentYears}\n" +
                $"\n");

        }

        private void GetMemberByID()
        {
            Console.WriteLine("Enter the member ID: ");
            int memberID = int.Parse(Console.ReadLine());
            Customer member = _repo.GetCustomerByID(memberID);

            if(member != null)
            {
                DisplayCustomer(member);
                ReturnToMenu();
            }
            else
            {
                Console.WriteLine("Invalid Member ID. Please press any key to return to main menu.");
                ReturnToMenu();
            }

        }

        private void ReturnToMenu()
        {
            {
                Console.WriteLine("Press any key to return to Main Menu.");
                Console.ReadKey();
                RunMenu();
            }
        }
    }
}
