namespace Library_API.Models.DTOs
{
    public class UpdateBookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvalible  { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Description { get; set; }
    }
}
