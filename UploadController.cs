using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public UploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public ActionResult Post(List<IFormFile>files)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "img", "Editor");
            string fNmae = Guid.NewGuid().ToString().Replace("_", "");
            var fileName = $"{path}/{fNmae}";
            try
            {
                var ext = DAL.Upload.Instance.UpImg(files[0], fileName);
                if (ext ==null)
                    return Json(Result.Err("请上传图片文件"));
                else
                {
                    var file = $"https://{HttpContext.Request.Host.Value}/img/Editor/{fNmae}{ext}";

                    return Json(Result.Ok("上传成功"));

                }

            }
            catch(Exception ex)
            {
                return Json(Result.Err(ex.Message));
            }
        }
    }
}
