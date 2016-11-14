using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;

namespace product
{
    public partial class cktree :Common.UserControl
    {
        protected DataView dv_dep, dvjson;
        protected StringBuilder result;
        protected string flag;

        protected void Page_Load(object sender, EventArgs e)
        {
            hidtype.Value = this.Request.QueryString["id"];
            string partRights = isPartcode();//当前角色

            if (!Page.IsPostBack)
            {
                dvjson = hb.getProdatable("SELECT * FROM  dbo.BaseStore WHERE   Basecode  IN ( SELECT SR_storecode  FROM  dbo.SYS_RightStore where SP_code='" + partRights + "' )").DefaultView;//所有仓库

                result = new StringBuilder();
                result.Append("{id:\"0\",");


               //CreateTree("0");

               string ck1 = ConfigurationManager.AppSettings["ck1"].ToString();

               string[] sck1 = ck1.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

               for (int i = 0; i < sck1.Length; i++)
               {
                   string tid = sck1[i].Split(new char[] { '_' })[0];
                   string tname = sck1[i].Split(new char[] { '_' })[1];

                   dvjson.RowFilter = "Fcode='" + tid + "'";
                   string[] treeid = new string[dvjson.Count];

                   dvjson.RowFilter = "basecode='" + tid + "'";
                   string[] treeids = new string[dvjson.Count];

                   if (treeid.Length == 0 && treeids.Length==0) continue;

                   if (i == 0)
                   {
                       result.Append("item:[{id:\"" + tid + "\",text:\"" + tname + "\",");
                   }
                   else
                   {
                       result.Append("{id:\"" + tid + "\",text:\"" + tname + "\",");
                   }
                   CreateTree(tid);
                   //result.Append("}]");
               }

               if (result[result.Length - 1] == ',')
               {
                   result.Remove(result.Length - 1, 1);
               }

               result.Append("]}");

                string ss = result.ToString();

                ltljson.Text = "<script> var treejson=" + ss + "</script>";
            }
        }

        private void CreateTree(string fid)
        {
            
            
            
            dvjson.RowFilter = "Fcode='" + fid + "'";
            int i = 0;
            string[] treeid = new string[dvjson.Count];
            string[] treename = new string[dvjson.Count];
            bool flag = false;

            if (treeid.Length > 0)
            {
                result.Append("item:[");
                flag = true;
            }
            else
            {
                result.Remove(result.Length - 1, 1);
                result.Append("},");
            }


            for (; i < dvjson.Count; i++)
            {
                treeid[i] = dvjson[i]["Basecode"].ToString();
                treename[i] = dvjson[i]["basename"].ToString();
            }
            for (i = 0; i < treeid.Length; i++)
            {
                result.Append("{id:\"" + treeid[i] + "\",text:\"" + treename[i] + "\",");
                CreateTree(treeid[i]);
            }
            if (flag)
            {
                result.Remove(result.Length - 1, 1);
                result.Append("]},");
            }
        }
    }
}