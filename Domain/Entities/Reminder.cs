namespace DepartmentHierarchyAndReminderManagementSystem.Domain.Entities
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReminderDateTime { get; set; }
        public bool EmailSent { get; set; } = false;
        public string Email { get; set; }
    }
}
