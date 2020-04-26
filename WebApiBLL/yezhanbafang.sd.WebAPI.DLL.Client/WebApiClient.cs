using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using yezhanbafang.sd.MSSQL;

namespace yezhanbafang.sd.WebAPI
{
    /// <summary>
    /// 20200426 取消加密的方式,外层的https的方式本身就加密了,不用额外再搞了.
    /// </summary>
    class WebApiClient
    {
        string Url { get; set; }
        string ConfigPth { get; set; }
        public WebApiClient( string url,string configpath)
        {
            this.Url = url;
            this.ConfigPth = configpath;
        }

        /// <summary>
        /// 异步方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="RouteName">路由名称</param>
        /// <returns></returns>
        public async Task<BllClass> CallWebAPI_Async(string sql, string RouteName)
        {
            BllClass bc = new BllClass();
            bc.RouteName = RouteName;
            DLLjson dj = new DLLjson();
            dj.SQL_string = sql;
            dj.ConfigPath = this.ConfigPth;
            bc.JsonIn = JsonConvert.SerializeObject(dj);
            string rts = await CallWebAPI_Async(bc);
            BllClass bcrt = JsonConvert.DeserializeObject<BllClass>(rts);
            if (!bcrt.IsNormal)
            {
                throw new Exception(bcrt.ErrorMsg);
            }
            return bcrt;
        }

        /// <summary>
        /// 异步方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cuser">操作者</param>
        /// <param name="RouteName">路由名称</param>
        /// <returns></returns>
        public async Task<BllClass> CallWebAPI_Async(string sql, string cuser, string RouteName)
        {
            BllClass bc = new BllClass();
            bc.RouteName = RouteName;
            DLLjson dj = new DLLjson();
            dj.SQL_string = sql;
            dj.ConfigPath = this.ConfigPth;
            dj.Operater = cuser;
            bc.JsonIn = JsonConvert.SerializeObject(dj);
            string rts = await CallWebAPI_Async(bc);
            BllClass bcrt = JsonConvert.DeserializeObject<BllClass>(rts);
            if (!bcrt.IsNormal)
            {
                throw new Exception(bcrt.ErrorMsg);
            }
            return bcrt;
        }

        /// <summary>
        /// 异步方法
        /// </summary>
        /// <param name="SPname">存储过程名称</param>
        /// <param name="LD">参数集合</param>
        /// <param name="RouteName">路由名称</param>
        /// <returns></returns>
        public async Task<BllClass> CallWebAPI_Async(string SPname,  List<DbParameter> LD, string RouteName)
        {
            List<IoRyDbParameter> li = new List<IoRyDbParameter>();
            foreach (var item in LD)
            {
                IoRyDbParameter ip = new IoRyDbParameter();
                ip.Name = item.ParameterName;
                ip.Value = Convert.ToString(item.Value);
                li.Add(ip);
            }
            BllClass bc = new BllClass();
            bc.RouteName = RouteName;
            DLLjson dj = new DLLjson();
            dj.SQL_string = SPname;
            dj.ConfigPath = this.ConfigPth;
            dj.DbParaList = li;
            bc.JsonIn = JsonConvert.SerializeObject(dj);
            string rts = await CallWebAPI_Async(bc);
            BllClass bcrt = JsonConvert.DeserializeObject<BllClass>(rts);
            if (!bcrt.IsNormal)
            {
                throw new Exception(bcrt.ErrorMsg);
            }
            return bcrt;
        }

        /// <summary>
        /// 异步基础方法
        /// </summary>
        /// <param name="bcin">入参</param>
        /// <returns></returns>
        async Task<string> CallWebAPI_Async(BllClass bcin)
        {
            using (var client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(bcin));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync(this.Url, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        /// <summary>
        /// 同步调用方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="RouteName">路由名称</param>
        /// <returns></returns>
        public BllClass CallWebAPI_Syn(string sql, string RouteName)
        {
            BllClass bc = new BllClass();
            bc.RouteName = RouteName;
            DLLjson dj = new DLLjson();
            dj.SQL_string = sql;
            dj.ConfigPath = this.ConfigPth;
            bc.JsonIn = JsonConvert.SerializeObject(dj);
            string rts = CallWebAPI_Syn(bc);
            BllClass bcrt = JsonConvert.DeserializeObject<BllClass>(rts);
            if (!bcrt.IsNormal)
            {
                throw new Exception(bcrt.ErrorMsg);
            }
            return bcrt;
        }
        /// <summary>
        /// 同步调用方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cuser">操作者</param>
        /// <param name="RouteName">路由名称</param>
        /// <returns></returns>
        public BllClass CallWebAPI_Syn(string sql, string cuser, string RouteName)
        {
            BllClass bc = new BllClass();
            bc.RouteName = RouteName;
            DLLjson dj = new DLLjson();
            dj.SQL_string = sql;
            dj.ConfigPath = this.ConfigPth;
            dj.Operater = cuser;
            bc.JsonIn = JsonConvert.SerializeObject(dj);
            string rts = CallWebAPI_Syn(bc);
            BllClass bcrt = JsonConvert.DeserializeObject<BllClass>(rts);
            if (!bcrt.IsNormal)
            {
                throw new Exception(bcrt.ErrorMsg);
            }
            return bcrt;
        }

        /// <summary>
        /// 同步调用方法
        /// </summary>
        /// <param name="SPname">存储过程名称</param>
        /// <param name="LD">参数集合</param>
        /// <param name="RouteName">路由名称</param>
        /// <returns></returns>
        public BllClass CallWebAPI_Syn(string SPname, List<DbParameter> LD, string RouteName)
        {
            List<IoRyDbParameter> li = new List<IoRyDbParameter>();
            foreach (var item in LD)
            {
                IoRyDbParameter ip = new IoRyDbParameter();
                ip.Name = item.ParameterName;
                ip.Value = Convert.ToString(item.Value);
                li.Add(ip);
            }
            BllClass bc = new BllClass();
            bc.RouteName = RouteName;
            DLLjson dj = new DLLjson();
            dj.SQL_string = SPname;
            dj.ConfigPath = this.ConfigPth;
            dj.DbParaList = li;
            bc.JsonIn = JsonConvert.SerializeObject(dj);
            string rts = CallWebAPI_Syn(bc);
            BllClass bcrt = JsonConvert.DeserializeObject<BllClass>(rts);
            if (!bcrt.IsNormal)
            {
                throw new Exception(bcrt.ErrorMsg);
            }
            return bcrt;
        }

        /// <summary>
        /// 同步基础方法
        /// </summary>
        /// <param name="bcin"></param>
        /// <returns></returns>
        string CallWebAPI_Syn(BllClass bcin)
        {
            using (var client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(bcin));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync(this.Url, content).Result;
                response.EnsureSuccessStatusCode();//用来抛异常的
                string responseBody = response.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
        }
    }
}
