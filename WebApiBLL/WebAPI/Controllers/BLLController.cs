using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace yezhanbafang.sd.WebAPI.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BLLController : ControllerBase
    {
        //创建日志记录组件实例
        static ILog _ydhlog = null;
        private static log4net.Repository.ILoggerRepository LoggerRepository;
        public static ILog Ydhlog
        {
            get
            {
                if (_ydhlog == null)
                {
                    LoggerRepository = LogManager.CreateRepository("Log4netConsolePractice");
                    log4net.Config.XmlConfigurator.ConfigureAndWatch(LoggerRepository, new FileInfo("log4netConfig.xml"));
                    _ydhlog = LogManager.GetLogger(LoggerRepository.Name, typeof(Program));
                }
                return _ydhlog;
            }
        }

        // POST: api/BLL
        [HttpPost]
        public BllClass Post(BllClass bcin)
        {
            try
            {

                string sin = JsonConvert.SerializeObject(bcin);
                if (bcin.IsLog)
                {
                    Ydhlog.Info("入参:");
                    Ydhlog.Info(sin);
                }
                Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + bcin.DLLName); // 获取当前程序集 
                object obj = assembly.CreateInstance(bcin.ClassName);
                object obse = obj.GetType().GetMethod(bcin.Function, new Type[] { typeof(string) }).Invoke(obj, new object[] { sin });
                if (bcin.IsLog)
                {
                    Ydhlog.Info("出参:");
                    Ydhlog.Info(obse.ToString());
                }
                BllClass bcout = JsonConvert.DeserializeObject<BllClass>(obse.ToString());
                if (!bcout.IsNormal)
                {
                    Ydhlog.Error("可预期的错误:");
                    Ydhlog.Error(bcout.ErrorMsg);
                }
                return bcout;
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + " ------> ";
                    me = me.InnerException;
                }
                exmsg += me.Message;

                Ydhlog.Error(exmsg);

                BllClass bc = new BllClass();
                bc.IsNormal = false;
                bc.ErrorMsg = exmsg;
                return bc;
            }
        }
    }
}
