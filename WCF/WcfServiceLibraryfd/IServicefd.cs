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
        void ServerSendMessage(string mesg);
    }

    [DataContract]
    public class ydhDeliver
    {
        string _Name;

        [DataMember]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        int _Index;
        [DataMember]
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        byte[] _Context;
        [DataMember]
        public byte[] Context
        {
            get { return _Context; }
            set { _Context = value; }
        }

        [DataMember]
        public bool IsFinish { get; set; }

        /// <summary>
        /// 数据类型
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
}
