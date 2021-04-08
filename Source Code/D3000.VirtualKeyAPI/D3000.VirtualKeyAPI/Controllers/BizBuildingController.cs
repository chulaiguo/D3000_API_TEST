using System.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using D3000.VirtualKey;

namespace D3000.VirtualKeyAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BizBuildingController : ControllerBase
	{
		[HttpPost]
		public IActionResult GetBuildingList()
		{
			var __authorization__ = this.Request.Headers["Authorization"];

			string __options__;
			using (var reader = new StreamReader(this.Request.Body))
			{
				__options__ = reader.ReadToEndAsync().Result;
			}

			string __json__ = new BizBuilding().GetBuildingList(__authorization__, __options__);
			return Content(__json__, "application/json", Encoding.UTF8);
		}
	}

}
