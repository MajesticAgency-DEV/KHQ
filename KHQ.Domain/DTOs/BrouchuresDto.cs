using KHQ.Domain.Entities;

namespace KHQ.Domain.DTOs
{
    public class BrouchuresDto
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
    public class BrouchuresDtoNew
    {
        public Guid Id { get; set; }
        public List<Brouchures> BrouchuresDto { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
