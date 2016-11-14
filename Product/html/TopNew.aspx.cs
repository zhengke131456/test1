using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace product.html
{
	public partial class TopNew : Common.BasePage
	{
		public string userName= "";
	
		protected void Page_Load(object sender, EventArgs e) {

			if (!IsPostBack) {
				userName = userCode();
			}

		}
		
	}
}