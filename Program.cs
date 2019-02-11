using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class Program
{
    IList<Employee> employeeList;
    IList<Salary> salaryList;

    public Program()
    {
        employeeList = new List<Employee> {
            new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
            new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
            new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
            new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
            new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
            new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
            new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
        };

        salaryList = new List<Salary> {
            new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},

            new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},

            new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},

            new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},

            new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},

            new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},

            new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
        };
    }

    public static void Main()
    {
        Program program = new Program();


        //Print total Salary of all the employee with their corresponding names in ascending order of their salary.
       program.Task1();
        //print Employee details of 2nd oldest employee including total monthly salary.
        program.Task2();
        //print Mean of Monthly, Performance, Bonus salary of employees whose age is greater than 30
        program.Task3();
        Console.ReadLine();
    }

   
    public void Task1()
    {
        var taskQuery = from employee in employeeList
                     join salary in salaryList
                     on employee.EmployeeID equals salary.EmployeeID into employee1
                     select new
                     {
                         Name = employee.EmployeeFirstName + employee.EmployeeLastName,
                         TotalSalary = employee1.Sum(s => s.Amount)
                     };
        var taskQuery1 = from employee in taskQuery orderby employee.TotalSalary select employee;

        Console.WriteLine("Total salary Of All The Employee in ascending order of their salary with Name: ");
        foreach (var emp in taskQuery1)
        {
           
            Console.WriteLine($"Name : {emp.Name} \t Total Salary : {emp.TotalSalary}");
        }
        Console.WriteLine();
    }
     
    public void Task2()
    {
        var taskQuery2 = from employee in employeeList
                         join salary in salaryList
                         on employee.EmployeeID equals salary.EmployeeID
                         where salary.Type == SalaryType.Monthly 
                         orderby employee.Age descending
                         select new
                         {
                             employee,
                             salary
                         }into result group result by result.employee.EmployeeID;

        Console.WriteLine("Employee details of 2nd oldest employee including total monthly salary.");
        foreach (var emp in taskQuery2.Take(2).Skip(1))
        {
            
            foreach(var emp1 in emp)
            {
                Console.WriteLine();
                Console.WriteLine($"Id:{emp1.employee.EmployeeID}");
                Console.WriteLine($"Name: {emp1.employee.EmployeeFirstName} {emp1.employee.EmployeeLastName}");
                Console.WriteLine($"Age:{emp1.employee.Age}");
                Console.WriteLine($"Monthly Salary:{emp1.salary.Amount}");
            
            }
        }
        Console.WriteLine();
    }

    public void Task3()
    {
      
        var taskQuery3 = from employee in employeeList where employee.Age > 30
                         join salary in salaryList
                         on employee.EmployeeID equals salary.EmployeeID into employee1
                         select new {
                             EmployeeId = employee.EmployeeID,
                             EmployeeName = employee.EmployeeFirstName + employee.EmployeeLastName,
                             EmployeeAge = employee.Age,
                             AverageSalary = employee1.Average(s=>s.Amount)
                             
                         };


        Console.WriteLine("Mean of Monthly, Performance, Bonus salary of employees whose age is greater than 30");
        foreach (var emp in taskQuery3)
        {
            Console.WriteLine();
            Console.WriteLine($"Name : {emp.EmployeeName} \t Age :{emp.EmployeeAge} \t Average Salary: {emp.AverageSalary}");

        }
    }
}

public enum SalaryType
{
    Monthly,
    Performance,
    Bonus
}

public class Employee
{
    public int EmployeeID { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public int Age { get; set; }
}

public class Salary
{
    public int EmployeeID { get; set; }
    public int Amount { get; set; }
    public SalaryType Type { get; set; }
}