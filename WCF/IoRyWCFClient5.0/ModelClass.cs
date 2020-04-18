using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoRyWCFClientV5
{
    public class ModelClassDeliver
    {
        string _Name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        int _Index;
        /// <summary>
        /// 序号
        /// </summary>
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }
        /// <summary>
        /// 是否结束
        /// </summary>
        public bool IsFinish { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Exstr { get; set; }
        /// <summary>
        /// 当前进度
        /// </summary>
        public int Now { get; set; }
        /// <summary>
        /// 总长度
        /// </summary>
        public int Max { get; set; }


    }
}
