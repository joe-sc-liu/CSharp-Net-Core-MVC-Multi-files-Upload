using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using mvcFileUpload.Filters;
using mvcFileUpload.Helpers;
using mvcFileUpload.Models;

namespace mvcFileUpload.Controllers
{
    [Route("api/[controller]s")]
    public class FileController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        private readonly static Dictionary<string, string> _contentTypes = new Dictionary<string, string>
        {
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif", "image/gif"}
        };

        private readonly string _folder;

        public FileController(IHostingEnvironment env)
        {
            // 把上傳目錄設為：wwwroot\UploadFolder
            _folder = $@"{env.WebRootPath}\UploadFolder";
        }


        //DisableFormValueModelBindingFilter
        //Action 套用此 Filter 後，HTML Form 就不會被轉換成物件傳入 Action，因此也就可以移除 Action 的參數了。
        //StreamFile
        //StreamFile 會將 HTML Form 的內容以 FormValueProvider 包裝後回傳，並以委派方法讓你實做上傳的事件，以此例來說就是直接以串流的方式直接寫檔。
        //這樣就能避免 ASP.NET Core 依賴緩衝記憶體上傳檔案。

        [Route("album")]
        [HttpPost]
        [DisableFormValueModelBindingFilter]
        public async Task<IActionResult> Album()
        {
            var photoCount = 0;
            var formValueProvider = await Request.StreamFile((file) =>
            {
                photoCount++;
                return System.IO.File.Create($"{_folder}\\{file.FileName}");
            });

            var model = new AlbumModel
            {
                Title = formValueProvider.GetValue("title").ToString(),
                Date = Convert.ToDateTime(formValueProvider.GetValue("date").ToString())
            };

            // ...

            return Ok(new
            {
                title = model.Title,
                date = model.Date.ToString("yyyy/MM/dd"),
                photoCount = photoCount
            });
        }




    }
}