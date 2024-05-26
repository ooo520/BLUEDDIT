using bluedit.DataAccess;
using bluedit.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bluedit.Controllers
{
	[Route("api/[controller]")]
	public class OpinionController : Controller
	{
		private readonly IOpinionRepository _opinionRepository;
		private readonly IUserRepository _userRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public OpinionController(IOpinionRepository opinionRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
		{
			_opinionRepository = opinionRepository;
			_userRepository = userRepository;
			_httpContextAccessor = httpContextAccessor;
		}

		public string username => _httpContextAccessor.HttpContext.Request.Cookies["username"];
		public string userpass => _httpContextAccessor.HttpContext.Request.Cookies["userpass"];

		[HttpPost("upvote/{answerId}")]
		public async Task<IActionResult> Upvote(long answerId)
		{
			if (!IsLoggedIn())
			{
				return BadRequest();
			}

			return Ok(await SubmitOpinion(answerId, true));
		}

		[HttpPost("downvote/{answerId}")]
		public async Task<IActionResult> Downvote(long answerId)
		{
			if (!IsLoggedIn())
			{
				return BadRequest();
			}

			return Ok(await SubmitOpinion(answerId, false));
		}

		private async Task<int> SubmitOpinion(long answerId, bool vote)
		{
			long userId = _userRepository.GetByName(username).Id;    // user should not be null

			Dbo.Opinion opinion = _opinionRepository.GetUserOpinionOnAnswer(userId, answerId);
			Dbo.Opinion newOpinion = new()
			{
				AuthorId = userId,
				AnswerId = answerId,
				Like = vote
			};

			if (opinion == null)
			{
				await _opinionRepository.Create(newOpinion);
				return vote ? 1 : -1;
			}
			else if (opinion.Like == vote)
			{
				await _opinionRepository.Delete(opinion.Id);
				return vote ? -1 : 1;
			}
			else
			{
				opinion.Like = vote;
				await _opinionRepository.Update(opinion);
				return vote ? 2 : -2;
			}
		}

		private bool IsLoggedIn()
		{
			return !(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userpass) || (_userRepository.GetByName(username)?.Password != userpass));
		}
	}
}
