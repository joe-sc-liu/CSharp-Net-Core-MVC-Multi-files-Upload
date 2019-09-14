using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace mvcFileUpload
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //Request Limits 檔案限制
        //上傳大檔可能還會遇到單一 Request 封包過大的錯誤。
        //Kestrel 若是將 ASP.NET Core 單獨運行在 Kestrel 上，預設單一上傳封包是 30,000,000 bytes 大約是 28.6MB，
        //單次 Request 上傳的大小限制可以在 KestrelServerLimits 修改 MaxRequestBodySize。如下：
        //Program.cs

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


                //以下限制，請勿在windows執行，因為windows用的是iis，Kestrel是linux在用的
                //.UseKestrel(o => {
                //    // 100MB
                //    o.Limits.MaxRequestBodySize = 100 * 1024 * 1024;
                //});
    }
}
