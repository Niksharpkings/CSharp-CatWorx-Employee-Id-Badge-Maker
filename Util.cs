// Import correct packages
// using System; //Not Need Anymore! // for Console class and Console.WriteLine() method and Console.ReadLine() method and Console.Write() method and Console.Read() method and Console.Clear() method and Console.Beep() method
// using System.IO; //Not Need Anymore! // Import System.IO for File class and Directory class (for creating directories)
// using System.Collections.Generic; //Not Need Anymore! // Import correct packages using System; using System.IO; using System.Collections.Generic;
// using System.Net.Http; //Not Need Anymore! // for HttpClient class and related methods and properties (e.g. GetStringAsync)
// using System.Threading.Tasks; //Not Need Anymore! // Add this line to import the Task class from the System.Threading.Tasks namespace
using SkiaSharp; //Not Need Anymore! // Add this line to import the SkiaSharp library for drawing images and text on a canvas (https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/)

namespace CatWorx.BadgeMaker
{
  class Util
  {
    // PrintEmployees Method declared as "static"
    public static void PrintEmployees(List<Employee> employees)
    {
      for (int i = 0; i < employees.Count; i++) // Loop Through
      {
        string template = "{0,-10}\t{1,-20}\t{2}";
        Console.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl())); // Print Employee Info to Console Window
      }
    }

    //MakeCSV Method declared as "static"
    public static void MakeCSV(List<Employee> employees)
    //if true then add more employee data to the file "employees.csv" in the "data" folder (if it exists)
    {
      // Check to see if folder exists, if not remove from memory
      if (!Directory.Exists("data"))
      {
        // If not, create it
        Directory.CreateDirectory("data"); // Create the data folder if it doesn't exist yet
      }

      using (StreamWriter file = new StreamWriter("data/employees.csv"))
      {
        file.WriteLine("ID,Name,PhotoUrl"); // Write header row to CSV file

        // Loop over employees
        for (int i = 0; i < employees.Count; i++)
        {
          // Write each employee to the file
          string template = "{0},{1},{2}"; // Add a template string for the CSV format 
          file.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl())); // Write to file here using template and employees[i] properties 
        }
      }
    }
    async public static Task MakeBadges(List<Employee> employees) //public static async Make Badges
    {
      // Layout variables

      //Badge dimensions
      int BADGE_WIDTH = 669;
      int BADGE_HEIGHT = 1044;

      //Badge margins
      int PHOTO_LEFT_X = 184;
      int PHOTO_TOP_Y = 215;
      int PHOTO_RIGHT_X = 486;
      int PHOTO_BOTTOM_Y = 517;

      //Badge text
      int COMPANY_NAME_Y = 150; // Y position of company name

      // Company name font size
      int EMPLOYEE_NAME_Y = 600; // Employee name font size

      //Employee Id font
      int EMPLOYEE_ID_Y = 730; //Employee Id font size

      // instance of HttpClient is disposed after code in the block has run
      using (HttpClient client = new HttpClient())
      {
        // Check to see if folder exists 
        for (int i = 0; i < employees.Count; i++) //iterate thru employees
        {
          SKImage photo = SKImage.FromEncodedData(await client.GetStreamAsync(employees[i].GetPhotoUrl())); // Get photo from URL and convert to SKImage object
          SKImage background = SKImage.FromEncodedData(File.OpenRead("badge.png")); // Load background image from file and convert to SKImage object

          // Create a new bitmap
          SKBitmap badge = new SKBitmap(BADGE_WIDTH, BADGE_HEIGHT); // Create a new bitmap with the dimensions of the badge
          SKCanvas canvas = new SKCanvas(badge); // Create a new canvas to draw on the bitmap (badge)

          // Draw background
          canvas.DrawImage(background, new SKRect(0, 0, BADGE_WIDTH, BADGE_HEIGHT)); // Draw the background image on the canvas
          canvas.DrawImage(photo, new SKRect(PHOTO_LEFT_X, PHOTO_TOP_Y, PHOTO_RIGHT_X, PHOTO_BOTTOM_Y)); // Draw the photo on the canvas

          // Company name
          SKPaint paint = new SKPaint(); // Create a new paint object
          paint.TextSize = 42.0f; // Set the font size with float
          paint.IsAntialias = true; // Set anti-aliasing to true
          paint.Color = SKColors.White; // Set the color to white
          paint.IsStroke = false; // Set stroke to false
          paint.TextAlign = SKTextAlign.Center; // Set the text alignment to center
          paint.Typeface = SKTypeface.FromFamilyName("Arial"); // Set the font to Arial

          // Draw company name
          canvas.DrawText(employees[i].GetCompanyName(), BADGE_WIDTH / 2f, COMPANY_NAME_Y, paint); // Draw the company name on the canvas with the paint object we just created and the font size we set above (42.0f)

          // Employee name
          paint.Color = SKColors.Black; // Set the color to black for the employee name text (the company name text is already white)
          canvas.DrawText(employees[i].GetFullName(), BADGE_WIDTH / 2f, EMPLOYEE_NAME_Y, paint); // Draw the employee name on the canvas with the paint object we just created and the font size we set above (42.0f)

          // Employee ID
          paint.Typeface = SKTypeface.FromFamilyName("Courier New"); // Set the font to Courier New for the employee ID text (the company name and employee name text are already Arial)
          canvas.DrawText(employees[i].GetId().ToString(), BADGE_WIDTH / 2f, EMPLOYEE_ID_Y, paint); // Draw the employee ID on the canvas with the paint object we just created and the font size we set above (42.0f)

          // Save the bitmap to a file using the SKImage API (https://docs.microsoft.com/en-us/dotnet/api/skiasharp.skimage?view=skiasharp-1.68.0)
          SKImage finalImage = SKImage.FromBitmap(badge); // Convert the bitmap to an SKImage object so we can save it to a file later on (the bitmap is the final product)
          SKData data = finalImage.Encode(); // Encode the SKImage object to a SKData object so we can save it to a file later on (the data is the final product)

          // Save the file to disk using the SKData API (https://docs.microsoft.com/en-us/dotnet/api/skiasharp.skdata?view=skiasharp-1.68.0)
          string template = "data/{0}_badge.png"; // Create a template for the file name (the file name will be the employee ID followed by "_badge.png")
          data.SaveTo(File.OpenWrite(string.Format(template, employees[i].GetId()))); // Save the data to a file with the file name we just created (the file name will be the employee ID followed by "_badge.png")
        }
      }
    }

  }
}