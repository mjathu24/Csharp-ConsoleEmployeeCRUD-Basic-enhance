using System;
using Microsoft.Data.Sqlite;

namespace EmployeeCRUDApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n ----Employee Management----");
                Console.WriteLine("1.Create Employee");
                Console.WriteLine("2.List Employees");
                Console.WriteLine("3.Update Employee");
                Console.WriteLine("4.Delete Employee");
                Console.WriteLine("5.Exit");
                Console.Write("select an option: ");
                string option = Console.ReadLine();
                using (var connection = OpenConnection())
                {
                    switch (option)
                    {
                        case "1":
                            Console.Clear();
                            CreateEmployee(connection);
                            break;

                        case "2":
                            Console.Clear();
                            ListEmployees(connection);
                            break;

                        case "3":
                            Console.Clear();
                            UpdateEmployee(connection);
                            break;

                        case "4":
                            Console.Clear();
                            DeleteEmployee(connection);
                            break;

                        case "5":

                            exit = true;
                            break;

                        default:

                            Console.WriteLine("Invalid option.Please try again.");
                            break;

                    }

                    if (!exit)
                    {
                        Console.WriteLine("\n Press Enter to continue....");
                        Console.ReadLine();
                    }
                }
            }
        }

        static SqliteConnection OpenConnection()
        {
            var connection = new SqliteConnection("Data Source=employee.db;");
            connection.Open();
            string CreateTableQuery = @"CREATE TABLE IF NOT EXISTS Employees(
                EmployeeID INTEGER PRIMARY KEY AUTOINCREMENT,
                FirstName TEXT NOT NULL,
                LastName TEXT NOT NULL,
                DateOfBirth TEXT NOT NULL)";

            using (var command = new SqliteCommand(CreateTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
            return connection;
        }


        //create Employee
        static void CreateEmployee(SqliteConnection connection)
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name:");
            string lastName = Console.ReadLine();
            Console.Write("Enter Date of Birth (yyyy-mm-dd):");
            string dob = Console.ReadLine();

            bool result = EmployeeRepository.CreateEmployee(connection, firstName, lastName, dob);
            if (result == true)
            {
                Console.WriteLine("Employee Created Successfully");
            }
            else
            {
                Console.WriteLine("failed to create Employee.");
            }
        }


        //list of All Employees
        static void ListEmployees(SqliteConnection connection)
        {
            var emplist = EmployeeRepository.ListEmployees(connection);
          
                    Console.WriteLine("\n----Employee List ----");
                   foreach(var emp in emplist)
                    {
                        Console.WriteLine($"ID: {emp[0]}, Name: {emp[1]}{emp[2]},DOB: {emp[3]}");
                    }
                
            
        }

        //Update Employee
        static void UpdateEmployee(SqliteConnection connection)
        {
            Console.Write("Enter Employee ID to update: ");
            int employeeID = int.Parse(Console.ReadLine());
            Console.Write("Enter New First Name: ");
            string newFirstName = Console.ReadLine();
            Console.Write("Enter New Last Name: ");
            string newLastName = Console.ReadLine();

           bool result = EmployeeRepository.UpdateEmployee(connection, newFirstName, newLastName, employeeID);
        if (result == true)
            {
                Console.WriteLine("Employee Updated Successfully");
            }
            else
            {
                Console.WriteLine("failed to create Update Employee");
            }
        }


        //Delete Employee
        static void DeleteEmployee(SqliteConnection connection)
        {
            Console.Write("Enter Employee ID to delete: ");
            int employeeID = int.Parse(Console.ReadLine());

         bool result = EmployeeRepository.DeleteEmployee(connection, employeeID);
         if (result == true)
            {
                Console.WriteLine("Employee Deleted Successfully");
            }
            else
            {
                Console.WriteLine("failed to Deleted Employee");
            }
        }
    }
}