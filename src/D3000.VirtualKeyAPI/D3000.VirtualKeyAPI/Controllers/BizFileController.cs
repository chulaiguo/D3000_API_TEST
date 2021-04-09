using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace D3000.VirtualKeyAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BizFileController : ControllerBase
	{
		private readonly IWebHostEnvironment _environment;

		public BizFileController(IWebHostEnvironment environment)
		{
			this._environment = environment;
		}

		[HttpPost]
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
					if (!Directory.Exists(this._environment.ContentRootPath + "\\upload"))
					{
						Directory.CreateDirectory(this._environment.ContentRootPath + "\\upload");
					}

					count++;
					string filePath = _environment.ContentRootPath + "\\upload\\" + file.FileName;
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						file.CopyTo(stream);
					}
				}
			}

			return Ok(new { ok = true, message = $"Upload {count} files" });
		}

		public IActionResult Download(string fileName)
		{
			if (!Directory.Exists(this._environment.ContentRootPath + "\\download"))
			{
				return NotFound();
			}

			string filePath = this._environment.ContentRootPath + "\\download\\" + fileName;
			if (!System.IO.File.Exists(filePath))
			{
				return NotFound();
			}

			byte[] data = System.IO.File.ReadAllBytes(filePath);

			var provider = new FileExtensionContentTypeProvider();
			if (!provider.TryGetContentType(fileName, out var contentType))
			{
				contentType = "application/octet-stream";
			}
			return File(data, contentType, fileName);
		}
	}
}
