using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Shop.Repository;

namespace Web_Shop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OrderController : Controller
	{
		private readonly DataContext _dataContext;
		public OrderController(DataContext contcext) {
			_dataContext = contcext;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _dataContext.Orders.OrderByDescending(p => p.Id).ToListAsync()); ;
		}
        public async Task<IActionResult> ViewOrder(string ordercode)
        {
			var DetailsOrder = await _dataContext.OrderDetails.Include(od =>od.Product).Where(od => od.OrderCode == ordercode).ToListAsync();
			return View(DetailsOrder);
        }
    }
}
