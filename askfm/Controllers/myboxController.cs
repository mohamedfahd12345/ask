using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Security.Claims;
using askfm.Models;
using askfm.helper;

namespace askfm.Controllers
{
    [Authorize]
    public class myboxController : Controller
    {
        private readonly askfmContext db = new askfmContext();
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var all_question = (from u in db.AspNetUsers
                                join q in db.Questions on  u.Id equals q.FromUserId

                                where q.ToUserId == userId &&q.Reply==false
                                select new show_question
                                {
                                    question=q.Description,
                                    question_id=q.Id,
                                    user_id=u.Id,
                                    user_name=u.UserName

                                }).ToList();
            return View(all_question);
        }
    }
}
