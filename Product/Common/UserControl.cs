using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;

namespace product.Common
{
    public class UserControl : System.Web.UI.UserControl
    {
        public static string Ip { get; set; }
        public static string Username { get; set; }
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected DataTable dbdate;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.init())
            {
                Ip = Session["IP"].ToString();
                Username = Session["UserName"].ToString();
                Username = Session["UserName"].ToString();
            }
        }

        private bool init()
        {
            if (HttpContext.Current.Session["UserName"] == null || HttpContext.Current.Request.Cookies["username"] == null)
            {

                Response.Write("<script>alert(\"您还没有登陆或登陆已过期\");window.parent.location='../login.aspx';</script>");
                return false;
            }
            else return true;
        }

        /// <summary>
        /// 判断人员角色权限
        /// </summary>
        /// <returns></returns>
        public string ispartRights()
        {
            string Role = "";
            string username = HttpContext.Current.Request.Cookies["username"].Value;
            dbdate = hb.getdate("part", "userinfo where username='" + username + "'");
            if (dbdate.Rows.Count > 0)
            {


                Role = dbdate.Rows[0]["part"].ToString();

            }


            dbdate.Clear();
            return Role;

        }
        /// <summary>
        /// 判断人员角色权限 编码
        /// </summary>
        /// <returns></returns>
        public string isPartcode()
        {
            string Role = "";
            string username = HttpContext.Current.Request.Cookies["username"].Value;
            dbdate = hb.getdate("SP_Code", "userinfo where username='" + username + "'");
            if (dbdate.Rows.Count > 0)
            {
                Role = dbdate.Rows[0]["SP_Code"].ToString();
            }


            dbdate.Clear();
            return Role;

        }
        /// <summary>
        /// 获取城市编码吗 
        /// </summary>
        /// <returns></returns>
        public DataTable iscitycode()
        {

            string username = HttpContext.Current.Request.Cookies["username"].Value;
            DataTable dbdate = hb.getProdatable("SELECT Srcode FROM dbo.SYS_RightNew INNER JOIN dbo.userinfo ON spcode=SP_Code WHERE username='" + username + "'");




            return dbdate;

        }
        /// <summary>
        /// 当前人员账号
        /// </summary>
        /// <returns></returns>
        public static string userCode()
        {
            string username = "";

            if (HttpContext.Current.Request.Cookies["username"] != null)
            {
                username = HttpContext.Current.Request.Cookies["username"].Value;
            }



            return username;
        }
        /// <summary>
        /// 人员区域权限 
        /// </summary>
        /// <returns></returns>
        public string isDistrictRights()
        {
            string Role = "";
            string username = HttpContext.Current.Request.Cookies["username"].Value;

            dbdate = hb.getdate("area", "userinfo where username='" + username + "'");

            if (dbdate.Rows.Count > 0)
            {
                Role = dbdate.Rows[0]["area"].ToString();
            }
            dbdate.Clear();
            return Role;
        }

        /// <summary>
        /// 获取URL参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetQueryString(string key)
        {
            string Tmp = string.Empty;
            if (Request[key] != null)
            {
                Tmp = BasePage.FilterWhereStr(Request[key]);
            }
            return Tmp;
        }
        public static string FilterWhereStr(string inputstr)
        {
            if (inputstr.ToLower().IndexOf("declare") > -1 || inputstr.ToLower().IndexOf("script") > -1)
            {
                return "";
            }
            string[] f = new string[] { "?", ";", "\"", "'" };
            foreach (string fs in f)
            {
                inputstr = inputstr.Replace(fs, "");
            }
            return inputstr;
        }


        /// <summary>
        /// 匹配CAD
        /// </summary>
        /// <param name="cad"></param>
        /// <returns></returns>
        public bool GetRegexCAD(string cad)
        {
            //	CAD号必须满足： 前6位数字+“_”+3位数字，如：“350696_103”

            string[] str = cad.Split('_');

            if (str.Length > 0)
            {
                if (str[0].Length == 6 && str[1].Length == 3)
                {
                    return true;
                }
                else { return false; }

            }
            else { return false; }

        }
        /// <summary>
        /// c)	查货日期必须以这种规格存在 如：“20150607“
        /// </summary>
        /// <param name="cad"></param>
        /// <returns></returns>
        public bool GetRegexNum(string cad)
        {

            string pat = @"^\d{8}$";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);

            Match m = r.Match(cad);
            if (m.Length > 0)
            {
                return true;

            }
            else { return false; }

        }

    }
}
