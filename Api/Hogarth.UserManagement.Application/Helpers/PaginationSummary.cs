
namespace Hogarth.UserManagement.Application.Helpers
{
    public class PaginationSummary
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalCount { get; set; }
        public int FirstPage { get; set; } = 1;

        // Computed property to dynamically calculate LastPage
        public int LastPage => PerPage > 0 ? (int)Math.Ceiling(TotalCount / (double)PerPage) : 1;

        public string? OrderBy { get; set; }
        public bool Ascending { get; set; } = true;
        public string? SearchParam { get; set; }
    }
}
