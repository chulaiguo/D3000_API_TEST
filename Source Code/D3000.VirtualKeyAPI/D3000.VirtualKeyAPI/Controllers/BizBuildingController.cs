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
		public IActionResult GetAllList()
		{
			var __authorization__ = this.Request.Headers["Authorization"];

			string __options__;
			using (var reader = new StreamReader(this.Request.Body))
			{
				__options__ = reader.ReadToEndAsync().Result;
			}

			return Ok(new BizBuilding().GetAllList(__authorization__, __options__));
		}
		[HttpPost]
		public IActionResult GetByAddress()
		{
			var __authorization__ = this.Request.Headers["Authorization"];

			string __options__;
			using (var reader = new StreamReader(this.Request.Body))
			{
				__options__ = reader.ReadToEndAsync().Result;
			}

			return Ok(new BizBuilding().GetByAddress(__authorization__, __options__));
		}
		[HttpPost]
		public IActionResult Insert()
		{
			var __authorization__ = this.Request.Headers["Authorization"];

			string __options__;
			using (var reader = new StreamReader(this.Request.Body))
			{
				__options__ = reader.ReadToEndAsync().Result;
			}

			return Ok(new BizBuilding().Insert(__authorization__, __options__));
		}
		[HttpPost]
		public IActionResult InsertWithChildren()
		{
			var __authorization__ = this.Request.Headers["Authorization"];

			string __options__;
			using (var reader = new StreamReader(this.Request.Body))
			{
				__options__ = reader.ReadToEndAsync().Result;
			}

			return Ok(new BizBuilding().InsertWithChildren(__authorization__, __options__));
		}
		[HttpPost]
		public IActionResult Update()
		{
			var __authorization__ = this.Request.Headers["Authorization"];

			string __options__;
			using (var reader = new StreamReader(this.Request.Body))
			{
				__options__ = reader.ReadToEndAsync().Result;
			}

			return Ok(new BizBuilding().Update(__authorization__, __options__));
		}
	}

}
