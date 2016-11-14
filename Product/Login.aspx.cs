using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using product.Common;
namespace product
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string IPaddress = BasePage.GetAddressIP();
            if (HttpContext.Current.Request.Cookies[IPaddress] != null)
            {
                HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[IPaddress];
                username.Value = Cookie.Value.ToString();
                Session["IP"] = IPaddress;
                Session["UserName"] = Cookie.Value.ToString();
                chb.Checked = true;
            }
        }
    }
}