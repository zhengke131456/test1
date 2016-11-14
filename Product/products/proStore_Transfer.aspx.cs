using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using productcommon;
using System.Web.Services;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.Runtime.Serialization.Json;

namespace product.products
{

    public partial class proStore_Transfer : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

        StringBuilder sqlstr = new StringBuilder();
        protected int n = 0;
        DataTable outtable = null;
        DataTable intable = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string str = Context.Request["outname"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    if (!IsPostBack)
                    {
                        productlist(str);
                        outname.Value = str;
                    }
                }
            }
            catch { }



            if (!IsPostBack)
            {

                // Hidddistrict.Value = isDistrictRights();
                Hidpart.Value = isPartcode();


                try
                {

                    bind();

                }
                catch { }
            }
        }


        private void bind()
        {
            DataTable dt;
            string sql = " (SELECT Basecode,basename FROM dbo.BaseStore INNER JOIN   dbo.SYS_RightStore  ON  Basecode=SR_storecode WHERE SP_code='" + Hidpart.Value + "')hh";
            dt = hb.getdate(" * ", sql);
            string szJson = GetJson(dt);
            innamelist.Value = szJson;
            intable = dt;

            sql = " (SELECT Basecode,basename FROM dbo.BaseStore)hh";
            dt = hb.getdate(" * ", sql);
            szJson = GetJson(dt);
            outnamelist.Value = szJson;
            outtable = dt;
        }
        /// <summary>
        /// 从表中获取json字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetJson(DataTable dt)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(dt.Rows[i]["basename"].ToString());
            }
            DataContractJsonSerializer json = new DataContractJsonSerializer(list.GetType());
            string szJson = ""; using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, list);
                szJson = Encoding.UTF8.GetString(stream.ToArray());
            }
            szJson = szJson.Replace("\"", "").Replace("[", "").Replace("]", "");
            return szJson;
        }
        protected void dpstore_SelectedIndexChanged(object sender, EventArgs e)
        {
            //productlist();
        }


        public void productlist(string storeCode)
        {

            //if (dpstore.SelectedItem.ToString() != "请选择")
            //{
            dpproduct.Items.Clear();
            if (!string.IsNullOrEmpty(storeCode))
            {

                //去掉库存为0的产品
                dpproduct.DataSource = hb.getdate(" rpcode,qty,WHCode,CONVERT(VARCHAR(20), rpcode) + ' 供应商编码：' + CAD+'  '+'        des'+pdes +'  数量:' + CONVERT(VARCHAR(20),QTY) as Name ", " (SELECT  h.rpcode,WHCode,qty,CAD,des AS 'pdes' FROM (SELECT rpcode ,stockcode as WHCode ,SUM(ISNULL(stockNum,0)) AS qty FROM dbo.stock_storck where stockcode=(select BaseCode from BaseStore where basename='" + storeCode + "')  GROUP BY rpcode,stockcode )h  LEFT JOIN   dbo.products ON  h.rpcode = dbo.products.rpcode )hh where QTY > 0");
                //string i = "select  rpcode,qty,WHCode,CONVERT(VARCHAR(20), rpcode) + ' 供应商编码：' + CAD+'  '+'        des'+pdes +'  数量:' + CONVERT(VARCHAR(20),QTY) as Name  from  (SELECT  h.rpcode,WHCode,qty,CAD,des AS 'pdes' FROM (SELECT rpcode ,stockcode as WHCode ,SUM(ISNULL(stockNum,0)) AS qty FROM dbo.stock_storck where stockcode=(select BaseCode from BaseStore where basename='杭州市睿配服务中心')    GROUP BY rpcode,stockcode )h  LEFT JOIN   dbo.products ON  h.rpcode = dbo.products.rpcode )hh where QTY > 0";
                //显示仓库所有的产品
                //dpproduct.DataSource = hb.getdate(" rpcode,qty,WHCode,CONVERT(VARCHAR(20), rpcode) + ' 供应商编码：' + CAD+'  '+'        des'+pdes +'  数量:' + CONVERT(VARCHAR(20),QTY) as Name ", " (SELECT  h.rpcode,WHCode,qty,CAD,des AS 'pdes' FROM (SELECT rpcode ,stockcode as WHCode ,SUM(ISNULL(stockNum,0)) AS qty FROM dbo.stock_storck where stockcode='" + storeCode + "'  GROUP BY rpcode,stockcode )h  LEFT JOIN   dbo.products ON  h.rpcode = dbo.products.rpcode )hh");
                dpproduct.DataTextField = "Name";
                dpproduct.DataValueField = "rpcode";
                dpproduct.DataBind();
                dpproduct.Items.Insert(0, "请选择");
            }
            else
            {
                dpproduct.Items.Clear();
            }
        }






        protected void BtnOK_Click(object sender, EventArgs e)
        {



            string instore = "";
            string outstore = "";
            int dbNum = 0; //调拨数量

            string rpcode = "", OutName = "", InName = "";
            OutName = outname.Value;
            InName = inname.Value;

            if (!string.IsNullOrEmpty(OutName))
            {
                outstore = OutName;
                if (dpproduct.SelectedItem.ToString() != "请选择")
                {
                    rpcode = dpproduct.SelectedItem.Value;
                    rpcode = dpproduct.SelectedValue;
                }
                if (Zhnum.Value != "")//调拨数量
                {
                    dbNum = Convert.ToInt32(Zhnum.Value);

                }
                if (!string.IsNullOrEmpty(InName))
                {
                    instore = InName;

                }
                if (!string.Equals(instore, "") && !string.Equals(outstore, "") && !string.Equals(rpcode, "") && dbNum > 0)
                {

                    string username = userCode();
                    sqlstr.Append("  INSERT INTO  dbo.transferslip( rpcode , QTY ,states ,outwh , inWH , opcode  )values('" + rpcode + "'," + dbNum + ",'待出库',(select Basecode from BaseStore where basename='" + outstore + "'),(select Basecode from BaseStore where basename='" + instore + "'),'" + username + "')");


                    //sqlstr.Append(" INSERT  INTO  dbo.inproduct  ( rpcode , QTY , WHCode ,spmark , usercode,statustype) values('" + rpcode + "'," + dbNum + ",'" + instore + "','4','" + username + "','调拨入库')");



                }

                if (sqlstr.Length > 0)
                {
                    if (hb.insetpro(sqlstr.ToString()))
                    {

                        bind();
                        dpproduct.Items.Clear();
                        Response.Write("<script type='text/javascript'>window.parent.alert('新建调拨单成功！');</script><script type='text/javascript'>window.onload = function () {document.getElementById(\"Zhnum\").value = \"\";};</script>");

                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'>window.parent.alert('新建调拨单失败！');</script>");
                    }
                }
                else
                {
                    Response.Write("<script type='text/javascript'>window.parent.alert('数据不能为空！');</script>");
                }
            }

            dpproduct.Items.Clear();
        }

    }
}