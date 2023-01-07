using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebUI.Areas.Administrator.Controllers
{
	[Area("Administrator"), Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ICoreService<User> _userService;
		private readonly ICoreService<Post> _postService;
		private readonly ICoreService<Category> _catService;
		private readonly ICoreService<Comment> _commentService;

		public HomeController(ILogger<HomeController> logger, ICoreService<Category> catService, ICoreService<User> UserService, ICoreService<Post> postService, ICoreService<Comment> commentService)
		{
			_logger = logger;
			_catService = catService;
			_userService = UserService;
			_postService = postService;
			_commentService = commentService;
		}

		public IActionResult Index()
		{
			ViewBag.Kullanici = _userService.GetActive().Count;
			ViewBag.Category = _catService.GetActive().Count;
			ViewBag.Post = _postService.GetActive().Count;
			ViewBag.Comments = _commentService.GetActive().Count;
			return View();
		}
	}
}
