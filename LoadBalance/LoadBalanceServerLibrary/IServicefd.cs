﻿using System.Runtime.Serialization;
using System.ServiceModel;
using System.Collections.Generic;

namespace yezhanbafang.fw.WCF
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ImyCallBack))]
    public interface ImyService
    {
        [OperationContract]
        bool ClientSendData(ydhDeliver dler);

        /// <summary>
        /// 异步接口
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="xmlParam">内容xml</param>
        /// <param name="callOperator">调用者</param>
        /// <param name="certificate">密码</param>
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void ClientSendMessage(string methodName, string xmlParam, string callOperator, string certificate);

        /// <summary>
        /// 同步接口
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="xmlParam">内容xml</param>
        /// <param name="callOperator">调用者</param>
        /// <param name="certificate">密码</param>
        [OperationContract]
        string SynMessage(string methodName, string xmlParam, string callOperator, string certificate);
    }

    interface ImyCallBack
    {
        [OperationContract]
        bool ServerSendData(ydhDeliver dler);

        [OperationContract(IsOneWay = true)]
        void ServerSendMessage(string methodName, string xmlMsg);
    }

    [DataContract]
    public class ydhDeliver
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        [DataMember]
        public int Index { get; set; }

        /// <summary>
        /// 传输内容
        /// </summary>
        [DataMember]
        public byte[] Context { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        [DataMember]
        public bool IsFinish { get; set; }

        /// <summary>
        /// 函数名称
        /// </summary>
        [DataMember]
        public string FunctionName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        [DataMember]
        public string DataType { get; set; }
        /// <summary>
        /// 扩展字段
        /// </summary>
        [DataMember]
        public string Exstr { get; set; }
        /// <summary>
        /// 当前进度
        /// </summary>
        [DataMember]
        public int Now { get; set; }
        /// <summary>
        /// 总长度
        /// </summary>
        [DataMember]
        public int Max { get; set; }
    }

    /// <summary>
    /// 
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
