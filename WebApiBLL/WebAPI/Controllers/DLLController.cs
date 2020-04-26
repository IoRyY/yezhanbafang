using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using yezhanbafang.sd.MSSQL;

namespace yezhanbafang.sd.WebAPI.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DLLController : ControllerBase
    {
        // POST: api/DLL
        [HttpPost]
        public BllClass Post(BllClass bcin)
        {
            try
            {
                DLLjson dj = JsonConvert.DeserializeObject<DLLjson>(bcin.JsonIn);
                IoRyClass ic = new IoRyClass(dj.ConfigPath);
                DataSet ds = null;
                byte[] mbs = null;
                string StringDataSet = null;
                switch (bcin.RouteName)
                {
                    case "GetDataSet":
                        ds = ic.GetDataSet(dj.SQL_string);
                        mbs = IoRyClass.GetXmlFormatDataSet(ds);
                        StringDataSet = IoRyClass.BytesToString(mbs);
                        //去掉xml的声明,因为外层还有一个xml
                        StringDataSet = StringDataSet.Replace("<?xml version=\"1.0\"?>", "");
                        bcin.JsonOut = StringDataSet;
                        return bcin;
                    case "GetDataSet_Log":
                        ds = ic.Log_GetDataSet(dj.SQL_string, dj.Operater);
                        mbs = IoRyClass.GetXmlFormatDataSet(ds);
                        StringDataSet = IoRyClass.BytesToString(mbs);
                        //去掉xml的声明,因为外层还有一个xml
                        StringDataSet = StringDataSet.Replace("<?xml version=\"1.0\"?>", "");
                        bcin.JsonOut = StringDataSet;
                        return bcin;
                    case "ExcutSqlTran":
                        StringDataSet = ic.ExecuteSqlTran(dj.SQL_string);
                        bcin.JsonOut = StringDataSet;
                        return bcin;
                    case "ExcutSqlTran_Log":
                        StringDataSet = ic.Log_ExecuteSqlTran(dj.SQL_string, dj.Operater);
                        bcin.JsonOut = StringDataSet;
                        return bcin;
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
                        //去掉xml的声明,因为外层还有一个xml
                        StringDataSet = StringDataSet.Replace("<?xml version=\"1.0\"?>", "");
                        bcin.JsonOut = StringDataSet;
                        return bcin;
                    default:
                        bcin.IsNormal = false;
                        bcin.ErrorMsg = "找不到此Function";
                        return bcin;
                }
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
                bcin.IsNormal = false;
                bcin.ErrorMsg = exmsg;
                return bcin;
            }
        }
    }
}
