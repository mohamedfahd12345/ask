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
    public class meController : Controller
    {
        private readonly  askfmContext db=new askfmContext();
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           // var user = new user();
            var target_user = db.AspNetUsers.Where(x=>x.Id==userId).Select(x =>
            new  user {
              name=  x.UserName,
               email= x.Email,
               linkedin_link= x.LinkedInLink,
              tweeter_link=  x.TweeterLink,
              cv_link=  x.CvLink,
               photo= x.Photo,
               facebook_link= x.FacebookLink,
               id=x.Id
               
            }
            ).FirstOrDefault();
            return View(target_user);
        }
        public IActionResult edit(string id)
        {
            var target_user = db.AspNetUsers.Where(x => x.Id == id).Select(x =>
                 new user
                 {
                     name = x.UserName,
                     email = x.Email,
                     linkedin_link = x.LinkedInLink,
                     tweeter_link = x.TweeterLink,
                     cv_link = x.CvLink,
                     photo = x.Photo,
                     facebook_link = x.FacebookLink,
                     id = x.Id
                 }
             ).FirstOrDefault();
            return View(target_user);
        }
        [HttpPost]
        public IActionResult edit(user user)
        {
            var target_user = db.AspNetUsers.Where(x => x.Id == user.id).FirstOrDefault();
            if (target_user == null)
                return NotFound();
            target_user.UserName = user.name;
            target_user.TweeterLink = user.tweeter_link;
            target_user.LinkedInLink = user.linkedin_link;
            target_user.CvLink = user.cv_link;
            target_user.Photo = user.photo;
            target_user.FacebookLink = user.facebook_link;
            db.AspNetUsers.Update(target_user);
            db.SaveChanges();
            return Redirect("/me/index");
        }
    }
}
