namespace AttendanceSystem.Data.DomainEntities
{
    public class Attendance
    {
        public required DateTime UtcDateTime { get; set; }
        public int AttendanceId { get; set; }
        public Boolean status { get; set; } = true;
        public required int EmployeeId { get; set; }
    }
}
