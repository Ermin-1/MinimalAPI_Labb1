using Microsoft.AspNetCore.Mvc;

namespace Web_Book.Controllers
{
	public class BookController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
