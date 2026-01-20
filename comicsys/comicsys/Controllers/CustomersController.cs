using Microsoft.AspNetCore.Mvc;
using ComicSystem.Data;
using ComicSystem.Models;

namespace ComicSystem.Controllers
{
	public class CustomersController : Controller
	{
		private readonly ComicSystemContext _context;

		public CustomersController(ComicSystemContext context)
		{
			_context = context;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(Customer customer)
		{
			if (ModelState.IsValid)
			{
				customer.RegistrationDate = DateTime.Now;
				_context.Add(customer);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Home");
			}
			return View(customer);
		}
	}
}