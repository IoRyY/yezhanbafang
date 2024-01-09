using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace yezhanbafang.sd.Core
{
    /// <summary>
    /// 袁东辉 2010-11-1日 总结之前项目，提取有用的东西。
    /// 袁东辉 2010-11-11 修改了所有的方法，带path的方法和不带path的方法不能一样，带path要重新读路径获取连接方式等，不带的则读取默认
    /// 2010-11-23 捋了一下公司笔记本的之前的项目，唯一一个有点用处，但是没加到类库的方法就是webcatch了。
    /// 执行存储过程,用sqlparameter的方式,一般返回数据集  2012-4-17添加
    /// 2013-3-27 加入了Oracle数据库
    /// 2013-4-17 本想把XML操作也家进去,但是感觉实在没啥可以集成的.写如下提醒
    /// linqtoxml命名空间System.Xml.Linq;读取:XElement xe = XElement.Load("XMLFile1.xml");删除Remove();保存 xe.Save("XMLFile1.xml");
    /// 选择e.Elements("con").Select(x =>x.Attribute("a").Value).ToList();
    /// 新增xe.Add(new XElement("con", new XAttribute("a", "4"),new XAttribute("b","ydh")));
    /// 2013-10-25加入了各种对Oracle的支持，包括Oracle存储过程取得返回值
    /// 20190306 加入了设置sql连接超时时间的支持
    /// 2019年11月增加了带参数的SQL语句,解决bit类型的sql语句,也可以用作sql防注入
    /// 2020年2月30日新增oralce加密方法支持provide
    /// 20200302 加入Excel的OlEDB方式
    /// 20200318 把Excel操作当成比较普通的方式 注意 Excel的方式不能执行Delete语句
    /// 20200320 修改带log的表的结构,老数据用xml来表示
    /// 20200323 增加了日志表的本地IP和本地UUID
    /// 20200401 优化了public结构,增加了DbParameterS的getdatatale的方式以及执行事务方式,增加了带DbParameterS的日志支持
    /// 20200401 整体命名,结构大改,升级到2.0版本
    /// 20200407 解决方案从最初的没有,到灯向晓影无眠到夜战八方,standard相关DLL,github,nuget正式上线
    /// </summary>
    public class YezhanbafangCore
    {

        #region 属性

        /// <summary>
        /// 数据库类型
        /// </summary>
        ConType _Contype = ConType.Null;

        /// <summary>
        /// 数据库类型
        /// </summary>
        protected ConType Contype
        {
            get
            {
                if (this._Contype == ConType.Null)
                {
                    this.ReadConString();
                }
                return this._Contype;
            }
            set { this._Contype = value; }
        }

        /// <summary>
        /// 数据库超时时间
        /// </summary>
        protected int timeout = -1;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConString { get; set; }

        /// <summary>
        /// 默认路径
        /// </summary>
        string _Path = System.AppDomain.CurrentDomain.BaseDirectory + "constring.xml";

        /// <summary>
        /// 设置或者获取默认路径
        /// </summary>
        protected string Path
        {
            get { return this._Path; }
            set { this._Path = value; }
        }

        static string _PCIP = null;
        /// <summary>
        /// 本地电脑IP
        /// </summary>
        static public string PCIP
        {
            get
            {
                if (_PCIP == null)
                {
                    SelectQuery query = new SelectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                    {
                        foreach (var item in searcher.Get())
                        {
                            if (Convert.ToBoolean(item["IPEnabled"]))
                            {
                                _PCIP = ((string[])item["IPAddress"])[0];
                            }
                        }
                    }
                }
                return _PCIP;
            }
        }

        static string _UUID = null;
        /// <summary>
        /// 本地电脑UUID
        /// </summary>
        static public string UUID
        {
            get
            {
                if (_UUID == null)
                {
                    SelectQuery query = new SelectQuery("select * from Win32_ComputerSystemProduct");
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                    {
                        foreach (var item in searcher.Get())
                        {
                            _UUID = item["UUID"].ToString();
                        }
                    }
                }
                return _UUID;
            }
        }

        #endregion

        #region 数据库基类

        /// <summary>
        /// 读取xml自定义的链接字符串
        /// </summary>
        /// <returns></returns>
        void ReadConString()
        {
            this.ReadConString(this.Path);
        }

        /// <summary>
        /// 读取xml自定义的链接字符串
        /// </summary>
        /// <param name="path">xml文件的名称</param>
        /// <returns></returns>
        void ReadConString(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //判断是否存在此xml
            if (!File.Exists(path))
            {
                throw new Exception("不能找到IoRyClass类的XML配置文件！");
            }
            xmlDoc.Load(path);
            //判断连接字符的类型
            XmlNode contype = xmlDoc.SelectSingleNode("constring/type");
            if (contype.InnerText.Trim() != "MSSQL" && contype.InnerText.Trim() != "ACCESS" 
                && contype.InnerText.Trim() != "Oracle" && contype.InnerText.Trim() != "Excel" && contype.InnerText.Trim() != "MySQL")
            {
                throw new Exception("数据连接类型没填写,或者填写错误,只能填写MSSQL;Oracle;MySQL;ACCESS;Excel并且区分大小写!");
            }

            if (contype.InnerText.Trim() == "MSSQL")//sql
            {
                this._Contype = ConType.MSSQL;
                //判断是否用简单的直接写字符串的方式
                XmlNode mynode = xmlDoc.SelectSingleNode("constring/sqlserver/simple");
                if (mynode.InnerText.Trim() != "")
                {
                    this.ConString = mynode.FirstChild.Value;
                }
                else
                {
                    mynode = xmlDoc.SelectSingleNode("constring/sqlserver/ip");
                    string ip = mynode.FirstChild.Value;
                    mynode = xmlDoc.SelectSingleNode("constring/sqlserver/databasename");
                    string databasename = mynode.FirstChild.Value;
                    mynode = xmlDoc.SelectSingleNode("constring/sqlserver/username");
                    string username = mynode.FirstChild.Value;
                    string password = null;
                    mynode = xmlDoc.SelectSingleNode("constring/sqlserver/passwordencryption");
                    //判断数据库字符串是否加密
                    if (mynode.InnerText.Trim() != "")
                    {
                        XmlNode key = xmlDoc.SelectSingleNode("constring/sqlserver/encryptKey");
                        password = YezhanbafangCore.DecryptDES(mynode.FirstChild.Value, key.FirstChild.Value);
                    }
                    else
                    {
                        mynode = xmlDoc.SelectSingleNode("constring/sqlserver/password");
                        password = mynode.FirstChild.Value;
                    }
                    string con = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", ip, databasename, username, password);
                    this.ConString = con;
                }
            }
            else if (contype.InnerText.Trim() == "ACCESS") //access
            {
                this._Contype = ConType.Access;
                //判断是否用简单的直接写字符串的方式
                XmlNode mynode = xmlDoc.SelectSingleNode("constring/access/simple");
                if (mynode.InnerText.Trim() != "")
                {
                    this.ConString = mynode.FirstChild.Value;
                }
                else
                {
                    mynode = xmlDoc.SelectSingleNode("constring/access/path");
                    string accpath = mynode.FirstChild.Value;
                    if (File.Exists(accpath))
                    {
                        string password = null;
                        mynode = xmlDoc.SelectSingleNode("constring/access/passwordencryption");
                        //判断数据库字符串是否加密
                        if (mynode.InnerText.Trim() != "")
                        {
                            XmlNode key = xmlDoc.SelectSingleNode("constring/access/encryptKey");
                            password = YezhanbafangCore.DecryptDES(mynode.FirstChild.Value, key.FirstChild.Value);
                        }
                        else
                        {
                            mynode = xmlDoc.SelectSingleNode("constring/access/password");
                            password = mynode.FirstChild.Value;
                        }
                        string con = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password={1}", accpath, password);
                        this.ConString = con;
                    }
                    else
                    {
                        throw new Exception("找不到文件:" + accpath);
                    }
                }
            }
            else if (contype.InnerText.Trim() == "Oracle") //Oracle
            {
                this._Contype = ConType.Oracle;
                //判断是否用简单的直接写字符串的方式
                XmlNode mynode = xmlDoc.SelectSingleNode("constring/Oracle/simple");
                if (mynode.InnerText.Trim() != "")
                {
                    this.ConString = mynode.FirstChild.Value;
                }
                else
                {
                    mynode = xmlDoc.SelectSingleNode("constring/Oracle/DataSource");
                    string DBServer = mynode.FirstChild.Value;
                    mynode = xmlDoc.SelectSingleNode("constring/Oracle/username");
                    string username = mynode.FirstChild.Value;
                    string password = null;
                    mynode = xmlDoc.SelectSingleNode("constring/Oracle/passwordencryption");
                    //判断数据库字符串是否加密
                    if (mynode.InnerText.Trim() != "")
                    {
                        XmlNode key = xmlDoc.SelectSingleNode("constring/Oracle/encryptKey");
                        password = YezhanbafangCore.DecryptDES(mynode.FirstChild.Value, key.FirstChild.Value);
                    }
                    else
                    {
                        mynode = xmlDoc.SelectSingleNode("constring/Oracle/password");
                        password = mynode.FirstChild.Value;
                    }
                    mynode = xmlDoc.SelectSingleNode("constring/Oracle/Provider");
                    string con;
                    con = string.Format("Data Source={0};User ID={1};Password={2}", DBServer, username, password);
                    this.ConString = con;
                }
            }
            else if (contype.InnerText.Trim() == "Excel")
            {
                this.Contype = ConType.Excel;
                XmlNode mynode = xmlDoc.SelectSingleNode("constring/Excel/path");
                string Excelpath = mynode.FirstChild.Value;
                if (File.Exists(Excelpath))
                {
                    this.ConString = this.GetExcelReadonlyConnStr(Excelpath);
                }
                else
                {
                    throw new Exception("找不到文件:" + Excelpath);
                }

            }
            else if (contype.InnerText.Trim() == "MySQL") 
            {
                this._Contype = ConType.MySQL;
                //判断是否用简单的直接写字符串的方式
                XmlNode mynode = xmlDoc.SelectSingleNode("constring/MySQL/simple");
                if (mynode.InnerText.Trim() != "")
                {
                    this.ConString = mynode.FirstChild.Value;
                }
                else
                {
                    mynode = xmlDoc.SelectSingleNode("constring/MySQL/databasename");
                    string databasename = mynode.FirstChild.Value;
                    mynode = xmlDoc.SelectSingleNode("constring/MySQL/ip");
                    string IP = mynode.FirstChild.Value;
                    mynode = xmlDoc.SelectSingleNode("constring/MySQL/port");
                    string PORT = mynode.FirstChild.Value;
                    mynode = xmlDoc.SelectSingleNode("constring/MySQL/username");
                    string username = mynode.FirstChild.Value;
                    string password = null;
                    mynode = xmlDoc.SelectSingleNode("constring/MySQL/passwordencryption");
                    //判断数据库字符串是否加密
                    if (mynode.InnerText.Trim() != "")
                    {
                        XmlNode key = xmlDoc.SelectSingleNode("constring/MySQL/encryptKey");
                        password = YezhanbafangCore.DecryptDES(mynode.FirstChild.Value, key.FirstChild.Value);
                    }
                    else
                    {
                        mynode = xmlDoc.SelectSingleNode("constring/MySQL/password");
                        password = mynode.FirstChild.Value;
                    }
                    //20230419 改动了链接字符串
                    string con = string.Format("server={1};port={2};uid={3};pwd={4};database={0};", databasename, IP, PORT, username, password);
                    this.ConString = con;
                }
            }
            else
            {
                this.ConString = null;
            }
        }

        /// <summary>
        /// 可以写入的Excel链接字符串
        /// </summary>
        protected void ExcelWriteConString()
        {
            string path = this.Path;
            XmlDocument xmlDoc = new XmlDocument();
            //判断是否存在此xml
            if (!File.Exists(path))
            {
                throw new Exception("不能找到IoRyClass类的XML配置文件！");
            }
            xmlDoc.Load(path);
            //判断连接字符的类型
            XmlNode contype = xmlDoc.SelectSingleNode("constring/type");
            if (contype.InnerText.Trim() != "SQL" && contype.InnerText.Trim() != "ACCESS" && contype.InnerText.Trim() != "Oracle" && contype.InnerText.Trim() != "Excel")
            {
                throw new Exception("数据连接类型没填写,或者填写错误,只能填写SQL;ACCESS;Oracle;Excel并且区分大小写!");
            }

            if (contype.InnerText.Trim() == "Excel")
            {
                XmlNode mynode = xmlDoc.SelectSingleNode("constring/Excel/path");
                string Excelpath = mynode.FirstChild.Value;
                if (File.Exists(Excelpath))
                {
                    this.ConString = this.GetExcelConnStr(Excelpath);
                }
                else
                {
                    throw new Exception("找不到文件:" + Excelpath);
                }
            }
            else
            {
                this.ConString = null;
            }
        }

        /// <summary>
        /// 只读Excel连接串
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected string GetExcelReadonlyConnStr(string filePath)
        {
            string connStr = string.Empty;

            if (filePath.Contains(".xlsx"))
            {
                connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1;'";
            }
            else if (filePath.Contains(".xls"))
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1;'";
            }
            else
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath.Remove(filePath.LastIndexOf("\\") + 1) + ";Extended Properties='Text;FMT=Delimited;HDR=YES;'";
            }

            return connStr;
        }

        /// <summary>
        /// Excel连接串
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected string GetExcelConnStr(string filePath)
        {
            string connStr = string.Empty;

            if (filePath.Contains(".xlsx"))
            {
                connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=0;'";
            }
            else if (filePath.Contains(".xls"))
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=0;'";
            }
            else
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath.Remove(filePath.LastIndexOf("\\") + 1) + ";Extended Properties='Text;FMT=Delimited;HDR=YES;'";
            }

            return connStr;
        }


        #endregion

        #region 加密

        #region 可逆向运算的加密方式，需要密钥 对称加密

        //默认密钥向量
        private static byte[] Keys = { 0x22, 0x74, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region 非对称加密

        //RSA rsa = RSA.Create();
        //rsa.ToXmlString(false)公钥
        //rsa.ToXmlString(true)秘钥
        //string enData = EnRSA(加密串, rsa.ToXmlString(false));
        //string bbb = DeRSA(解密串, rsa.ToXmlString(true));

        /// <summary>
        /// 非对称加密 加密 默认的有长度限制
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publickey">RSA rsa = RSA.Create();rsa.ToXmlString(false)</param>
        /// <returns></returns>
        public static string EnRSA(string data, string publickey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(data), false);
            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// 非对称加密 加密 无长度限制
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publickey"></param>
        /// <returns></returns>
        public static String EncryptRSA_long(string data, string publickey)
        {
            using (RSACryptoServiceProvider RSACryptography = new RSACryptoServiceProvider())
            {
                Byte[] PlaintextData = Encoding.UTF8.GetBytes(data);
                RSACryptography.FromXmlString(publickey);
                int MaxBlockSize = RSACryptography.KeySize / 8 - 11;    //加密块最大长度限制

                if (PlaintextData.Length <= MaxBlockSize)
                {
                    return Convert.ToBase64String(RSACryptography.Encrypt(PlaintextData, false));
                }

                using (MemoryStream PlaiStream = new MemoryStream(PlaintextData))
                {
                    using (MemoryStream CrypStream = new MemoryStream())
                    {
                        Byte[] Buffer = new Byte[MaxBlockSize];
                        int BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);

                        while (BlockSize > 0)
                        {
                            Byte[] ToEncrypt = new Byte[BlockSize];
                            Array.Copy(Buffer, 0, ToEncrypt, 0, BlockSize);

                            Byte[] Cryptograph = RSACryptography.Encrypt(ToEncrypt, false);
                            CrypStream.Write(Cryptograph, 0, Cryptograph.Length);

                            BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);
                        }

                        return Convert.ToBase64String(CrypStream.ToArray(), Base64FormattingOptions.None);
                    }
                }
            }
        }

        /// <summary>
        /// 非对称加密 解密 默认的有长度限制
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privatekey">RSA rsa = RSA.Create();rsa.ToXmlString(true)</param>
        /// <returns></returns>
        public static string DeRSA(string data, string privatekey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privatekey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(data), false);
            return Encoding.UTF8.GetString(cipherbytes);
        }

        /// <summary>
        /// 非对称加密 解密 无长度限制
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privatekey"></param>
        /// <returns></returns>
        public static String DecryptRSA_long(string data, string privatekey)
        {
            using (RSACryptoServiceProvider RSACryptography = new RSACryptoServiceProvider())
            {
                byte[] CiphertextData = Convert.FromBase64String(data);
                RSACryptography.FromXmlString(privatekey);
                int MaxBlockSize = RSACryptography.KeySize / 8;    //解密块最大长度限制

                if (CiphertextData.Length <= MaxBlockSize)
                {
                    return Encoding.UTF8.GetString(RSACryptography.Decrypt(CiphertextData, false));
                }

                using (MemoryStream CrypStream = new MemoryStream(CiphertextData))
                {
                    using (MemoryStream PlaiStream = new MemoryStream())
                    {
                        Byte[] Buffer = new Byte[MaxBlockSize];
                        int BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);

                        while (BlockSize > 0)
                        {
                            Byte[] ToDecrypt = new Byte[BlockSize];
                            Array.Copy(Buffer, 0, ToDecrypt, 0, BlockSize);

                            Byte[] Plaintext = RSACryptography.Decrypt(ToDecrypt, false);
                            PlaiStream.Write(Plaintext, 0, Plaintext.Length);

                            BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);
                        }

                        return Encoding.UTF8.GetString(PlaiStream.ToArray());
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 这个函数用来给数据进行md5加密 不可逆向运算 2016-3-9改为UFT8编码
        /// </summary>
        /// <param name="strIn">输入字符串</param>
        /// <returns>输出</returns>
        public static string MD5(string strIn)
        {
            byte[] MainClass = System.Text.Encoding.UTF8.GetBytes(strIn);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(MainClass);
            return BitConverter.ToString(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="byteIn"></param>
        /// <returns></returns>
        public static string MD5(byte[] byteIn)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(byteIn);
            return BitConverter.ToString(result);
        }

        #endregion

        #region 字节转化，压缩

        /// <summary>
        /// 压缩数据
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static byte[] CompressData(byte[] Data)
        {
            if (Data != null)
            {
                MemoryStream ms = new MemoryStream();
                DeflateStream deflateStream = new DeflateStream(ms, CompressionMode.Compress, false);
                deflateStream.Write(Data, 0, Data.Length);
                deflateStream.Close();
                return ms.ToArray();
            }
            return null;
        }

        /// <summary>
        /// 解压缩数据
        /// </summary>
        /// <param name="Data">解压的数据</param>
        /// <param name="nMaxScale">由于无法获取实际解压后的大小,设置一个相对于原始数据大小的比例值</param>
        /// <returns></returns>
        public static byte[] DecompressData(byte[] Data, int nMaxScale)
        {
            MemoryStream ms = new MemoryStream(Data);

            #region -获取解压后的大小,要改进(太慢了)

            //DeflateStream deflateStreamSize = new DeflateStream(ms, CompressionMode.Decompress,false);
            //int nLenght    = new int();
            //nLenght        = 0;
            //while (deflateStreamSize.ReadByte() != -1)
            //{
            //    nLenght = nLenght + 1;
            //}

            #endregion

            //byte[] dezipArray = new byte[nLenght];
            //ms.Position = 0;
            // 由于无法获取实际解压后的大小,这里设置一个估算值
            byte[] dezipArray = new byte[Data.Length * nMaxScale];
            DeflateStream deflateStream = new DeflateStream(ms, CompressionMode.Decompress, false);
            int nL = deflateStream.Read(dezipArray, 0, dezipArray.Length);
            byte[] resultData = new byte[nL];
            Array.Copy(dezipArray, resultData, nL);
            deflateStream.Close();
            return resultData;
        }

        /// <summary>
        /// 取得DataSet的二进制XML数据(仅用于WebService序列化使用,可以提高传输速度约%40)
        /// </summary>
        /// <param name="dsOriginal">序列化的DataSet</param>
        /// <returns>二进制形式的XML</returns>
        public static byte[] GetBinaryFormatDataSet(DataSet dsOriginal)
        {
            if (dsOriginal != null)
            {
                byte[] binaryDataResult = null;
                using (MemoryStream memStream = new MemoryStream())        // 需要一个内存流对象
                {
                    IFormatter brFormatter = new BinaryFormatter();        // 二进制序列化对象
                    dsOriginal.RemotingFormat = SerializationFormat.Binary;// DataSet必须拥有此属性

                    brFormatter.Serialize(memStream, dsOriginal);
                    binaryDataResult = memStream.ToArray();
                    memStream.Close();
                    return binaryDataResult;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将二进制的DataSetXML反序列化为标准XML DataSet
        /// </summary>
        /// <param name="binaryData">需要反序列化的DatSet</param>
        /// <returns>标准XML的DataSet</returns>
        public static DataSet RetrieveDataSet(byte[] binaryData)
        {
            if (binaryData != null)
            {
                using (MemoryStream memStream = new MemoryStream(binaryData))
                {
                    IFormatter brFormatter = new BinaryFormatter();
                    DataSet ds = (DataSet)brFormatter.Deserialize(memStream);
                    memStream.Close();
                    return ds;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 序列化DataSet对象
        /// </summary>
        /// <param name="dsOriginal"></param>
        /// <returns></returns>
        public static byte[] GetXmlFormatDataSet(DataSet dsOriginal)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                XmlSerializer xs = new XmlSerializer(typeof(DataSet));
                xs.Serialize(memStream, dsOriginal);
                return memStream.ToArray();
            }
        }

        /// <summary>
        /// 反序列化DataSet对象
        /// </summary>
        /// <param name="binaryData"></param>
        /// <returns></returns>
        public static DataSet RetrieveXmlDataSet(byte[] binaryData)
        {
            using (MemoryStream memStream = new MemoryStream(binaryData))
            {
                XmlSerializer xs = new XmlSerializer(typeof(DataSet));
                DataSet ds = xs.Deserialize(memStream) as DataSet;
                return ds;
            }
        }

        /// <summary>
        /// 将string转化成utf8的bytes
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] StringToBytes(string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// 将bytes转化成string
        /// </summary>
        /// <param name="mybyte"></param>
        /// <returns></returns>
        public static string BytesToString(byte[] mybyte)
        {
            return System.Text.Encoding.UTF8.GetString(mybyte);
        }

        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="str">输入</param>
        /// <returns>压缩后的byte[]</returns>
        public static byte[] StringToZip(string str)
        {
            byte[] myby = YezhanbafangCore.StringToBytes(str);
            return YezhanbafangCore.CompressData(myby);
        }

        /// <summary>
        /// 解压缩字符串
        /// </summary>
        /// <param name="mybytes"></param>
        /// <returns></returns>
        public static string ZipToString(byte[] mybytes)
        {
            byte[] myte = YezhanbafangCore.DecompressData(mybytes, 10);
            return YezhanbafangCore.BytesToString(myte);
        }

        /// <summary>
        /// 压缩DataTable
        /// </summary>
        /// <param name="mydt">DataTable</param>
        /// <returns></returns>
        public static byte[] DataTableToZip(DataTable mydt)
        {
            DataSet myds = new DataSet();
            myds.Tables.Add(mydt.Copy());
            byte[] mybt = YezhanbafangCore.GetBinaryFormatDataSet(myds);
            return YezhanbafangCore.CompressData(mybt);
        }

        /// <summary>
        /// 解压DataTable
        /// </summary>
        /// <param name="mybytes"></param>
        /// <returns></returns>
        public static DataTable ZipToDateTable(byte[] mybytes)
        {
            byte[] myby = YezhanbafangCore.DecompressData(mybytes, 50);
            DataSet myds = YezhanbafangCore.RetrieveDataSet(myby);
            return myds.Tables[0];
        }

        /// <summary>
        /// 压缩DataSet
        /// </summary>
        /// <param name="myds">DateSet</param>
        /// <returns></returns>
        public static byte[] DataSetToZip(DataSet myds)
        {
            byte[] mybys = YezhanbafangCore.GetBinaryFormatDataSet(myds);
            return YezhanbafangCore.CompressData(mybys);
        }

        /// <summary>
        /// 解压DataSet
        /// </summary>
        /// <param name="mybytes"></param>
        /// <returns></returns>
        public static DataSet ZipToDataSet(byte[] mybytes)
        {
            byte[] myby = YezhanbafangCore.DecompressData(mybytes, 50);
            return YezhanbafangCore.RetrieveDataSet(myby);
        }

        #endregion

        #region CSV文件
        /// <summary>
        /// 注意这里用的是gb2312,符合现在一般Excel打开CSV的格式
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public void CSV_Create(DataTable dt, string path)
        {
            try
            {
                System.IO.FileStream fs = new FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                System.Text.Encoding gb2312 = System.Text.Encoding.GetEncoding("gb2312");
                StreamWriter sw = new StreamWriter(fs, gb2312);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i].ColumnName);
                    if (i == dt.Columns.Count - 1)
                    {
                        sw.WriteLine();
                    }
                    else
                    {
                        sw.Write(",");
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sw.Write(Convert.ToString(dt.Rows[i][j]));
                        if (j < dt.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    if (i < dt.Rows.Count - 1)
                    {
                        sw.WriteLine();
                    }
                }
                sw.Flush();
                sw.Close();
            }
            catch (Exception me)
            {
                throw me;
            }
        }

        #endregion
    }

    #region 枚举
    /// <summary>
    /// 链接类型
    /// </summary>
    public enum ConType
    {
        /// <summary>
        /// 空
        /// </summary>
        Null,
        /// <summary>
        /// sql
        /// </summary>
        MSSQL,
        /// <summary>
        /// access
        /// </summary>
        Access,
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle,
        /// <summary>
        /// MySQL
        /// </summary>
        MySQL,
        /// <summary>
        /// Excel
        /// </summary>
        Excel
    }

    #endregion
}
