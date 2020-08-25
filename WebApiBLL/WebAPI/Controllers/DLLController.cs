using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using yezhanbafang.sd.MSSQL;

namespace yezhanbafang.sd.WebAPI.Service.Controllers
{
    /// <summary>
    /// 20200426 关于使用XmlSerializer类对DataSet的序列化与反序列化的问题,找到了原因,
    /// webapi是用.netCore写的,在用XmlSerializer类序列化dataset的时候,GUID类型属于比较特殊的类,转换出来类型是这样的
    /// <xs:element name="UUID" msdata:DataType="System.Guid, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e" type="xs:string" minOccurs="0" />
    /// 而调用webapi的客户端是.net framework写的,对于它,在用XmlSerializer类序列化dataset的时候 GUID的类型是这样的
    /// <xs:element name="UUID" msdata:DataType="System.Guid, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
    /// 所以,一旦webapi的服务端和客户端一个是.net standard Core 一个是.net framework,而传输的dataset中又含有类似于上面GUID的特殊类型,就会造成类型不一致报错.
    /// 
    /// 另外的一个解决方案是利用json序列化反序列化datase,用Newtonsoft.Json做专门的dataset类型转换的时候,发现序列化的结果根本没有类型,所以想GUID这种类型反序列化后类型会变成string,也是非常不爽
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DLLController : ControllerBase
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

        // POST: api/DLL
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
                DLLjson dj = JsonConvert.DeserializeObject<DLLjson>(bcin.JsonIn);
                IoRyClass ic = new IoRyClass(dj.ConfigPath);
                DataSet ds = null;
                byte[] mbs = null;
                string StringDataSet = null;
                switch (bcin.RouteName)
                {
                    case "GetDataSet":
                        ds = ic.GetDataSet(dj.SQL_string);
                        //StringDataSet = JsonConvert.SerializeObject(ds, new Newtonsoft.Json.Converters.DataSetConverter());
                        mbs = IoRyClass.GetXmlFormatDataSet(ds);
                        StringDataSet = IoRyClass.BytesToString(mbs);
                        //StringDataSet = Convert.ToBase64String(mbs);

                        //byte[] mbs1 = IoRyClass.StringToBytes(StringDataSet);
                        //DataSet ds1 = IoRyClass.RetrieveXmlDataSet(mbs1);
                        //DataSet ds1 = JsonConvert.DeserializeObject<DataSet>(StringDataSet, new Newtonsoft.Json.Converters.DataSetConverter());

                        bcin.JsonOut = StringDataSet;
                        break;
                    case "GetDataSet_Log":
                        ds = ic.Log_GetDataSet(dj.SQL_string, dj.Operater);
                        mbs = IoRyClass.GetXmlFormatDataSet(ds);
                        StringDataSet = IoRyClass.BytesToString(mbs);
                        bcin.JsonOut = StringDataSet;
                        break;
                    case "ExcutSqlTran":
                        StringDataSet = ic.ExecuteSqlTran(dj.SQL_string);
                        bcin.JsonOut = StringDataSet;
                        break;
                    case "ExcutSqlTran_Log":
                        StringDataSet = ic.Log_ExecuteSqlTran(dj.SQL_string, dj.Operater);
                        bcin.JsonOut = StringDataSet;
                        break;
                    case "ExcutSP":
                        List<DbParameter> LD = new List<DbParameter>();
                        foreach (var item in dj.DbParaList)
                        {
                            //这里就先只写SqlServer的吧,其他的数据库类型,用到了再说
                            SqlParameter sp = new SqlParameter();
                            sp.Value = item.Value;
                            sp.ParameterName = item.Name;
                            LD.Add(sp);
                        }
                        ds = ic.ExecuteSP(dj.SQL_string, LD);
                        mbs = IoRyClass.GetXmlFormatDataSet(ds);
                        StringDataSet = IoRyClass.BytesToString(mbs);
                        bcin.JsonOut = StringDataSet;
                        break;
                    default:
                        bcin.IsNormal = false;
                        bcin.ErrorMsg = "找不到此RouteName";
                        break;
                }
                string sout = JsonConvert.SerializeObject(bcin);
                if (bcin.IsLog)
                {
                    Ydhlog.Info("出参:");
                    Ydhlog.Info(sout);
                }
                if (!bcin.IsNormal)
                {
                    Ydhlog.Error("可预期的错误:");
                    Ydhlog.Error(sout);
                }
                return bcin;
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "\r\n------>\r\n";
                    me = me.InnerException;
                }
                exmsg += me.Message;

                Ydhlog.Error(exmsg);

                bcin.IsNormal = false;
                bcin.ErrorMsg = exmsg;
                return bcin;
            }
        }
    }
}
