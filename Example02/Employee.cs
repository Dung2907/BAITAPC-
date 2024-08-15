namespace Example02;

public class Employee
{
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string EmailId { get; set; }
    public Employee()
    {
        LastName = "";
        FirstName = "";
        EmailId = "";
        
    }
}