using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using BlogProject.WebUI.Areas.Administrator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogProject.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator"), Authorize]
    public class CommentController : Controller
    {
        private readonly ICoreService<Comment> _commentService;
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<User> _userService;


        public CommentController(ICoreService<Comment> commentService, ICoreService<Post> postService, ICoreService<User> userService)
        {
            _commentService = commentService;
            _postService = postService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_commentService.GetAll(t0 =>t0.Kullanici, t2=>t2.Gonderi).ToList());
        }

        public IActionResult CommentDetail(Guid id)
        {
            CommentDetailVM vm = new CommentDetailVM();
            vm.Comment = _commentService.GetById(id);
            vm.User = _userService.GetById(_commentService.GetById(id).UserId);
            vm.Post = _postService.GetById(_commentService.GetById(id).PostId);

            return View(vm);
        }

        public IActionResult Activate(Guid id)
        {
            _commentService.Activate(id);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            _commentService.Remove(_commentService.GetById(id));
            return RedirectToAction("Index");
        }
    }
}
