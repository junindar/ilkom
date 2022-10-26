namespace MudBlazor.Input.Data.ViewModel
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string NamaCategory { get; set; }
        public List<Book> Books { get; set; }
    }
}
