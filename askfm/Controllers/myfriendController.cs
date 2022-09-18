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
    public class myfriendController : Controller
    {
        private readonly askfmContext db = new askfmContext();
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var target_followers = (from u in db.AspNetUsers
                              join f in db.Followers on u.Id equals f.ChildId
                         
                              where u.Id == userId
                              select new user
                              {
                                 name= u.UserName,
                                 id=u.Id

                              }).ToList();
            ///////////////////////////////////////////////////////////////////////////////////////////
            var all = new friends();
            foreach (var item in target_followers)
            {
                all.friend.Add(item);
            }

            var target_recommendtion = db.AspNetUsers.Where(x => x.Id!= userId).Select(x =>
               new user
               {
                   name = x.UserName,
                   
                   id = x.Id

               }
           ).ToList();

            foreach(var item in target_recommendtion)
            {
                all.recommedtion.Add(item);
            }
            return View(all);
           
        }
    }
}
