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
    public class subquestionsController : Controller
    {
        private readonly askfmContext db = new askfmContext();
        public IActionResult Index(int id)
        {
            var new_subquestion = new sub_question();
            //-------------------------TARGET QUESTION---------------------------------------\\
            var target_question = db.Questions.Where(x => x.Id == id).Select(x =>
                  new
                  {
                      x.Id,
                      x.Description
                  }
            ).FirstOrDefault();
            new_subquestion.question_id = target_question.Id;
            new_subquestion.question_description = target_question.Description;

            //-------------------------MAIN ANSWER---------------------------------------\\
            var main_answer = db.Answers.Where(x => x.QestionId == id && x.Main == true).Select(x=>
                new {
                    x.Description
                }
                ).FirstOrDefault();
            new_subquestion.main_answer = main_answer.Description;

            //-------------------------SUB ANSWERS---------------------------------------\\
            var all_answer = db.Answers.Where(x => x.QestionId == id && x.Main == false).Select(x =>
                      new
                      {
                          x.Description
                      }
            ).ToList();
            foreach(var item in all_answer)
            {
                if(item.Description!=null)
                {
                    new_subquestion.answers.Add(item.Description);
                }
               
            }

           
            return View(new_subquestion);
        }

        public IActionResult create(sub_question sub_Question)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var new_answer = new Answer();
            new_answer.QestionId = sub_Question.question_id;
            new_answer.Description = sub_Question.ask_descripion;
            new_answer.Date = DateTime.Now;
            new_answer.FromUserId = userId;
            new_answer.Main = false;
            db.Answers.Add(new_answer);
            db.SaveChanges();
            return Redirect("/subquestions/index/" + sub_Question.question_id);  
        }
    }
}
