using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using System.IO.Compression;
using System.Net.Mail;
using System.Data.OleDb;
namespace product.Common
{
    public class BasePage : System.Web.UI.Page
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
        /// 当前人员角色
        /// </summary>
        /// <returns></returns>
        public static string userCode2()
        {
            string Role = "";
            string username = HttpContext.Current.Request.Cookies["username"].Value;
              ProductBLL.Basebll.BaseList hb1 = new ProductBLL.Basebll.BaseList();

            DataTable dt1  = hb1.getdate("SP_Code", "userinfo where username='" + username + "'");
            if (dt1.Rows.Count > 0)
            {
                Role = dt1.Rows[0]["SP_Code"].ToString();
            }

            dt1.Clear();
            return Role;
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
        /// 分页按钮
        /// </summary>
        /// <param name="currentUrl"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public string GenPaginationBar(string currentUrl, int pageSize, int currentPage, int totalRecord)
        {
            int pageCount = 0;
            if (totalRecord > 0)
            {
                pageCount = (totalRecord - 1) / pageSize + 1;
            }
            if (currentPage <= 0) currentPage = 1;
            StringBuilder sb = new StringBuilder();
            int linkNum = 10;
            if (totalRecord > 0)
            {
                int i = 0;
                if (currentPage >= linkNum && currentPage <= linkNum * 10)
                    linkNum = (int)(linkNum / 1.5);
                else if (currentPage > linkNum * 10 && currentPage <= linkNum * 100)
                    linkNum = (int)(linkNum / 2);
                else if (currentPage > linkNum * 100)
                    linkNum = (int)(linkNum / 4);
                sb.Append(@"<div class='PagesCount'>第<span class='ye'>" + currentPage + "</span>页 共<span class='ye'>" + pageCount.ToString() + "</span>页 </div>");
                string prev = @"<a href='" + GeneratePageLink(currentUrl, 1) + "' class='ChangePage'>首页</a><a href='" + GeneratePageLink(currentUrl, currentPage - 1) + "' class='ChangePage'>&lt;上一页</a>";
                string next = @"<a href='" + GeneratePageLink(currentUrl, currentPage + 1) + "' class='ChangePage'>下一页&gt;</a><a class='ChangePage' href='" + GeneratePageLink(currentUrl, pageCount) + "'>尾页</a> ";
                if (currentPage == 1)
                    prev = @"<a href='#' style='display:none;' class='ChangePage'>&lt;上一页</a>";
                if (currentPage >= pageCount)
                    next = @"<a href='#' style='display:none;' class='ChangePage'>下一页&gt;</a>";
                sb.Append("<div class='Pages_List'>");
                sb.Append(prev);
                if (currentPage < 10)
                {
                    int temp;
                    if (pageCount < 10)
                        temp = pageCount;
                    else
                        temp = 10;
                    for (i = 1; i <= temp; i++)
                    {
                        if (i != currentPage)
                            sb.Append(@"<a href='" + GeneratePageLink(currentUrl, i) + "'>" + i.ToString() + "</a>");
                        else
                            sb.Append(@"<span class='this-page'>" + i.ToString() + "</span>");
                    }
                }
                else if (currentPage >= 10 && currentPage <= pageCount - (linkNum / 2))
                {
                    for (i = currentPage - (linkNum / 2); i <= currentPage + (linkNum / 2); i++)
                    {
                        if (i == currentPage)
                            sb.Append(@"<span class='this-page'>" + currentPage.ToString() + "</span>");
                        else
                            sb.Append(@"<a href='" + GeneratePageLink(currentUrl, i) + "'>" + i.ToString() + "</a>");
                    }
                }
                else
                {
                    for (i = linkNum - 1; i >= 0; i--)
                    {
                        if (pageCount - i != currentPage)
                        {
                            sb.Append(@"<a href='" + GeneratePageLink(currentUrl, pageCount - i) + "'>" + (pageCount - i).ToString() + "</a>");
                        }
                        else
                            sb.Append(@"<span class='this-page'>" + currentPage.ToString() + "</span>");
                    }
                }
                sb.Append(next);
                sb.Append("</div>");
                //跳转
                string gotoString = "<div class='Pages_goto'>转到第<input type=\"text\"  id=\"gopage\" maxlength=\"5\" size=\"2\" />页     <input  type=\"image\" id=\"gopage1\" src=\"../images/page_go.gif\" value=\"go\"  onclick=\"location.href=('" + currentUrl + "').replace('[page]',document.getElementById('gopage').value);return false;\" /> </div>";
                sb.Append(gotoString);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成分页页面链接
        /// </summary>
        /// <param name="currentUrl">页面地址</param>
        /// <param name="page">要指向的页码</param>
        /// <returns>链接地址</returns>
        public string GeneratePageLink(string currentUrl, int page)
        {
            return currentUrl.Replace("[page]", page.ToString());
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

        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="sendtousername">用户账号</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="message">邮件内容</param>
        public void SendMail163(string sendtousername, string subject, string message)
        {


            DataTable dtuser = hb.GetDataSet("select * from userinfo where username='" + sendtousername + "'").Tables[0];

            if (dtuser.Rows.Count == 0) return;


            string sd = dtuser.Rows[0]["email"].ToString();

            try
            {

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.163.com");

                //smtp.Credentials = new System.Net.NetworkCredential("zhengke131456", "miaomiao0724");
                //smtp.Credentials = new System.Net.NetworkCredential("systemadminerp", "miaomiao0724");
                //cbzlmwopqxfisnjn 邮箱授权码 注意：126或者163 邮箱目前必须开通 客户端授权密码才能开通Smtp 服务
                //  此时邮箱身份验证的密码 必须换成授权码才能登陆

                smtp.Credentials = new System.Net.NetworkCredential("erpsystemadmin", "cbzlmwopqxfisnjn");

                System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage();
                // Message.From = new System.Net.Mail.MailAddress("zhengke131456@163.com");

                Message.From = new System.Net.Mail.MailAddress("erpsystemadmin@163.com");
                Message.To.Add(sd);

                Message.Subject = subject;
                Message.Body = message;
                Message.SubjectEncoding = System.Text.Encoding.UTF8;
                Message.BodyEncoding = System.Text.Encoding.UTF8;
                Message.Priority = System.Net.Mail.MailPriority.High;
                //Message.IsBodyHtml = true;


                smtp.Send(Message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        public static string GetAddressIP()
        {
            ///获取本地的IP地址

            string user_IP = string.Empty;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    user_IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    user_IP = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
            }
            else
            {
                user_IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            //string AddressIP = string.Empty;
            //foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList) {
            //    if (_IPAddress.AddressFamily.ToString() == "InterNetwork") {
            //        AddressIP = _IPAddress.ToString();
            //    }
            //}
            return user_IP;

        }

        /// <summary>
        /// 判断是否是市级关系
        /// </summary>
        /// <returns></returns>
        public  bool CheckStroeIsLevel( string twoLname,string threeLname)
        {
            bool bl = false;

            DataTable dbdate = hb.getdate("basecode,fcode,nodelevel", " BaseStore ");

            DataRow[] dr = dbdate.Select("basecode='" + twoLname + "' and fcode='" + threeLname + "' and nodelevel='" + 3 + "'");
            if (dr.Length > 0) bl = true;

            DataRow[] drr = dbdate.Select("basecode='" + threeLname + "' and fcode='" + twoLname + "' and nodelevel='" + 3 + "'");
            if (drr.Length > 0) bl = true;

            return bl;
        }

        /// <summary>
        /// 读EXECL
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";

            //string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties=Excel 12.0;HDR=YES;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds;
        }

    }
}
