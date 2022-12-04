// using System; //Not Need Anymore! //for Console class and related methods and properties (e.g. WriteLine)
// using System.IO; //Not Need Anymore! //for File class and related methods and properties (e.g. ReadAllLines)
// using System.Net; //Not Need Anymore! //for WebClient class and related methods and properties (e.g. DownloadString)
// using System.Collections.Generic; //for List class and related methods and properties (e.g. Add)
// using System.Threading.Tasks; //Not Need Anymore! //Add this line to import the Task class from the System.Threading.Tasks namespace
// using System.Net.Http; //Not Need Anymore!  //for HttpClient class and related methods and properties (e.g. GetStringAsync)
using Newtonsoft.Json.Linq; //for JObject class and related methods and properties (e.g. Parse)

namespace CatWorx.BadgeMaker
{
  class PeopleFetcher
  {
    public static async Task<List<Employee>> GetEmployees()
    {
      await Task.Delay(0); // This is a placeholder to make the method async and avoid compiler errors until you write your own code
      List<Employee> employees = new List<Employee>(); // Create a new List of Employee objects called employees to store the employees we get from the API in memory
      while (true)
      {
        //Input for First Name or else exit Application
        Console.WriteLine("Enter first name (leave empty to exit): "); // Prompt the user to enter a first name
        string firstName = Console.ReadLine() ?? "";
        if (firstName == "")
        {
          break;
        }
        //Input for last name
        Console.WriteLine("Enter last name: ");
        string lastName = Console.ReadLine() ?? "";

        //Input for Badge ID
        Console.WriteLine("Enter ID: ");
        int id = Int32.Parse(Console.ReadLine() ?? ""); //Parse to Int32

        //Input for Photo URL for Badge
        Console.Write("Enter Photo URL: ");
        string photoUrl = Console.ReadLine() ?? "";

        Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl); //Create new Employee Object with input data from user
        employees.Add(currentEmployee); //Add Employee to List of Employees to be returned at end of method call to a CSV.
      }
      return employees; //Return List of Employees
    }
    async public static Task<List<Employee>> GetFromApi()
    {
      List<Employee> employees = new List<Employee>(); // Create a new List of Employee objects called employees to store the employees we get from the API in memory
      // Calling the API and getting the JSON data back from an httpClient
      using (HttpClient client = new HttpClient()) // Create a new HttpClient object called client
      {
        string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture"); // Call the API and get the JSON data back
        Console.WriteLine(response); // print response to console
        JObject json = JObject.Parse(response); // parse response into JSON object

        foreach (JToken token in json.SelectToken("results")!) // Loop through the results array in the JSON API
        {
          string firstName = token.SelectToken("name.first")!.ToString(); // Get the first name from the JSON API
          string lastName = token.SelectToken("name.last")!.ToString(); // Get the last name from the JSON API
          int id = Int32.Parse(token.SelectToken("id.value")!.ToString()!.Replace("-", "")); // Get the ID from the JSON API and remove the dashes
          string photoUrl = token.SelectToken("picture.large")!.ToString(); // Get the photo URL from the JSON API
                                                                            // if Employee is not in the List of Employees, add them to the List of Employees to be returned at end of method call to a CSV.
          if (!employees.Exists(employee => employee.GetFullName() == firstName + " " + lastName))
          {
            Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl); // Create a new Employee object called currentEmployee
            employees.Add(currentEmployee); // Add the currentEmployee to the employees List
          }
          else // If the Employee is already in the List of Employees, do nothing
          {
            continue;
          }
        }
      }
      return employees; // Return the employees List
    }
  }
}

//           Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl); //
//           employees.Add(currentEmployee);
//         }
//       }
//       return employees;
//     }
//   }
// }

//         {
//           // Parse JSON data
//           Employee emp = new Employee
//           (
//               token.SelectToken("name.first")!.ToString(),
//               token.SelectToken("name.last")!.ToString(),
//               Int32.Parse(token.SelectToken("id.value")!.ToString().Replace("-", "")),
//               token.SelectToken("picture.large")!.ToString()
//           );
//           employees.Add(emp);
//         }
//       }
//       return employees;
//     }
//   }
// }