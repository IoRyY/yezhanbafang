using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace yezhanbafang.sd.WebAPI
{
    /// <summary>
    /// 传入传出的json
    /// </summary>
    public class BllClass
    {
        /// <summary>
        /// 要调用的DLL名称
        /// </summary>
        public string DLLName { get; set; }
        /// <summary>
        /// 要调用的类名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 要调用的方法名称
        /// </summary>
        public string Function { get; set; }
        /// <summary>
        /// 方法路由名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// Json的入参
        /// </summary>
        public string JsonIn { get; set; }
        /// <summary>
        /// Json的出参
        /// </summary>
        public string JsonOut { get; set; }
        /// <summary>
        /// 消息是否正常
        /// </summary>
        public bool IsNormal { get; set; } = true;
        /// <summary>
        /// 错误消息描述
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 错误消息代码
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 执行耗时 毫秒(负载均衡+BLL+DLL)总耗时
        /// </summary>
        public double TimeCost { get; set; }
    }
}
