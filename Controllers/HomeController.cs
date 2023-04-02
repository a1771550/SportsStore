using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 4;
        public HomeController(IStoreRepository repository)
        {
            this.repository = repository;
        }
        //public IActionResult Index(int productPage=1) => View(repository.Products.OrderBy(p=>p.ProductID).Skip((productPage-1)*PageSize).Take(PageSize));
        public ViewResult Index(string? category, int productPage = 1)
        {
            ViewData["Title"] = "Home";
            var filteredproducts = repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductID);
            return View(new ProductsListViewModel
            {
                Products = filteredproducts.Skip((productPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo { CurrentPage = productPage, ItemsPerPage = PageSize, TotalItems = filteredproducts.Count() },
                CurrentCategory = category
            });
        }
    }
}