using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace D3000.VirtualKeyAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UploadController : ControllerBase
	{
		private readonly IWebHostEnvironment _environment;

		public UploadController(IWebHostEnvironment environment)
		{
			this._environment = environment;
		}

		public IActionResult Upload()
		{
			int count = 0;
			var files = this.Request.Form.Files;
			foreach (var file in files)
			{
				var fileType = Path.GetExtension(file.FileName);
				if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".png" ||
					fileType.ToLower() == ".jpeg")
				{
					if (!Directory.Exists(this._environment.WebRootPath + "\\upload"))
					{
						Directory.CreateDirectory(this._environment.WebRootPath + "\\upload");
					}

					count++;
					string filePath = _environment.WebRootPath + "\\upload" + file.FileName;
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						file.CopyTo(stream);
					}
				}
			}

			return Ok(new { ok = true, count });
		}
	}
}
