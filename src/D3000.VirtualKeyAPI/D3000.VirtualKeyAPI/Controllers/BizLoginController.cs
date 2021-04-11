using System.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using D3000.VirtualKey;

namespace D3000.VirtualKeyAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BizLoginController : ControllerBase
	{
		[HttpPost]
		public IActionResult Login()
		{
			var __authorization__ = this.Request.Headers["x-auth-token"];

			string __options__;
			using (var reader = new StreamReader(this.Request.Body))
			{
				__options__ = reader.ReadToEndAsync().Result;
			}

			return Ok(new BizLogin().Login(__authorization__, __options__));
		}
	}

}
