using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Collections;
using System.Configuration;

namespace product.products
{
    public partial class Store_permutation : Common.BasePage
    {

        protected string cai = "";
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        //StringBuilder sqloutID = new StringBuilder();
        //StringBuilder sqlinID = new StringBuilder();
            StringBuilder sqlspin = new StringBuilder();
        StringBuilder sqlsql = new StringBuilder(); //
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //当前人员区域权限

                hiddpart.Value = ispartRights();
            }
           
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            bool fileIsValid = false;
            //如果确认了文件上传，则判断文件类型是否符合要求
            if (this.FileUpload1.HasFile)
            {
                //获取上传文件的后缀名
                String fileExtension = System.IO.Path.GetExtension(this.FileUpload1.FileName).ToLower();//ToLower是将Unicode字符的值转换成它的小写等效项
                
                //判断文件类型是否符合要求

                if (fileExtension == ".csv")
                {
                    fileIsValid = true;
                }
                else 
                {
                   Response.Write("<script type='text/javascript'>window.parent.alert('文件格式不正确!请上传正确格式的文件')</script>");
                   return;
                }
                
            }
            //如果文件类型符合要求，则用SaveAs方法实现上传，并显示信息
            if (fileIsValid == true)
            {
                try
                {
                    string name = Server.MapPath("~/uploadxls/") + "IN" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";
                    this.FileUpload1.SaveAs(name);
                    if (File.Exists(name))
                    {
                        int qty = 0; int numsl = 0;
                        DataTable dt = ProductBLL.Search.Searcher.OpenCSV(name);
                        if (dt.Rows.Count > 0)
                        {

                            string RKstorecode = "";
                            string CKstorecode = "";


                            #region  插入数据
                            int s = 0;
                            int storeID = 0, rkstoreID=0;
                            string username = userCode();
                            #region for补全产品编码

                            for (int k = 0; k < dt.Rows.Count; k++)
                            {
                                
                                if (dt.Rows[k]["产品编码"].ToString().Length < 6)
                                {
                                    //PadLeft
                                    dt.Rows[k]["产品编码"] = dt.Rows[k]["产品编码"].ToString().PadLeft(6, '0');
                                }
                            }
                            #endregion


                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                RKstorecode = dt.Rows[i]["仓库编码"].ToString();
                                CKstorecode = dt.Rows[i]["来源仓库编码"].ToString();
                                //仓库是否存在
                                if (hb.GetScalar("baseinfo where  type=4 AND (basecode='" + RKstorecode + "') ") != 0)
                                {
                                    if (hb.GetScalar("baseinfo where  type=4 AND (basecode='" + CKstorecode + "') ") != 0)
                                    {

                                        if (hiddpart.Value != "系统管理员")
                                        {
                                         
                                            #region 根据权限判断是否有该仓库权限

                                            //根据仓库名查找该仓库属于的区域
                                            string Area = hb.GetScalarstring("area", "baseinfo where  type=4  AND basecode='" + RKstorecode + "'");
                                            if (Area != hiddtype.Value)
                                            {

                                                s = 4;
                                               // sqlinID.Append(0);
                                                break;
                                            }

                                            #endregion
                                        }
                                        #region 数据操作

                                        //if (dt.Rows[i]["产品编码"].ToString().Length < 6)
                                        //{
                                        //    //不够6位数 左边补0
                                        //    dt.Rows[i]["产品编码"] = dt.Rows[i]["产品编码"].ToString().PadLeft(6, '0');
                                        //}

                                        //cai = dt.Rows[i]["产品编码"].ToString();

                                        #region  出库数是否大于库存数

                                        if (!sqlspin.ToString().Contains(dt.Rows[i]["产品编码"].ToString() + CKstorecode))
                                        {
                                            string ss = "产品编码='" + dt.Rows[i]["产品编码"].ToString() + "' and 来源仓库编码='" + CKstorecode + "'";
                                            DataRow[] sRow = dt.Select(ss);//规律execl下当前编码当前仓库的的数据
                                            if (sRow.Count() >= 0)
                                            {
                                                numsl = 0;
                                                for (int j = 0; j < sRow.Count(); j++)
                                                {
                                                    numsl = Convert.ToInt32(sRow[j]["数量"].ToString()) + numsl;
                                                }
                                            }

                                          storeID = hb.getproScalar("SELECT id FROM dbo.baseinfo WHERE [type]=4 AND Basecode='" + CKstorecode + "'");//仓库ID
                                          rkstoreID = hb.getproScalar("SELECT id FROM dbo.baseinfo WHERE [type]=4 AND Basecode='" + RKstorecode + "'");//ruk仓库ID
                                          qty = hb.getproScalar("SELECT stockNum FROM  dbo.stock_storck   INNER JOIN dbo.baseinfo  ON dbo.stock_storck.stockID= dbo.baseinfo.id WHERE  [type]=4 AND  Basecode='" + CKstorecode + "' AND CAI='" + dt.Rows[i]["产品编码"].ToString().Trim() + "'");

                                            if (qty < numsl)
                                            {
                                                s = 2;
                                                break;
                                                
                                            }
                                            sqlspin.Append(dt.Rows[i]["产品编码"].ToString() + CKstorecode);
                                        }

                                        #endregion
                                        
                                        #region 标识处理
                                            string tsbs = dt.Rows[i]["标识"].ToString().Trim();
                                            if (tsbs != "")
                                            {
                                                switch (tsbs)
                                                {
                                                    case "正常入库":
                                                        tsbs = "1";
                                                        break;
                                                    case "正常销售出库":
                                                        tsbs = "2";
                                                        break;
                                                    case "退货入库":
                                                        tsbs = "3";
                                                        break;
                                                    case "仓库置换":
                                                        tsbs = "4";
                                                        break;
                                                    case "货品内销":
                                                        tsbs = "5";
                                                        break;
                                                    case "特殊削减":
                                                        tsbs = "6";
                                                        break;
                                                    case "特殊出库":
                                                        tsbs = "7";
                                                        break;
                                                    default:
                                                        tsbs = "1";
                                                        break;
                                                }
                                            }
                                            #endregion

                                            #region 执行出口操作
                                            sqlsql.Append("UPDATE dbo.stock_storck  SET stockNum=stockNum-" + Convert.ToInt32(dt.Rows[i]["数量"].ToString()) + " ,datechange=GETDATE() WHERE  CAI='" + dt.Rows[i]["产品编码"].ToString() + "'AND  stockID =" + storeID + "  ");
                                            sqlsql.Append("INSERT  INTO dbo.outproduct ( CAI, OD, WHcode, QTY, spmark,TSNote,usercode,WH ) VALUES  ('" + dt.Rows[i]["产品编码"].ToString() + "', '" + dt.Rows[i]["置换日期"].ToString() + "', '" + CKstorecode + "'," + Convert.ToInt32(dt.Rows[i]["数量"].ToString()) + "," + tsbs + ",'仓库置换','" + username + "','" + storeID + "')");
                                            string note = dt.Rows[i]["产品编码"].ToString() + Convert.ToDateTime(dt.Rows[i]["置换日期"].ToString()).ToString("yyyy/MM/dd").Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + RKstorecode;
                                            //string sqlstr = "exec pro_instore  '" + dt.Rows[i]["产品编码"].ToString() + "','','','" + dt.Rows[i]["置换日期"].ToString() + "','','" + numsl + "','" + RKstorecode + "','" + Convert.ToInt32(tsbs) + "','" + note + "','0','' ,'" + userCode() + "' ";


                                        //入库
                                            if (hb.ProExecinset("SELECT TOP 1 1 FROM  dbo.stock_storck  WHERE stockID='" + rkstoreID + "'  AND CAI='" + dt.Rows[i]["产品编码"].ToString() + "'"))
                                            {
                                                sqlsql.Append(" UPDATE dbo.stock_storck  SET stockNum=stockNum+" + Convert.ToInt32(dt.Rows[i]["数量"].ToString()) + ",datechange=GETDATE() WHERE  stockID='" + rkstoreID + "' AND  CAI= '" + dt.Rows[i]["产品编码"].ToString() + "' ");

                                            }
                                            else
                                            {
                                               
                                                sqlsql.Append(" INSERT INTO dbo.stock_storck  ( CAI , desNote , stockNum ,Inserttime ,stockID , datechange ) VALUES('" + dt.Rows[i]["产品编码"].ToString() + "','" + note + "','" + Convert.ToInt32(dt.Rows[i]["数量"].ToString()) + "',GETDATE(),'" + rkstoreID + "',GETDATE()) ");
                                            }
                                        
                                            sqlsql.Append(" INSERT  INTO dbo.inproduct ( CAI ,SHDate ,QTY , WHCode , spmark , Note , inprice ,intype ,usercode,WH )VALUES('" + dt.Rows[i]["产品编码"].ToString() + "','" + dt.Rows[i]["置换日期"].ToString() + "','" + Convert.ToInt32(dt.Rows[i]["数量"].ToString()) + "','" + RKstorecode + "','" + tsbs + "','" + dt.Rows[i]["标识"].ToString() + "','0','','" + username + "','" + rkstoreID + "')");
                                            #endregion
                                        #region 历史版本
                                        /*    
                                       
                                        qty = hb.getproScalar("exec Pro_Query_Stocks '" + CKstorecode + "','" + cai.Trim() + "'");
                                        numsl = Convert.ToInt32(dt.Rows[i]["数量"].ToString());

                                        if (qty >= numsl)//如果库存数大于置换数 允许出库
                                        {
                                            #region 标识处理
                                            string tsbs = dt.Rows[i]["标识"].ToString().Trim();
                                            if (tsbs != "")
                                            {
                                                switch (tsbs)
                                                {
                                                    case "正常入库":
                                                        tsbs = "1";
                                                        break;
                                                    case "正常销售出库":
                                                        tsbs = "2";
                                                        break;
                                                    case "退货入库":
                                                        tsbs = "3";
                                                        break;
                                                    case "仓库置换":
                                                        tsbs = "4";
                                                        break;
                                                    case "货品内销":
                                                        tsbs = "5";
                                                        break;
                                                    case "特殊削减":
                                                        tsbs = "6";
                                                        break;
                                                    case "特殊出库":
                                                        tsbs = "7";
                                                        break;
                                                    default:
                                                        tsbs = "1";
                                                        break;
                                                }
                                            }
                                            #endregion
                                            string sqlstr = "exec pro_outproductZH_Details '" + cai.Trim() + "', '" + dt.Rows[i]["置换日期"].ToString() + "'," + numsl + ",'" + CKstorecode + "'," + tsbs + ",'" + userCode() + "' ";
                                            DataTable dtd = hb.getProdatable(sqlstr); //返回入库ID 
                                            if (dtd.Rows.Count > 0)
                                            {
                                                if (dtd.Rows[0][0].ToString() == "true" && dtd.Rows[0][1].ToString() == "true")
                                                {
                                                    s = 0; //成功

                                                    string maxlsh = hb.GetScalarstring("ISNULL(MAX(code),00000001)", "dbo.SYS_Code where [C_type]=1");
                                                    string outid = dtd.Rows[0][2].ToString();



                                                     *待解决： 如果置换出库10  需要两天入库信息 此时生成的置换入库时一条记录还是两条记录

                                                    string sqlin = "exec pro_inproductZH_Details  '" + outid + "', '" + cai.Trim() + "'," + numsl + ",'" + RKstorecode + "','" + CKstorecode + "'," + tsbs + ", '" + Convert.ToDateTime(dt.Rows[i]["置换日期"].ToString()) + "','" + maxlsh + "','" + userCode() + "'";
                                                    sqloutID.Append(dtd.Rows[0][2].ToString() + ',');


                                                    DataTable dtin = hb.getProdatable(sqlin); //返回入库ID 
                                                    if (dtin.Rows.Count > 0)
                                                    {
                                                        if (dtin.Rows[0][0].ToString() == "true" && dtin.Rows[0][1].ToString() == "true")
                                                        {
                                                            s = 0; //成功
                                                            string inid = dtin.Rows[0][2].ToString();
                                                            sqlinID.Append(inid + ',');
                                                        }
                                                        else
                                                        {
                                                            sqlinID.Append(dtin.Rows[0][2].ToString());
                                                            s = 1; //出库失败
                                                            break;
                                                        }
                                                    }

                                                    //生成入库

                                                }
                                                else if (dtd.Rows[0][0].ToString() == "false" && dtd.Rows[0][1].ToString() == "false")
                                                {

                                                    sqloutID.Append(dtd.Rows[0][2].ToString());
                                                    s = 1; //出库失败
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            sqlinID.Append(0);
                                            s = 2;//可用数不足
                                            break;
                                        }

                                        */
                                         #endregion
                                        #endregion

                                    }
                                    else
                                    {
                                        s = 3;//没有操作该仓库全新啊
                                        //sqlinID.Append(0);
                                        break;
                                    }

                                }
                                else
                                {
                                    s = 5; //没有出库仓库
                                    //sqlinID.Append(0);
                                    break;
                                }


                            }
                            #endregion


                            #region  根据数字解析相应结果
                            if (s == 0)
                            {
                                if (hb.ProExecinset(sqlsql.ToString()))
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../products/Store_permutation.aspx';</script>");

                                }
                                else
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.alert('上传失败');window.location.href='../products/Store_permutation.aspx';</script>");
                                }
                               
                            }
                            else if (s == 1)
                            {

                                Response.Write("<script type='text/javascript'>window.parent.alert('上传失败');window.location.href='../products/Store_permutation.aspx';</script>");
                            }
                            else if (s == 2)
                            {
                                Response.Write("<script type='text/javascript'>window.parent.alert('仓库可用数小于置换数:" + CKstorecode + "');window.location.href='../products/Store_permutation.aspx';</script>");
                            }
                            else if (s == 3)
                            {
                                Response.Write("<script type='text/javascript'>window.parent.alert('系统没有该入库仓库:" + RKstorecode + "');window.location.href='../products/Store_permutation.aspx';</script>");
                            }
                            else if (s == 5)
                            {
                                Response.Write("<script type='text/javascript'>window.parent.alert('系统没有该出库仓库:" + CKstorecode + "');window.location.href='../products/Store_permutation.aspx';</script>");
                            }
                            else if (s == 4)
                            {

                                Response.Write("<script type='text/javascript'>window.parent.alert('当前人员只能操作隶属于:" + hiddtype.Value + "区域的仓库数据');</script>");
                            }

                            #endregion


                            #region  历史版本  根据数字解析相应结果
                            /*
                                if (s == 0)
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../products/Store_permutation.aspx';</script>");
                                }
                                else
                                {

                                    //删除已经插入的数据
                                    if (hb.ProExecinset("DELETE dbo.inproduct WHERE  id IN(" + sqlinID.ToString() + ");DELETE inproductDetails WHERE ipd_ID IN(" + sqlinID.ToString() + ")") == true)
                                    {
                                        #region 返回结果



                                        if (s == 1)
                                        {
                                            //上传失败的话 要把已经上传成功的数据删除掉
                                            Response.Write("<script type='text/javascript'>window.parent.alert('上传失败,请删除已经上传的数据');window.location.href='../products/Store_permutation.aspx';</script>");
                                        }
                                        else if (s == 2)
                                        {
                                            Response.Write("<script type='text/javascript'>window.parent.alert('仓库可用数小于置换数:" + CKstorecode + "');window.location.href='../products/Store_permutation.aspx';</script>");
                                        }
                                        else if (s == 3)
                                        {
                                            Response.Write("<script type='text/javascript'>window.parent.alert('系统没有该仓库:" + RKstorecode + "');window.location.href='../products/Store_permutation.aspx';</script>");
                                        }
                                        else if (s == 5)
                                        {
                                            Response.Write("<script type='text/javascript'>window.parent.alert('系统没有该仓库:" + CKstorecode + "');window.location.href='../products/Store_permutation.aspx';</script>");
                                        }
                                        else if (s == 4)
                                        {

                                            Response.Write("<script type='text/javascript'>window.parent.alert('当前人员只能操作隶属于:" + hiddtype.Value + "区域的仓库数据');</script>");
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        Response.Write("<script type='text/javascript'>window.parent.alert('上传失败,请删除已经上传的数据');window.location.href='../products/Store_permutation.aspx';</script>");


                                    }



                               
                            */
                            #endregion
                        }

                        else 
                        {
                            Response.Write("<script type='text/javascript'>window.parent.alert('文件无内容');</script>");
                            //sqlpin.Clear();
                            return;
                        }
                    }
                
                }
                catch 
                {
                    Response.Write("<script type='text/javascript'>window.parent.alert('文件内容格式不正确，请核实')</script>");
                    return;
                }
                finally
                {
                    sqlspin.Clear();
                    sqlsql.Clear();
                }
            } 
        }
      
    }
}