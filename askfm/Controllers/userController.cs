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
    public class userController : Controller
    {
        private readonly askfmContext db = new askfmContext();
        public IActionResult Index(string id)
        {
            var profile=new profile();
            //--------------------------ALL INFORMATION ABOUT THIS USER-----------------------------------\\
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
            profile.user=target_user;
            //----------------------------HIS QUESTION AND ANSWER-------------------------------------------\\
            var all_question = (from q in db.Questions
                                    join a in db.Answers on q.Id equals a.QestionId

                                    where q.ToUserId==id&&q.Reply==true &&a.Main==true
                                    select new temp_ques
                                    {
                                       id=q.Id,
                                       description_answer=a.Description,
                                       description_ask=q.Description

                                    }).ToList();
            foreach (var item in all_question)
            {
                profile.all_question.Add(item);
               
            }

			
            //-----------------------------ASK QUSETION ----------------------------------------------------\\

            return View(profile);
        }
    }
}
