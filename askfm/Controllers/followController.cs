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
    public class followController : Controller
    {
        private readonly askfmContext db = new askfmContext();
        public IActionResult Index(profile profile)
        {
            string id = profile.user.id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var target_follow = db.Followers.Where(x => x.FatherId == id && x.ChildId == userId).FirstOrDefault();
            if (target_follow == null)
            {
                var new_follow = new Follower();
                new_follow.FatherId = id;
                new_follow.ChildId = userId;
                db.Followers.Add(new_follow);
                db.SaveChanges();
            }
            else
            {
                db.Followers.Remove(target_follow);
                db.SaveChanges();
            }
            return Redirect("/home/index");
        }
    }
}
