namespace CatWorx.BadgeMaker
{
  class Employee
  {
    private string FirstName; // Private field for first name
    private string LastName; // Private field for last name
    private int Id; // Private field for ID
    private string PhotoUrl; // Private field for photo URL
    public Employee(string firstName, string lastName, int id, string photoUrl) //Constructor for Employee
    {
      FirstName = firstName; // Set first name to the value passed in the constructor argument firstName
      LastName = lastName; // Set last name to the value passed in the constructor argument lastName
      Id = id; // Set ID to the value passed in the constructor argument id
      PhotoUrl = photoUrl; // Set photo URL to the value passed in the constructor argument photoUrl
    }
    public string GetFullName() //Method to get the full Name using the first and last name
    {
      return FirstName + " " + LastName; // Return the full name
    }
    public int GetId() //Method to get the ID integer number
    {
      return Id; // Return the ID
    }
    public string GetPhotoUrl() //Method to get the photo URL
    {
      return PhotoUrl; // Return the photo URL
    }
    public string GetCompanyName() //Method to get the Company Name
    {
      return "Cat Worx"; // Return the company name
    }
  }
}