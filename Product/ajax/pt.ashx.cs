using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Data;

namespace product.ajax
{
    /// <summary>
    /// pt 的摘要说明
    /// </summary>
    public class pt : IHttpHandler
    {
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected string show = "2";

        public void ProcessRequest(HttpContext context)
        {
            string prentId = context.Request.Form["ParentID"];
            string show = "2";

            //首次加载。
            if (prentId == "99999")
            {
               
                sinfo.Sqlstr = " dbo.CityProvince ";

                sinfo.PageSize = 10000;

                DataSet ds = new DataSet();
                ds = hb.QXGetprodList(sinfo);

                context.Response.ContentType = "text/plain";
                List<CascadeModel> list = new List<CascadeModel>();

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CascadeModel() { Id = "", Name = "请选择省份", PId = prentId });
                    }
                    list.Add(new CascadeModel() { Id = ds.Tables[1].Rows[i]["ID"].ToString(), Name = ds.Tables[1].Rows[i]["name"].ToString(), PId = ds.Tables[1].Rows[i]["id"].ToString() });
                }

                string resultStr = JsonConvert.SerializeObject(list);

                context.Response.Write(resultStr);
            }
            else
            {
                sinfo.Sqlstr = " dbo.City ";

                sinfo.PageSize = 10000;

                //有父类
                if (!string.IsNullOrEmpty(prentId.ToString()) && prentId != "99999")
                {
                    sinfo.Sqlstr = " dbo.City where  provinceid =  '" + prentId + "'";
                }

                DataSet ds = new DataSet();
                ds = hb.QXGetprodList(sinfo);


                context.Response.ContentType = "text/plain";
                List<CascadeModel> list = new List<CascadeModel>();

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CascadeModel() { Id = "", Name = "请选择城市", PId = prentId });
                    }

                    list.Add(new CascadeModel() { Id = ds.Tables[1].Rows[i]["id"].ToString(), Name = ds.Tables[1].Rows[i]["cityname"].ToString(), PId = ds.Tables[1].Rows[i]["provinceid"].ToString() });
                }
                string resultStr = JsonConvert.SerializeObject(list);

                context.Response.Write(resultStr);
            }
           
           
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class CascadeModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string PId { get; set; }
        }

    }
}