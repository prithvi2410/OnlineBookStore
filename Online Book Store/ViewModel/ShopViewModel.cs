using Online_Book_Store.Models;
using System.Collections.Generic;


namespace Online_Book_Store.ViewModel
{
    public class ShopViewModel 
    {
        public List<Book> Book { get; set; }
        public List<int> InCartBookId = new List<int>();
    }
}