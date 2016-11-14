using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.IO;
using ProductBLL.Basebll;

namespace product.upload
{
    public partial class UpPoductImage :Common.BasePage
    {
     

        string xiaotuFile = "", lunboFile = "", zhanshiFile = "", projectpath = "", absolutepath = "";
        protected string rpcode = "";
        protected BaseList hb = new BaseList();
        protected DataTable dbsource = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

            rpcode = GetQueryString("rpcode");

         

            //第一次请求执行
            if (!Page.IsPostBack)
            {
                #region 获取路径


                try
                {
                    projectpath = ConfigurationManager.AppSettings["projectpath"].ToString();
                    absolutepath = ConfigurationManager.AppSettings["absolutepath"].ToString();

                    xiaotuFile = ConfigurationManager.AppSettings["suolue"].ToString();
                    lunboFile = ConfigurationManager.AppSettings["lunbo"].ToString();
                    zhanshiFile = ConfigurationManager.AppSettings["zhanshi"].ToString();
                }
                catch
                {
                    projectpath = "http://127.0.0.1:8020/webHbulder/img/";
                    absolutepath = "C:\\Users\\work\\Desktop\\webHbulder\\img\\";
                    xiaotuFile = "liebiao";
                    lunboFile = "lunbo";
                    zhanshiFile = "zhanshi";

                }
                #endregion
                CreateSource();//显示图片
               
            }
            ListBind();
        }


       
        private void CreateSource()
        {

            dbsource.Columns.Add("rpcode");//rpcode
            dbsource.Columns.Add("ImageName");//显示的名字
            dbsource.Columns.Add("ImagePath");//图片URL路径
            dbsource.Columns.Add("ImageCode");//图片真实的名字
            dbsource.Columns.Add("absolutepath");//图片绝对路径 用来删除图片
            dbsource.Columns.Add("columnName");//列名  删除时用来修改图片
            DataTable db = hb.getdate(" *"," dbo.ProductImage WHERE ImgrRpcode='"+rpcode+"'");
            if (db.Rows.Count > 0)
            {

                #region 添加图片



               
                    ImageAdd("缩略图", xiaotuFile, db.Rows[0]["IconUrl"].ToString(), "IconUrl");
                    ImageAdd("轮播图片1", lunboFile, db.Rows[0]["ImageUrl1"].ToString(), "ImageUrl1");
                    ImageAdd("轮播图片2", lunboFile, db.Rows[0]["ImageUrl2"].ToString(), "ImageUrl2");
                    ImageAdd("轮播图片3", lunboFile, db.Rows[0]["ImageUrl3"].ToString(), "ImageUrl3");
                    ImageAdd("图片展示1", zhanshiFile, db.Rows[0]["Imgshow1"].ToString(), "Imgshow1");

                    ImageAdd("图片展示2", zhanshiFile, db.Rows[0]["Imgshow2"].ToString(), "Imgshow2");

                    ImageAdd("图片展示3", zhanshiFile, db.Rows[0]["Imgshow3"].ToString(), "Imgshow3");

                    ImageAdd("图片展示4", zhanshiFile, db.Rows[0]["Imgshow4"].ToString(), "Imgshow4");


            
                #endregion

            }
           

        }

        private void ImageAdd(string name, string path, string code, string columnName)
        {
            DataRow row = dbsource.NewRow();
            row["rpcode"] = rpcode;
            row["ImageName"] = name;
            row["ImagePath"] = projectpath + path + "/" + code;
            row["ImageCode"] = code;
            row["absolutepath"] = absolutepath + "\\\\\\" + path + "\\\\" + code;
            row["columnName"] = columnName;
            dbsource.Rows.Add(row);
        }

       
        
      

        public void ListBind()
        {

            this.DataList1.DataSource = dbsource;
            this.DataList1.DataBind();

           
       

        }


     

   
      

    }
           
}