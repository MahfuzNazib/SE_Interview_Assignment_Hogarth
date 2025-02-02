namespace Hogarth.UserManagement.Application.Helpers
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public T? Values { get; set; }
        public PaginationSummary? PaginationSummary { get; set; }
    }
}
