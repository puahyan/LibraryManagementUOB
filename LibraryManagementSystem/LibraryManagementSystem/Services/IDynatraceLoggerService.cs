namespace LibraryManagementSystem.Services
{
    public interface IDynatraceLoggerService
    {
        Task LogAsync(string message, object attributes = null);
    }
}
