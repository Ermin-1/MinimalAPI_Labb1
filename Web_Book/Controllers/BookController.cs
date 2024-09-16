using Library_API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_Book.Models;
using Web_Book.Services;

namespace Web_Book.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
       public async Task<IActionResult> BookIndex()
        {
            List<BookDTO> list = new List<BookDTO>();
            var response = await _bookService.GetAllBooks<ResponseDTO>();

            if (response != null && response.IsSuccess)
            {
				list = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result));
			}

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            BookDTO dto = new BookDTO();

            var response = await _bookService.GetBookById<ResponseDTO>(id);

            if (response != null && response.IsSuccess)
            {
                BookDTO model = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
				return View(model);
			}

			return View();
		}

		public async Task<IActionResult> BookCreate()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> BookCreate(BookDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _bookService.CreateBookAsync<ResponseDTO>(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(BookIndex));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> UpdateBook(int id)
        {
            var response = await _bookService.GetBookById<ResponseDTO>(id);

			if (response != null && response.IsSuccess)
			{
				BookDTO model = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
				return View(model);
			}
			return NotFound();
		}


		[HttpPost]
		public async Task<IActionResult> UpdateBook(BookDTO model)
		{
			if (ModelState.IsValid)
			{
				var response = await _bookService.UpdateBookAsync<ResponseDTO>(model);

				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(BookIndex));
				}
			}

			return View(model);
		}

		public async Task<IActionResult> DeleteBook(int id)
		{
			var response = await _bookService.GetBookById<ResponseDTO>(id);

			if (response != null && response.IsSuccess)
			{
				BookDTO model = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
				return View(model);
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> DeleteBook(BookDTO model)
		{
			if (ModelState.IsValid)
			{
				var response = await _bookService.DeleteBookAsync<ResponseDTO>(model.Id);

				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(BookIndex));
				}

			}
			return View(model);

		}
	}
}
