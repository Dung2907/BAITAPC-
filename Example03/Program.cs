namespace Example03
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeContext context = new EmployeeContext();

            Employee em = new Employee();
            em.FirstName = "Tu";
            em.LastName = "Nguyen";
            em.EmailId = "info@nguyenanhtu.com";
            context.Employees.Add(em);
            context.SaveChanges();

            em = new Employee();
            em.FirstName = "Phat";
            em.LastName = "Huynh";
            em.EmailId = "info@huynhtanphat.com";
            context.Employees.Add(em);
            context.SaveChanges();

            Console.WriteLine("-------Loading all data-------");
            List<Employee> employeeA = context.Employees.ToList();
            foreach (Employee empA in employeeA)
            {
                Console.WriteLine(empA.FirstName);
                Console.WriteLine(empA.LastName);
                Console.WriteLine(empA.EmailId);
            }

            Console.WriteLine("------Loading a single entity----------");
            Employee empB = context.Employees.Single(b => b.Id == 1);
            Console.WriteLine(empB.FirstName);
            Console.WriteLine(empB.LastName);
            Console.WriteLine(empB.EmailId);

            Console.WriteLine("-----Loading with Filtering------");
            List<Employee> employeeB = context.Employees.Where(b => b.FirstName.Contains("Tu")).ToList();
            foreach (Employee empC in employeeB)
            {
                Console.WriteLine(empC.FirstName);
                Console.WriteLine(empC.LastName);
                Console.WriteLine(empC.EmailId);
            }
        }
    }
}