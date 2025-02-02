namespace Hogarth.UserManagement.Application.DTOs.Common
{
    public class PaginationRequestDto
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string? searchValue { get; set; }
        public string? orderBy { get; set; }
        public bool isAscending { get; set; } = true;
    }
}
