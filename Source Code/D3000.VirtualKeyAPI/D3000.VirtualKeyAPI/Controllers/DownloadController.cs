using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;

namespace D3000.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class DownloadController : ControllerBase
	{
		private readonly IWebHostEnvironment _environment;

		public DownloadController(IWebHostEnvironment environment)
		{
			this._environment = environment;
		}

		public IActionResult Download(string fileName)
		{
			if (!Directory.Exists(this._environment.WebRootPath + "\\download"));
			{
				return NotFound();
			}

			string filePath = this._environment.WebRootPath + "\\download" + fileName;
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
