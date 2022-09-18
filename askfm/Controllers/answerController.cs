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
    public class answerController : Controller
    {
        private readonly askfmContext db = new askfmContext();
        public IActionResult Index(int id)
        {
            var target_question = db.Questions.Where(x => x.Id == id).Select(x =>
                  new answer
                  {
                      question = x.Description,
                      question_id=x.Id
                  }

                ).FirstOrDefault();
            return View(target_question);
        }
        public IActionResult create(answer answer)
        {
            var target_question = db.Questions.Where(x => x.Id == answer.question_id).FirstOrDefault();
            target_question.Reply = true;
            db.Questions.Update(target_question);
            db.SaveChanges();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var new_answer = new Answer();
            new_answer.Description= answer.description;
            new_answer.QestionId= answer.question_id;
            new_answer.Date = DateTime.Now;
            new_answer.Main = true;
            new_answer.FromUserId = userId;

            db.Answers.Add(new_answer);
            db.SaveChanges();
            return Redirect("/home/index");
        }
    }
}
