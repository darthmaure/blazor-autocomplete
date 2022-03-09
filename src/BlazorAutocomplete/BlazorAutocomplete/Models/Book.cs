namespace BlazorAutocomplete.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public IList<string> Tags { get; set; }
    }
}
