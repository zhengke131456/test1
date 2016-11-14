using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Data;

namespace product.ajax
{
    /// <summary>
    /// CascadeSelect 的摘要说明
    /// </summary>
    public class CascadeSelect : IHttpHandler
    {
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected string show = "2";

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            //context.Response.ContentType = "text/plain";
            //List<CascadeModel> list = new List<CascadeModel>();

            //list.Add(new CascadeModel() { Id = 1, Name = "全国", PId = 0 });
            //list.Add(new CascadeModel() { Id = 25, Name = "海外", PId = 0 });

            //list.Add(new CascadeModel() { Id = 2, Name = "河北", PId = 1 });
            //list.Add(new CascadeModel() { Id = 3, Name = "河南", PId = 1 });
            //list.Add(new CascadeModel() { Id = 4, Name = "湖北", PId = 1 });
            //list.Add(new CascadeModel() { Id = 5, Name = "湖南", PId = 1 });

            //list.Add(new CascadeModel() { Id = 6, Name = "河北-市xx", PId = 2 });
            //list.Add(new CascadeModel() { Id = 7, Name = "河北-市oo", PId = 2 });
            //list.Add(new CascadeModel() { Id = 8, Name = "河南-市xx", PId = 3 });
            //list.Add(new CascadeModel() { Id = 9, Name = "河南-市oo", PId = 3 });
            //list.Add(new CascadeModel() { Id = 10, Name = "湖北-市xx", PId = 4 });
            //list.Add(new CascadeModel() { Id = 11, Name = "湖北-市oo", PId = 4 });
            //list.Add(new CascadeModel() { Id = 12, Name = "长沙", PId = 5 });
            //list.Add(new CascadeModel() { Id = 13, Name = "益阳", PId = 5 });


            //list.Add(new CascadeModel() { Id = 14, Name = "河北-市xx县xx", PId = 6 });
            //list.Add(new CascadeModel() { Id = 15, Name = "河北-市xx县xx", PId = 6 });
            //list.Add(new CascadeModel() { Id = 16, Name = "河北-市xx县xx", PId = 6 });
            //list.Add(new CascadeModel() { Id = 17, Name = "河北-市xx县xx", PId = 6 });

            //list.Add(new CascadeModel() { Id = 18, Name = "河北-市oo-县oo", PId = 7 });
            //list.Add(new CascadeModel() { Id = 19, Name = "河北-市oo-县oo", PId = 7 });
            //list.Add(new CascadeModel() { Id = 20, Name = "河北-市oo-县oo", PId = 7 });
            //list.Add(new CascadeModel() { Id = 21, Name = "河北-市oo-县oo", PId = 7 });

            //list.Add(new CascadeModel() { Id = 22, Name = "益阳-资阳区", PId = 13 });
            //list.Add(new CascadeModel() { Id = 23, Name = "益阳-赫山区", PId = 13 });
            //list.Add(new CascadeModel() { Id = 24, Name = "益阳-桃江县", PId = 13 });


            //int prentId = Convert.ToInt32(context.Request.Form["ParentID"]);
            //List<CascadeModel> newList = list.Where<CascadeModel>(t => t.PId == prentId).ToList<CascadeModel>();
            //string resultStr = JsonConvert.SerializeObject(newList);
            //context.Response.Write(resultStr);


            

            //
            string prentId = context.Request.Form["ParentID"];
            string show = "2";

            //首次加载。
            if (prentId == "99999")
            {
                string partRights = isPartcode();
                sinfo.Sqlstr = " dbo.BaseStore WHERE   Basecode  IN ( SELECT SR_storecode  FROM  dbo.SYS_RightStore where SP_code='" + partRights + "' ) ";

                sinfo.PageSize = 10000;
                sinfo.Orderby = "type";

                DataSet ds1 = new DataSet();
                ds1 = hb.QXGetprodList(sinfo);
                DataTable dt = ds1.Tables[1];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Fcode"].ToString() == "0")
                    {
                        show = "3";
                        break;
                    }
                }

                if (show == "3")
                {
                    sinfo.Sqlstr = " dbo.BaseStore WHERE   Basecode  IN ( SELECT SR_storecode  FROM  dbo.SYS_RightStore where SP_code='" + partRights + "' and nodelevel=1 ) ";
                }
                else
                {
                    sinfo.Sqlstr = " dbo.BaseStore WHERE   Basecode  IN ( SELECT SR_storecode  FROM  dbo.SYS_RightStore where SP_code='" + partRights + "' and nodelevel=2 ) ";
                }
                //有父类
                if (!string.IsNullOrEmpty(prentId.ToString()) && prentId != "99999")
                {
                    sinfo.Sqlstr += " and  fcode =  '" + prentId + "'";
                }

                DataSet ds = new DataSet();
                ds = hb.QXGetprodList(sinfo);


                context.Response.ContentType = "text/plain";
                List<CascadeModel> list = new List<CascadeModel>();

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CascadeModel() { Id = "", Name = "请选择", PId = prentId });
                    }
                    list.Add(new CascadeModel() { Id = ds.Tables[1].Rows[i]["basecode"].ToString(), Name = ds.Tables[1].Rows[i]["basename"].ToString(), PId = ds.Tables[1].Rows[i]["fcode"].ToString() });
                }

                //List<CascadeModel> newList = list.Where<CascadeModel>(t => t.PId == prentId).ToList<CascadeModel>();
                //string resultStr = JsonConvert.SerializeObject(newList);

                string resultStr = JsonConvert.SerializeObject(list);

                context.Response.Write(resultStr);
            }
            else
            {
                string partRights = isPartcode();
                sinfo.Sqlstr = " dbo.BaseStore WHERE   Basecode  IN ( SELECT SR_storecode  FROM  dbo.SYS_RightStore where SP_code='" + partRights + "' ) ";

                sinfo.PageSize = 10000;
                sinfo.Orderby = "type";

                
                sinfo.Sqlstr = " dbo.BaseStore WHERE   Basecode  IN ( SELECT SR_storecode  FROM  dbo.SYS_RightStore where SP_code='" + partRights + "'  ) ";
               
                //有父类
                if (!string.IsNullOrEmpty(prentId.ToString()) && prentId != "99999")
                {
                    sinfo.Sqlstr += " and  fcode =  '" + prentId + "'";
                }

                DataSet ds = new DataSet();
                ds = hb.QXGetprodList(sinfo);


                context.Response.ContentType = "text/plain";
                List<CascadeModel> list = new List<CascadeModel>();

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        list.Add(new CascadeModel() { Id = "", Name = "请选择", PId = prentId });
                    }
                    
                    list.Add(new CascadeModel() { Id = ds.Tables[1].Rows[i]["basecode"].ToString(), Name = ds.Tables[1].Rows[i]["basename"].ToString(), PId = ds.Tables[1].Rows[i]["fcode"].ToString() });
                }
                string resultStr = JsonConvert.SerializeObject(list);

                context.Response.Write(resultStr);
            }
           
           


           // List<CascadeModel> newList = list.Where<CascadeModel>(t => t.PId == prentId).ToList<CascadeModel>();
           // string resultStr = JsonConvert.SerializeObject(newList);


           // List<CascadeModel> newList = list.Where<CascadeModel>(t => t.PId == prentId).ToList<CascadeModel>();
            

        }

        /// <summary>
        /// 判断人员角色权限 编码
        /// </summary>
        /// <returns></returns>
        public string isPartcode()
        {
            string Role = "";
            string username = HttpContext.Current.Request.Cookies["username"].Value;
            DataTable dbdate = new DataTable();
            dbdate = hb.getdate("SP_Code", "userinfo where username='" + username + "'");
            
            if (dbdate.Rows.Count > 0)
            {
                Role = dbdate.Rows[0]["SP_Code"].ToString();
            }


            dbdate.Clear();
            return Role;

        }

        public class CascadeModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string PId { get; set; }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}