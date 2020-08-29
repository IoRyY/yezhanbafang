using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1 最后修改时间：2020-3
 * Email windy_23762872@126.com 253625488@qq.com
 * 博客 2016BigProject http://blog.csdn.net/yuandonghuia/article/details/50514985
 * 作用 代码生成器生成的Table类
 * VS版本 2010 2013 2015 2019
 ***********************************************************************************/

namespace yezhanbafang
{
    /// <summary>
    /// 自定义前缀+表名称为类名
    /// </summary>
    public class Table_Car : IoRyRow
    {
        int? _int_index;
        /// <summary>
        /// 数据库int_index字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public int? int_index
        {
            get
            {
                return _int_index;
            }
            set
            {
                _int_index = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "int_index").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "int_index").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "int_index").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "int_index").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_cheliangleixing;
        /// <summary>
        /// 数据库str_cheliangleixing字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_cheliangleixing
        {
            get
            {
                return _str_cheliangleixing;
            }
            set
            {
                _str_cheliangleixing = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_cheliangleixing").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_cheliangleixing").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_cheliangleixing").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_cheliangleixing").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_cheliangpinpai;
        /// <summary>
        /// 数据库str_cheliangpinpai字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_cheliangpinpai
        {
            get
            {
                return _str_cheliangpinpai;
            }
            set
            {
                _str_cheliangpinpai = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_cheliangpinpai").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_cheliangpinpai").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_cheliangpinpai").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_cheliangpinpai").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_cheliangxinghao;
        /// <summary>
        /// 数据库str_cheliangxinghao字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_cheliangxinghao
        {
            get
            {
                return _str_cheliangxinghao;
            }
            set
            {
                _str_cheliangxinghao = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_cheliangxinghao").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_cheliangxinghao").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_cheliangxinghao").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_cheliangxinghao").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_cheshenyanse;
        /// <summary>
        /// 数据库str_cheshenyanse字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_cheshenyanse
        {
            get
            {
                return _str_cheshenyanse;
            }
            set
            {
                _str_cheshenyanse = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_cheshenyanse").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_cheshenyanse").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_cheshenyanse").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_cheshenyanse").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_chejiahao;
        /// <summary>
        /// 数据库str_chejiahao字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_chejiahao
        {
            get
            {
                return _str_chejiahao;
            }
            set
            {
                _str_chejiahao = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_chejiahao").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_chejiahao").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_chejiahao").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_chejiahao").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_guochanjinkou;
        /// <summary>
        /// 数据库str_guochanjinkou字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_guochanjinkou
        {
            get
            {
                return _str_guochanjinkou;
            }
            set
            {
                _str_guochanjinkou = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_guochanjinkou").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_guochanjinkou").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_guochanjinkou").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_guochanjinkou").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_fadongjihao;
        /// <summary>
        /// 数据库str_fadongjihao字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_fadongjihao
        {
            get
            {
                return _str_fadongjihao;
            }
            set
            {
                _str_fadongjihao = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_fadongjihao").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_fadongjihao").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_fadongjihao").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_fadongjihao").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_fadongjixinghao;
        /// <summary>
        /// 数据库str_fadongjixinghao字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_fadongjixinghao
        {
            get
            {
                return _str_fadongjixinghao;
            }
            set
            {
                _str_fadongjixinghao = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_fadongjixinghao").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_fadongjixinghao").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_fadongjixinghao").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_fadongjixinghao").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_ranliaozhonglei;
        /// <summary>
        /// 数据库str_ranliaozhonglei字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_ranliaozhonglei
        {
            get
            {
                return _str_ranliaozhonglei;
            }
            set
            {
                _str_ranliaozhonglei = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_ranliaozhonglei").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_ranliaozhonglei").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_ranliaozhonglei").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_ranliaozhonglei").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_pailiang;
        /// <summary>
        /// 数据库str_pailiang字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_pailiang
        {
            get
            {
                return _str_pailiang;
            }
            set
            {
                _str_pailiang = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_pailiang").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_pailiang").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_pailiang").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_pailiang").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_gonglv;
        /// <summary>
        /// 数据库str_gonglv字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_gonglv
        {
            get
            {
                return _str_gonglv;
            }
            set
            {
                _str_gonglv = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_gonglv").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_gonglv").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_gonglv").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_gonglv").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_zhizaochangmingcheng;
        /// <summary>
        /// 数据库str_zhizaochangmingcheng字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_zhizaochangmingcheng
        {
            get
            {
                return _str_zhizaochangmingcheng;
            }
            set
            {
                _str_zhizaochangmingcheng = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_zhizaochangmingcheng").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_zhizaochangmingcheng").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_zhizaochangmingcheng").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_zhizaochangmingcheng").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_zhuanxiangxingshi;
        /// <summary>
        /// 数据库str_zhuanxiangxingshi字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_zhuanxiangxingshi
        {
            get
            {
                return _str_zhuanxiangxingshi;
            }
            set
            {
                _str_zhuanxiangxingshi = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_zhuanxiangxingshi").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_zhuanxiangxingshi").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_zhuanxiangxingshi").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_zhuanxiangxingshi").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_qianlunju;
        /// <summary>
        /// 数据库str_qianlunju字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_qianlunju
        {
            get
            {
                return _str_qianlunju;
            }
            set
            {
                _str_qianlunju = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_qianlunju").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_qianlunju").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_qianlunju").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_qianlunju").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_houlunju;
        /// <summary>
        /// 数据库str_houlunju字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_houlunju
        {
            get
            {
                return _str_houlunju;
            }
            set
            {
                _str_houlunju = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_houlunju").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_houlunju").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_houlunju").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_houlunju").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_luntaishu;
        /// <summary>
        /// 数据库str_luntaishu字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_luntaishu
        {
            get
            {
                return _str_luntaishu;
            }
            set
            {
                _str_luntaishu = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_luntaishu").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_luntaishu").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_luntaishu").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_luntaishu").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_luntaiguige;
        /// <summary>
        /// 数据库str_luntaiguige字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_luntaiguige
        {
            get
            {
                return _str_luntaiguige;
            }
            set
            {
                _str_luntaiguige = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_luntaiguige").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_luntaiguige").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_luntaiguige").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_luntaiguige").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_gangbantanhuangpianshu;
        /// <summary>
        /// 数据库str_gangbantanhuangpianshu字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_gangbantanhuangpianshu
        {
            get
            {
                return _str_gangbantanhuangpianshu;
            }
            set
            {
                _str_gangbantanhuangpianshu = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_gangbantanhuangpianshu").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_gangbantanhuangpianshu").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_gangbantanhuangpianshu").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_gangbantanhuangpianshu").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_zhouju;
        /// <summary>
        /// 数据库str_zhouju字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_zhouju
        {
            get
            {
                return _str_zhouju;
            }
            set
            {
                _str_zhouju = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_zhouju").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_zhouju").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_zhouju").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_zhouju").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_zhoushu;
        /// <summary>
        /// 数据库str_zhoushu字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_zhoushu
        {
            get
            {
                return _str_zhoushu;
            }
            set
            {
                _str_zhoushu = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_zhoushu").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_zhoushu").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_zhoushu").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_zhoushu").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_waikuochicun_chang;
        /// <summary>
        /// 数据库str_waikuochicun_chang字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_waikuochicun_chang
        {
            get
            {
                return _str_waikuochicun_chang;
            }
            set
            {
                _str_waikuochicun_chang = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_waikuochicun_chang").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_waikuochicun_chang").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_waikuochicun_chang").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_waikuochicun_chang").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_waikuochicun_kuan;
        /// <summary>
        /// 数据库str_waikuochicun_kuan字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_waikuochicun_kuan
        {
            get
            {
                return _str_waikuochicun_kuan;
            }
            set
            {
                _str_waikuochicun_kuan = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_waikuochicun_kuan").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_waikuochicun_kuan").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_waikuochicun_kuan").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_waikuochicun_kuan").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_waikuochicun_gao;
        /// <summary>
        /// 数据库str_waikuochicun_gao字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_waikuochicun_gao
        {
            get
            {
                return _str_waikuochicun_gao;
            }
            set
            {
                _str_waikuochicun_gao = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_waikuochicun_gao").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_waikuochicun_gao").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_waikuochicun_gao").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_waikuochicun_gao").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_huoxiangneibuchicun_chang;
        /// <summary>
        /// 数据库str_huoxiangneibuchicun_chang字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_huoxiangneibuchicun_chang
        {
            get
            {
                return _str_huoxiangneibuchicun_chang;
            }
            set
            {
                _str_huoxiangneibuchicun_chang = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_huoxiangneibuchicun_chang").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_huoxiangneibuchicun_chang").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_huoxiangneibuchicun_chang").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_huoxiangneibuchicun_chang").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_huoxiangneibuchicun_kuan;
        /// <summary>
        /// 数据库str_huoxiangneibuchicun_kuan字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_huoxiangneibuchicun_kuan
        {
            get
            {
                return _str_huoxiangneibuchicun_kuan;
            }
            set
            {
                _str_huoxiangneibuchicun_kuan = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_huoxiangneibuchicun_kuan").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_huoxiangneibuchicun_kuan").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_huoxiangneibuchicun_kuan").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_huoxiangneibuchicun_kuan").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_huoxiangneibuchicun_gao;
        /// <summary>
        /// 数据库str_huoxiangneibuchicun_gao字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_huoxiangneibuchicun_gao
        {
            get
            {
                return _str_huoxiangneibuchicun_gao;
            }
            set
            {
                _str_huoxiangneibuchicun_gao = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_huoxiangneibuchicun_gao").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_huoxiangneibuchicun_gao").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_huoxiangneibuchicun_gao").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_huoxiangneibuchicun_gao").First().ioryValue = Convert.ToString(value);
            }
        }

        int? _int_zongzhiliang;
        /// <summary>
        /// 数据库int_zongzhiliang字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public int? int_zongzhiliang
        {
            get
            {
                return _int_zongzhiliang;
            }
            set
            {
                _int_zongzhiliang = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "int_zongzhiliang").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "int_zongzhiliang").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "int_zongzhiliang").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "int_zongzhiliang").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_hedingzaizhiliang;
        /// <summary>
        /// 数据库str_hedingzaizhiliang字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_hedingzaizhiliang
        {
            get
            {
                return _str_hedingzaizhiliang;
            }
            set
            {
                _str_hedingzaizhiliang = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_hedingzaizhiliang").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_hedingzaizhiliang").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_hedingzaizhiliang").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_hedingzaizhiliang").First().ioryValue = Convert.ToString(value);
            }
        }

        int? _int_hedingzaike;
        /// <summary>
        /// 数据库int_hedingzaike字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public int? int_hedingzaike
        {
            get
            {
                return _int_hedingzaike;
            }
            set
            {
                _int_hedingzaike = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "int_hedingzaike").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "int_hedingzaike").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "int_hedingzaike").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "int_hedingzaike").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_zhunqianyinzongzhiliang;
        /// <summary>
        /// 数据库str_zhunqianyinzongzhiliang字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_zhunqianyinzongzhiliang
        {
            get
            {
                return _str_zhunqianyinzongzhiliang;
            }
            set
            {
                _str_zhunqianyinzongzhiliang = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_zhunqianyinzongzhiliang").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_zhunqianyinzongzhiliang").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_zhunqianyinzongzhiliang").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_zhunqianyinzongzhiliang").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_jiashishizaike;
        /// <summary>
        /// 数据库str_jiashishizaike字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_jiashishizaike
        {
            get
            {
                return _str_jiashishizaike;
            }
            set
            {
                _str_jiashishizaike = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_jiashishizaike").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_jiashishizaike").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_jiashishizaike").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_jiashishizaike").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_shiyongxingzhi;
        /// <summary>
        /// 数据库str_shiyongxingzhi字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_shiyongxingzhi
        {
            get
            {
                return _str_shiyongxingzhi;
            }
            set
            {
                _str_shiyongxingzhi = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_shiyongxingzhi").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_shiyongxingzhi").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_shiyongxingzhi").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_shiyongxingzhi").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_chelianghuodefangshi;
        /// <summary>
        /// 数据库str_chelianghuodefangshi字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_chelianghuodefangshi
        {
            get
            {
                return _str_chelianghuodefangshi;
            }
            set
            {
                _str_chelianghuodefangshi = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_chelianghuodefangshi").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_chelianghuodefangshi").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_chelianghuodefangshi").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_chelianghuodefangshi").First().ioryValue = Convert.ToString(value);
            }
        }

        DateTime? _dat_cheliangchuchangriqi;
        /// <summary>
        /// 数据库dat_cheliangchuchangriqi字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public DateTime? dat_cheliangchuchangriqi
        {
            get
            {
                return _dat_cheliangchuchangriqi;
            }
            set
            {
                _dat_cheliangchuchangriqi = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "dat_cheliangchuchangriqi").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "dat_cheliangchuchangriqi").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "dat_cheliangchuchangriqi").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "dat_cheliangchuchangriqi").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_fazhengjiguangzhang;
        /// <summary>
        /// 数据库str_fazhengjiguangzhang字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_fazhengjiguangzhang
        {
            get
            {
                return _str_fazhengjiguangzhang;
            }
            set
            {
                _str_fazhengjiguangzhang = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_fazhengjiguangzhang").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_fazhengjiguangzhang").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_fazhengjiguangzhang").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_fazhengjiguangzhang").First().ioryValue = Convert.ToString(value);
            }
        }

        DateTime? _dat_fazhengriqi;
        /// <summary>
        /// 数据库dat_fazhengriqi字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public DateTime? dat_fazhengriqi
        {
            get
            {
                return _dat_fazhengriqi;
            }
            set
            {
                _dat_fazhengriqi = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "dat_fazhengriqi").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "dat_fazhengriqi").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "dat_fazhengriqi").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "dat_fazhengriqi").First().ioryValue = Convert.ToString(value);
            }
        }

        DateTime? _dat_createtime;
        /// <summary>
        /// 数据库dat_createtime字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public DateTime? dat_createtime
        {
            get
            {
                return _dat_createtime;
            }
            set
            {
                _dat_createtime = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "dat_createtime").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "dat_createtime").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "dat_createtime").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "dat_createtime").First().ioryValue = Convert.ToString(value);
            }
        }

        DateTime? _dat_changetime;
        /// <summary>
        /// 数据库dat_changetime字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public DateTime? dat_changetime
        {
            get
            {
                return _dat_changetime;
            }
            set
            {
                _dat_changetime = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "dat_changetime").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "dat_changetime").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "dat_changetime").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "dat_changetime").First().ioryValue = Convert.ToString(value);
            }
        }

        /// <summary>
        /// 实现IoRyTable的接口
        /// </summary>
        public void SetData(DataRow dr)
        {
            int_index = dr.Field<int?>("int_index");
            str_cheliangleixing = dr.Field<string>("str_cheliangleixing");
            str_cheliangpinpai = dr.Field<string>("str_cheliangpinpai");
            str_cheliangxinghao = dr.Field<string>("str_cheliangxinghao");
            str_cheshenyanse = dr.Field<string>("str_cheshenyanse");
            str_chejiahao = dr.Field<string>("str_chejiahao");
            str_guochanjinkou = dr.Field<string>("str_guochanjinkou");
            str_fadongjihao = dr.Field<string>("str_fadongjihao");
            str_fadongjixinghao = dr.Field<string>("str_fadongjixinghao");
            str_ranliaozhonglei = dr.Field<string>("str_ranliaozhonglei");
            str_pailiang = dr.Field<string>("str_pailiang");
            str_gonglv = dr.Field<string>("str_gonglv");
            str_zhizaochangmingcheng = dr.Field<string>("str_zhizaochangmingcheng");
            str_zhuanxiangxingshi = dr.Field<string>("str_zhuanxiangxingshi");
            str_qianlunju = dr.Field<string>("str_qianlunju");
            str_houlunju = dr.Field<string>("str_houlunju");
            str_luntaishu = dr.Field<string>("str_luntaishu");
            str_luntaiguige = dr.Field<string>("str_luntaiguige");
            str_gangbantanhuangpianshu = dr.Field<string>("str_gangbantanhuangpianshu");
            str_zhouju = dr.Field<string>("str_zhouju");
            str_zhoushu = dr.Field<string>("str_zhoushu");
            str_waikuochicun_chang = dr.Field<string>("str_waikuochicun_chang");
            str_waikuochicun_kuan = dr.Field<string>("str_waikuochicun_kuan");
            str_waikuochicun_gao = dr.Field<string>("str_waikuochicun_gao");
            str_huoxiangneibuchicun_chang = dr.Field<string>("str_huoxiangneibuchicun_chang");
            str_huoxiangneibuchicun_kuan = dr.Field<string>("str_huoxiangneibuchicun_kuan");
            str_huoxiangneibuchicun_gao = dr.Field<string>("str_huoxiangneibuchicun_gao");
            int_zongzhiliang = dr.Field<int?>("int_zongzhiliang");
            str_hedingzaizhiliang = dr.Field<string>("str_hedingzaizhiliang");
            int_hedingzaike = dr.Field<int?>("int_hedingzaike");
            str_zhunqianyinzongzhiliang = dr.Field<string>("str_zhunqianyinzongzhiliang");
            str_jiashishizaike = dr.Field<string>("str_jiashishizaike");
            str_shiyongxingzhi = dr.Field<string>("str_shiyongxingzhi");
            str_chelianghuodefangshi = dr.Field<string>("str_chelianghuodefangshi");
            dat_cheliangchuchangriqi = dr.Field<DateTime?>("dat_cheliangchuchangriqi");
            str_fazhengjiguangzhang = dr.Field<string>("str_fazhengjiguangzhang");
            dat_fazhengriqi = dr.Field<DateTime?>("dat_fazhengriqi");
            dat_createtime = dr.Field<DateTime?>("dat_createtime");
            dat_changetime = dr.Field<DateTime?>("dat_changetime");
            foreach (var item in LIC)
            {
                item.ioryValueChange = false;
            }
        }
        /// <summary>
        /// 初始化函数
        /// </summary>
        public Table_Car()
        {
            LIC.Add(new IoRyCol
            {
                ioryName = "int_index",
                ioryType = "int?",
                IsIdentity = true,
                IsKey = true,
                IsNull = false,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_cheliangleixing",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_cheliangpinpai",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_cheliangxinghao",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_cheshenyanse",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_chejiahao",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_guochanjinkou",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_fadongjihao",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_fadongjixinghao",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_ranliaozhonglei",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_pailiang",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_gonglv",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_zhizaochangmingcheng",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_zhuanxiangxingshi",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_qianlunju",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_houlunju",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_luntaishu",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_luntaiguige",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_gangbantanhuangpianshu",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_zhouju",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_zhoushu",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_waikuochicun_chang",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_waikuochicun_kuan",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_waikuochicun_gao",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_huoxiangneibuchicun_chang",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_huoxiangneibuchicun_kuan",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_huoxiangneibuchicun_gao",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "int_zongzhiliang",
                ioryType = "int?",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_hedingzaizhiliang",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "int_hedingzaike",
                ioryType = "int?",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_zhunqianyinzongzhiliang",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_jiashishizaike",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_shiyongxingzhi",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_chelianghuodefangshi",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "dat_cheliangchuchangriqi",
                ioryType = "DateTime?",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_fazhengjiguangzhang",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "dat_fazhengriqi",
                ioryType = "DateTime?",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "dat_createtime",
                ioryType = "DateTime?",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "dat_changetime",
                ioryType = "DateTime?",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
        }

        string tablename = "Table_Car";﻿
        /// <summary>
        /// LIC是列集合
        /// i前缀+列名为字段名
        /// </summary>
        List<IoRyCol> LIC = new List<IoRyCol>();

        /// <summary>
        /// 获取新增方法的Sql语句
        /// </summary>
        /// <returns></returns>
        string IoRyAdd_Sql()
        {
            string sqlp = " insert into " + tablename + " ({0}) values ({1})";
            List<string> lscname = new List<string>();
            List<string> lscvalue = new List<string>();
            foreach (IoRyCol item in this.LIC)
            {
                if (item.ioryValueNull == false && item.IsIdentity == false)
                {
                    lscname.Add(item.ioryName);
                    lscvalue.Add("'" + item.ioryValue.Replace("'", "''") + "'");
                }
            }
            if (lscname.Count == 0)
            {
                throw new Exception("新增的类必须有值!");
            }
            string sql = string.Format(sqlp, string.Join(",", lscname), string.Join(",", lscvalue));
            return sql;
        }

        /// <summary>
        /// 普通新增
        /// </summary>
        /// <returns></returns>
        public void IoRyAdd()
        {
            IoRyFunction.CallIoRyClass(this.IoRyAdd_Sql());
        }

        /// <summary>
        /// 普通新增 事务
        /// </summary>
        /// <param name="tran"></param>
        public void Tran_IoRyAdd(IoRyTransaction tran)
        {
            tran.Sql += this.IoRyAdd_Sql() + " ;";
        }

        /// <summary>
        /// 带Log新增
        /// </summary>
        /// <returns></returns>
        public void IoRyAdd(string cuser)
        {
            IoRyFunction.CallIoRyClass(this.IoRyAdd_Sql(), cuser);
        }

        /// <summary>
        /// 获取更新方法的Sql语句
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        string IoRyUpdate_Sql(List<string> keys)
        {
            string sqlp = "update " + tablename + " set {0} where {1}";
            List<string> lsset = new List<string>();
            List<string> lswhere = new List<string>();
            if (LIC.Any(x => x.ioryValueChange == true))
            {
                foreach (var item in LIC)
                {
                    if (item.ioryValueChange == true)
                    {
                        if (!keys.Contains(item.ioryName))
                        {
                            if (item.ioryValueNull)
                            {
                                lsset.Add(item.ioryName + " = null ");
                            }
                            else
                            {
                                lsset.Add(item.ioryName + "='" + item.ioryValue.Replace("'", "''") + "'");
                            }
                        }
                    }
                }
                foreach (var item in keys)
                {
                    string mv = LIC.Where(x => x.ioryName == item).First().ioryValue;
                    lswhere.Add(item + "='" + mv + "'");
                }
                string sql = string.Format(sqlp, string.Join(",", lsset), string.Join(" and ", lswhere));
                return sql;
            }
            else
            {
                throw new Exception("此数据没有修改!");
            }
        }

        /// <summary>
        /// 自定义where 修改
        /// </summary>
        /// <param name="keys"></param>
        public void IoRyUpdate(List<string> keys)
        {
            IoRyFunction.CallIoRyClass(this.IoRyUpdate_Sql(keys));
        }

        /// <summary>
        /// 自定义where 修改 事务
        /// </summary>
        /// <param name="tran"></param>
        public void Tran_IoRyUpdate(IoRyTransaction tran, List<string> keys)
        {
            tran.Sql += this.IoRyUpdate_Sql(keys) + " ;";
        }

        /// <summary>
        /// 自定义where 带Log 修改
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="cuser"></param>
        public void IoRyUpdate(List<string> keys, string cuser)
        {
            IoRyFunction.CallIoRyClass(this.IoRyUpdate_Sql(keys), cuser);
        }

        /// <summary>
        /// 普通修改 以keys为where
        /// </summary>
        public void IoRyUpdate()
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            this.IoRyUpdate(ls);
        }

        /// <summary>
        /// 普通修改 事务 以keys为where
        /// </summary>
        /// <param name="tran"></param>
        public void Tran_IoRyUpdate(IoRyTransaction tran)
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            tran.Sql += this.IoRyUpdate_Sql(ls) + " ;";
        }

        /// <summary>
        /// 带Log修改 以keys为where
        /// </summary>
        /// <param name="cuser"></param>
        public void IoRyUpdate(string cuser)
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            this.IoRyUpdate(ls, cuser);
        }

        /// <summary>
        /// 获取删除方法的Sql语句
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        string IoRyDelete_Sql(List<string> keys)
        {
            string sqlp = "delete " + tablename + " where {0}";
            List<string> lswhere = new List<string>();
            foreach (var item in keys)
            {
                string mv = LIC.Where(x => x.ioryName == item).First().ioryValue;
                lswhere.Add(item + "='" + mv + "'");
            }
            string sql = string.Format(sqlp, string.Join(" and ", lswhere));
            return sql;
        }


        /// <summary>
        /// 普通删除 自定义where
        /// </summary>
        /// <param name="keys"></param>
        public void IoRyDelete(List<string> keys)
        {
            IoRyFunction.CallIoRyClass(this.IoRyDelete_Sql(keys));
        }

        /// <summary>
        /// 普通删除 事务 自定义where
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="keys"></param>
        public void Tran_IoRyDelete(IoRyTransaction tran, List<string> keys)
        {
            tran.Sql += this.IoRyDelete_Sql(keys) + " ;";
        }

        /// <summary>
        /// 带Log删除 自定义where
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="cuser"></param>
        public void IoRyDelete(List<string> keys, string cuser)
        {
            IoRyFunction.CallIoRyClass(this.IoRyDelete_Sql(keys), cuser);
        }

        /// <summary>
        /// 普通删除 以keys为where 
        /// </summary>
        public void IoRyDelete()
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            this.IoRyDelete(ls);
        }

        /// <summary>
        /// 普通删除 事务 以keys为where 
        /// </summary>
        /// <param name="tran"></param>
        public void Tran_IoRyDelete(IoRyTransaction tran)
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            tran.Sql += this.IoRyDelete_Sql(ls) + " ;";
        }

        /// <summary>
        /// 带Log删除 以keys为where
        /// </summary>
        /// <param name="cuser"></param>
        public void IoRyDelete(string cuser)
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            this.IoRyDelete(ls, cuser);
        }
    }
}