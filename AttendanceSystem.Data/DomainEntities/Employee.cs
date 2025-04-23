namespace AttendanceSystem.Data.DomainEntities
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
    }
}
