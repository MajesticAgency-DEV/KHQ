namespace KHQ.Domain.DTOs
{
    public class E_Con_InnerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
    }
    public class Statistics
    {
        public List<E_Con_InnerDto> statistics { get; set; }
        public string Main_Title { get; set; }
        public string Main_Description { get; set; }
    }
}
