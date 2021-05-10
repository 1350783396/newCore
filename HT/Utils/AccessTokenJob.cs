using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HT.Utils
{
    public class AccessTokenJob : BackgroundService
    {
        //重写ExecuteAsync
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    WriteAsyncLog("aaa");
                    await Task.Delay(10000, stoppingToken); //7200秒执行一次 
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public static readonly object _lock = new object();
        /// <summary>
        /// 异步写日志
        /// </summary>
        /// <param name="strLog"></param>
        public static void WriteAsyncLog(string strLog)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(delegate (object obj)

            {
                lock (_lock)

                {
                    string sFilePath = "d:\\jcLog\\" + DateTime.Now.ToString("yyyyMM");
                    string sFileName = "rizhi" + DateTime.Now.ToString("dd") + ".log";
                    sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
                    if (!Directory.Exists(sFilePath))//验证路径是否存在
                    {
                        Directory.CreateDirectory(sFilePath);
                        //不存在则创建
                    }
                    FileStream fs;
                    StreamWriter sw;
                    if (System.IO.File.Exists(sFileName))
                    //验证文件是否存在，有则追加，无则创建
                    {
                        fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
                    }
                    else
                    {
                        fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
                    }
                    sw = new StreamWriter(fs);
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   " + strLog);
                    sw.Close();
                    fs.Close();
                }

            }));

            thread.Start(strLog);


        }

    }
}
