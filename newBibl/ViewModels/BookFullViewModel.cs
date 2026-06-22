namespace newBibl.ViewModels
{
    public class BookFullViewModel
    {
        public string BookName { get; set; } = string.Empty;
        public int PageCount { get; set; }

        public string AuthorName { get; set; } = string.Empty;

        public string Editions { get; set; } = string.Empty;
    }
}