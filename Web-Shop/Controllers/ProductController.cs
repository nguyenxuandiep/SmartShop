using Microsoft.AspNetCore.Mvc;
using Web_Shop.Repository;

namespace Web_Shop.Controllers
	
{
    public class ProductController : Controller
    {
		private readonly DataContext _dataContext;
		public ProductController(DataContext context)
		{
			_dataContext = context;
		}
		public IActionResult Index()
        {
            var products = _dataContext.Products.ToList();
            return View(products);
        }

		public IActionResult Details(int Id )
		{
			if (Id == null)
			{
				return RedirectToAction("Index");
			}
			var productsById = _dataContext.Products.Where(p => p.Id == Id).FirstOrDefault();
			return View(productsById);
		}
	}
}
