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
	public class questionController : Controller
	{
		private readonly askfmContext db = new askfmContext();
		public IActionResult Index(profile profile)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			//Console.WriteLine(profile.ask_Qes.descritption);
			//Console.WriteLine(profile.user.id);
			//Console.WriteLine(profile.ask_Qes.unknown);
			//Console.WriteLine(DateTime.Now);
			//Console.WriteLine(DateTime.UtcNow);

			var new_question = new Question();

			if (profile.ask_Qes.unknown == "YES")
			{
				new_question.Unknown = true;
			}
			else
			{
				new_question.Unknown = false;
			}
			new_question.Description = profile.ask_Qes.descritption;
			new_question.ToUserId = profile.user.id;
			new_question.FromUserId = userId;
			new_question.Date = DateTime.Now;
			new_question.Reply = false;
			new_question.Likes = 0;
			db.Questions.Add(new_question);
			db.SaveChanges();
			return Redirect("/home/index");
		}
	}
}
