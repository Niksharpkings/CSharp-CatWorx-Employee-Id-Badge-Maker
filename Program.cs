// Import correct packages
//using System; //Not Need Anymore! // for Console class and related methods and properties (e.g. WriteLine)
//using System.Collections.Generic; //Not Need Anymore! // for List class and related methods and properties (e.g. Add)
//using System.Threading.Tasks; //Not Need Anymore! // Add this line to import the Task class from the System.Threading.Tasks namespace using System.IO; // for File class and related methods and properties (e.g. ReadAllLines) using System.Net.Http; // for HttpClient class and related methods and properties (e.g. GetStringAsync) using SkiaSharp; // Add this line to import the SkiaSharp library for drawing images and text on a canvas (https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/) namespace CatWorx.BadgeMaker { class Program { static void Main(string[] args) { // Create a new HttpClient object

namespace CatWorx.BadgeMaker
{
  class Program
  {
    async static Task Main(string[] args)
    {
      // first dotnet run console OutPut
      bool confirmed = false; // Set confirmed to false
      string IntroName; // Declare a string variable called Key

      Console.Write("Hello there! Please Tell Me Your Name:"); // Prompt the user for their name
      IntroName = Console.ReadLine() ?? " "; // Read the user's name and store it in the Key variable
      Console.WriteLine("Well Hello There!" + IntroName + "! Welcome To CatWorx, Lets Make Sum Employee Badges!"); // Print a welcome message to the console window

      ConsoleKey response; // Declare a ConsoleKey variable called response

      do
      {
        Console.Write("Use the API to randomly generate Employees? [y/n]");
        response = Console.ReadKey(false).Key;   // true is intercept key (dont show), false is show
        if (response != ConsoleKey.Enter) // If the user pressed a key other than Enter
          Console.WriteLine(); // Print a new line
      } while (response != ConsoleKey.Y && response != ConsoleKey.N); // Loop while the user has not pressed Y or N

      confirmed = response == ConsoleKey.Y; // Set confirmed to true if the user pressed Y and false otherwise
      if (!confirmed)
      {
        List<Employee> employees = await PeopleFetcher.GetEmployees(); // Create a new List of Employee objects called employees and assign it the value returned by the GetEmployees method
        Util.PrintEmployees(employees); // Call the PrintEmployees method and pass it the employees List
        Util.MakeCSV(employees); // Call the MakeCSV method and pass it the employees List
        await Util.MakeBadges(employees); // Call the MakeBadges method and pass it the employees List
      }
      else
      {
        List<Employee> employees = await PeopleFetcher.GetFromApi(); // Create a new List of Employee objects called employees and assign it the value returned by the GetFromApi method
        Util.PrintEmployees(employees); // Call the PrintEmployees method and pass it the employees List
        Util.MakeCSV(employees); // Call the MakeCSV method and pass it the employees List
        await Util.MakeBadges(employees); // Call the MakeBadges method and pass it the employees List
      }
    }
  }
}