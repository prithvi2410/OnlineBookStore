using Online_Book_Store.Models;
using Online_Book_Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Book_Store.Controllers
{
    public class ShopController : Controller
    {
        private OnlineBookStoreContext database = new OnlineBookStoreContext();
        public ActionResult Home()
        {
            ShopViewModel shopViewModel = new ShopViewModel();
            shopViewModel.Book = database.Books.ToList();

            if (TempData["CurrentUserId"] == null)
            {
                List<CartItem> CurrentUserCartItems = new List<CartItem>();
                return View(shopViewModel);
            }
            else 
            {
                TempData.Keep();
                List<CartItem> CurrentUserCartItems = database.Carts.Find(Convert.ToInt32(TempData["CurrentUserId"])).CartItems.ToList();
                foreach (CartItem item in CurrentUserCartItems)
                {
                    shopViewModel.InCartBookId.Add(item.BookId);
                }
                return View(shopViewModel);
            }
        }

        public ActionResult Category(String category)
        {
            ViewBag.Category = category;
            ShopViewModel shopViewModel = new ShopViewModel();
            shopViewModel.Book = new List<Book>();
            foreach (Book item in database.Books)
            {
                if (item.Category == category)
                    shopViewModel.Book.Add(item);
            }

            if (TempData["CurrentUserId"] == null)
            {
                List<CartItem> CurrentUserCartItems = new List<CartItem>();
                return View(shopViewModel);
            }
            else
            {
                TempData.Keep();
                List<CartItem> CurrentUserCartItems = database.Carts.Find(Convert.ToInt32(TempData["CurrentUserId"])).CartItems.ToList();
                foreach (CartItem item in CurrentUserCartItems)
                {
                    shopViewModel.InCartBookId.Add(item.BookId);
                }
                return View(shopViewModel);
            }
        }
        public ActionResult Search(String searched)
        {
            if (searched == null) searched = string.Empty;
            ViewBag.Searched = searched;
            ShopViewModel shopViewModel = new ShopViewModel();

            shopViewModel.Book = new List<Book>();
            foreach (Book item in database.Books)
            {
                if (item.Name.ToLower().Contains(searched.ToLower()))
                    shopViewModel.Book.Add(item);
            }

            if (TempData["CurrentUserId"] == null)
            {
                List<CartItem> CurrentUserCartItems = new List<CartItem>();
                return View(shopViewModel);
            }
            else
            {
                TempData.Keep();
                List<CartItem> CurrentUserCartItems = database.Carts.Find(Convert.ToInt32(TempData["CurrentUserId"])).CartItems.ToList();
                foreach (CartItem item in CurrentUserCartItems)
                {
                    shopViewModel.InCartBookId.Add(item.BookId);
                }
                return View(shopViewModel);
            }
        }
    }
}