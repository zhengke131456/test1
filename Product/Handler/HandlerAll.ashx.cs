using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace product.Handler
{
    /// <summary>
    /// HandlerAll 的摘要说明
    /// </summary>
    public class HandlerAll : IHttpHandler
    {

        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

        //i=0 成功；i=2 失败；i=4 系统错误，联系管理员；i=5 批量出库 数量不足；
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = "",ids="";
            string tablename = "";
            string states = "";
            string ErrorMess = "";
            string username = Common.BasePage.userCode();//用户名
            string partcode = Common.BasePage.userCode2();//用户角色 
            string outdate = "";
            string dotype = context.Request["dotype"].ToString(); ;//操作内容
            int i = 2;
            string mes = "";//提示消息.

            StringBuilder sb = new StringBuilder();

            #region //更新调拨状态(文字） 亚静
            if (dotype == "setdb")
            {
                i = 0;
                id = context.Request["id"].ToString();
                tablename = context.Request["tablename"].ToString();
                states = context.Request["states"].ToString();


                if (id == "" || tablename == "" || states == "")
                {
                    i = 4;
                }
                else //执行
                {
                    string sql = "update " + tablename + " set states = '" + states + "'  where id = '" + id + "'";

                    if (hb.ExeSql(sql))
                    {
                         i = 0;     
                    }
                    else i = 2;
                }
            }
            #endregion

            #region //批量更新调拨状态
            if (dotype == "setpdb")
            {
                ids = context.Request["ids"].ToString();
                tablename = context.Request["tablename"].ToString();
                states = context.Request["states"].ToString();

                i = 0;
                if (ids == "" || tablename == "" || states == "")
                {
                    i = 4;
                }
                else //执行
                {
                    string[] dr = ids.Split(new char[]{ ','});

                    for (int j = 0; j < dr.Length; j++)
                    {
                        if (dr[j] == "-1") continue;

                        sb.Append("update " + tablename + " set states = '" + states + "'  where id = '" + dr[j] + "' ;");
                    }

                    if (hb.ExeSql(sb.ToString()))
                    {
                        i = 0;
                    }
                    else i = 2;
                }
            }

            #endregion

            #region //批量出库 ""_id_ 数量
            if (dotype == "setpcdb")
            {
                ids = context.Request["ids"].ToString();
                tablename = context.Request["tablename"].ToString();
                states = context.Request["states"].ToString();
                outdate = context.Request["outdate"].ToString();

                i = 0;
                if (ids == "" || tablename == "" || states == "")
                {
                    i = 4;
                }
                else //执行
                {
                    string[] dr = ids.Split(new char[] { ',' });
                    //i=0 成功；i=2 失败；i=4 系统错误，联系管理员；i=5 批量出库 数量不足；i=6 没有出库操作权限


                    string batch = "";
                    string batchnum = "";

                    #region 批次号
                    //LT+R/C日期 +1
                    //当天导入 数字累加
                    //LTR20151120-1

                    int numbatch = 1;
                    string day = DateTime.Today.ToString("yyyy-MM-dd");
                    string Scalar = hb.GetScalarstring("  ISNULL(MAX(batchCount),'') ", " dbo.outproduct   where CONVERT(VARCHAR(20),insettime,23)='" + day + "'");

                    if (Scalar != "")
                    {
                        numbatch = Convert.ToInt32(Scalar) + 1;
                    }

                    //生成出库明细
                    batch ="LT";
                    batchnum = batch + "C" + day.Replace("-", "") + "-" + numbatch;

                    #endregion


                    for (int j = 0; j < dr.Length; j++)
                    {
                        string[] drr = dr[j].Split(new char[] { '_' });
                        if (drr[1] == "-1") continue;

                        #region 操作


                        //调拨明细
                        DataTable tdt = hb.GetDataSet("select * from [transferslip] where id = '" + drr[1] + "'").Tables[0];

                        //仓库明细
                        DataTable tdtst = hb.GetDataSet("select * from [BaseStore] ").Tables[0];

                        if (tdt.Rows.Count <= 0)
                        {
                            i = 2;
                            mes = "系统错误，联系管理员";
                            break;
                        }
                        else
                        {
                            string tstates = tdt.Rows[0]["states"].ToString();
                            int tQTY = int.Parse(tdt.Rows[0]["QTY"].ToString());


                            //是否有出库权限
                            DataTable temp_right = hb.GetDataSet("select * from [SYS_RightStore] where sp_code = '" + partcode + "' and SR_storecode='" + tdt.Rows[0]["outwh"].ToString() + "'").Tables[0];

                            if (temp_right.Rows.Count < 0)
                            {
                                i = 6;
                                mes = "没有出库仓库名为：" + tdt.Rows[0]["outwh"].ToString() + " 的操作权限！请核实";
                                break;
                            }


                            if (tstates != "待出库")
                            {
                                i = 2;
                                mes = "系统错误，联系管理员";
                                break;
                            }
                            else
                            {
                                //当前仓库库存是否满足
                                DataTable tdtck = hb.GetDataSet("select * from [stock_storck] where stockcode = '" + drr[3] + "' and rpcode='" + tdt.Rows[0]["rpcode"].ToString() + "'").Tables[0];
                                if (tdtck.Rows.Count <= 0)
                                {
                                    i = 2;
                                    mes = "系统错误，联系管理员";
                                    break;
                                }
                                else
                                {
                                    //真实库存
                                    int tck = int.Parse(tdtck.Rows[0]["stocknum"].ToString());

                                    if (tck < int.Parse(drr[2].ToString()))
                                    {
                                        i = 5;
                                        mes = "仓库 睿配编码为" + tdt.Rows[0]["rpcode"].ToString() + "：在仓库：" + drr[3].ToString() + "中库存不足 " + drr[2] + ",请与管理员联系! ";
                                        break;
                                    }
                                    else
                                    {
                                        // 1.更改调拨单状态
                                        sb.Append(" update transferslip set states ='待入库', outwhtime='" + outdate + "' , outopcode='" + username + "'  where id='" + drr[1] + "'  ;");

                                        //2.生成出库记录
                                        //出仓库 信息
                                        DataRow[] drs = tdtst.Select("basecode='" + tdt.Rows[0]["outwh"].ToString() + "'");
                                        string tstID = drs[0]["id"].ToString();

                                        //入仓库 信息
                                        DataRow[] drss = tdtst.Select("basecode='" + tdt.Rows[0]["inwh"].ToString() + "'");
                                        string tstIDO = drss[0]["basename"].ToString();

                                        //备注 调库仓库：石家庄睿配服务中心(sjz0001)调拨数量：4调入仓库：新华站(SNP03110004)
                                        string tstBZ = drs[0]["basename"].ToString() + "(" + tdt.Rows[0]["outwh"].ToString() + ") 调拨数量：" + drr[2].ToString() + "  调入仓库：" + tstIDO + "(" + tdt.Rows[0]["inwh"].ToString() + ")";

                                        sb.Append(" insert  into outproduct( spmark, rpcode,cad,od,qty,wh,whcode,tsnote,usercode,statustype,inbatch,batchcount ) "+
                                        "values('4','" + tdt.Rows[0]["rpcode"].ToString() + "','" + tdt.Rows[0]["cad"].ToString() + "','" + outdate + "','" + tdt.Rows[0]["qty"].ToString() + "','" + tstID + "','" + tdt.Rows[0]["outwh"].ToString() + "','" + tstBZ + "','" + username + "','调拨出库','" + batchnum + "','" + numbatch + "') ;");

                                        //3.扣减库存数，生成计提库存
                                        string sql3 = "update [stock_storck] set stockJtNum=stockJtNum+" + drr[2].ToString() + " ,stockNum=stockNum-" + drr[2].ToString() + " where rpcode='" + tdt.Rows[0]["rpcode"].ToString() + "' and stockcode='" + tdt.Rows[0]["outwh"].ToString() + "' ;";

                                        sb.Append(sql3);
                                    }

                                }
                            }
                        }

                        #endregion

                    }

                    if (i == 0)
                    {
                        if (hb.ExeSql(sb.ToString()))
                        {
                            i = 0;
                        }
                        else
                        {
                             i = 2;
                        }
                    }
                    
                }
            }

            #endregion

            #region //批量入库 ""_id_ 数量
            if (dotype == "setprdb")
            {
                i = 0;
                ids = context.Request["ids"].ToString();
                tablename = context.Request["tablename"].ToString();
                states = context.Request["states"].ToString();
                outdate = context.Request["outdate"].ToString();

                if (ids == "" || tablename == "" || states == "")
                {
                    i = 4;
                }
                else //执行
                {
                    string[] dr = ids.Split(new char[] { ',' });
                    //i=0 成功；i=2 失败；i=4 系统错误，联系管理员；i=5 批量出库 数量不足；


                    string batch = "";
                    string batchnum = "";

                    #region 批次号
                    //LT+R/C日期 +1
                    //当天导入 数字累加
                    //LTR20151120-1

                    int numbatch = 1;
                    string day = DateTime.Today.ToString("yyyy-MM-dd");
                    string Scalar = hb.GetScalarstring("  ISNULL(MAX(batchCount),'') ", " dbo.inproduct   where CONVERT(VARCHAR(20),insettime,23)='" + day + "'");

                    if (Scalar != "")
                    {
                        numbatch = Convert.ToInt32(Scalar) + 1;
                    }

                    //生成出库明细
                    batch = "LT";
                    batchnum = batch + "R" + day.Replace("-", "") + "-" + numbatch;

                    #endregion


                    for (int j = 0; j < dr.Length; j++)
                    {
                        string[] drr = dr[j].Split(new char[] { '_' });
                        if (drr[1] == "-1") continue;

                        #region 操作

                        //
                        i = 0;

                        //调拨明细
                        DataTable tdt = hb.GetDataSet("select * from [transferslip] where id = '" + drr[1] + "'").Tables[0];

                        //仓库明细
                        DataTable tdtst = hb.GetDataSet("select * from [BaseStore] ").Tables[0];

                        if (tdt.Rows.Count <= 0)
                        {
                            i = 2;
                            mes = "系统错误，联系管理员";
                            break;
                        }
                        else
                        {
                            string tstates = tdt.Rows[0]["states"].ToString();
                            int tQTY = int.Parse(tdt.Rows[0]["QTY"].ToString());

                            //是否有入库权限
                            DataTable temp_right = hb.GetDataSet("select * from [SYS_RightStore] where sp_code = '" + partcode + "' and SR_storecode='" + tdt.Rows[0]["inwh"].ToString() + "'").Tables[0];

                            if (temp_right.Rows.Count < 0)
                            {
                                i = 6;
                                mes = "没有入库仓库名为：" + tdt.Rows[0]["inwh"].ToString() + " 的操作权限！请核实";
                                break;
                            }


                            if (tstates != "待入库")
                            {
                                i = 2;
                                mes = "系统错误，联系管理员";
                                break;
                            }
                            else
                            {
                               
                                // 1.更改调拨单状态
                                sb.Append(" update transferslip set states ='调拨已完成', inwhtime='" + outdate + "' , inopcode='" + username + "'  where id='" + drr[1] + "'  ;");

                                //2.生成入库记录

                                //出仓库 信息
                                DataRow[] drs = tdtst.Select("basecode='" + tdt.Rows[0]["outwh"].ToString() + "'");
                                //string tstID = drs[0]["id"].ToString();

                                //入仓库 信息
                                DataRow[] drss = tdtst.Select("basecode='" + tdt.Rows[0]["inwh"].ToString() + "'");
                                string tstIDO = drss[0]["basename"].ToString();
                                string temp_stockID = drss[0]["id"].ToString();

                                //备注 调库仓库：石家庄睿配服务中心(sjz0001)调拨数量：4调入仓库：新华站(SNP03110004)
                                string tstBZ = drs[0]["basename"].ToString() + "(" + tdt.Rows[0]["outwh"].ToString() + ") 调拨数量：" + drr[2].ToString() + "  调入仓库：" + tstIDO + "(" + tdt.Rows[0]["inwh"].ToString() + ")";

                                sb.Append(" insert  into inproduct( spmark,rpcode,cad,shdate,qty,wh,whcode,note,usercode,statustype,inbatch,batchcount ) " +
                                "values('4','" + tdt.Rows[0]["rpcode"].ToString() + "','" + tdt.Rows[0]["cad"].ToString() + "','" + outdate + "','" + tdt.Rows[0]["qty"].ToString() + "','" + temp_stockID + "','" + tdt.Rows[0]["inwh"].ToString() + "','" + tstBZ + "','" + username + "','调拨入库','" + batchnum + "','" + numbatch + "') ;");

                                //3.入库仓库增加存数。(有记录与无记录）
                                DataTable temp_stock = hb.GetDataSet("select 1 from [stock_storck]  where stockcode='" + tdt.Rows[0]["inwh"].ToString() + "' and rpcode='" + tdt.Rows[0]["rpcode"].ToString() + "'").Tables[0];
                                string sql3= "";
                                

                                //有
                                if (temp_stock.Rows.Count > 0)
                                {
                                    sql3 = "update [stock_storck] set stockNum=stockNum+" + drr[2].ToString() + "  where rpcode='" + tdt.Rows[0]["rpcode"].ToString() + "' and stockcode='" + tdt.Rows[0]["inwh"].ToString() + "' ;";
                                }
                                else
                                {
                                    sql3 = "insert into [stock_storck]( rpcode,cad,desnote,stocknum,stockcode,stockid) " +
                                            "values('" + tdt.Rows[0]["rpcode"].ToString() + "','" + tdt.Rows[0]["cad"].ToString() + "','" + tdt.Rows[0]["rpcode"].ToString() + tdt.Rows[0]["inwh"].ToString() + "','" + tdt.Rows[0]["qty"].ToString() + "','" + tdt.Rows[0]["inwh"].ToString() + "','" + temp_stockID + "') ;";
                                }
                                sb.Append(sql3);

                                //4.出库仓库计提库，扣减。
                                string sql4 = "update [stock_storck] set stockJtNum=stockJtNum-" + drr[2].ToString() + "  where rpcode='" + tdt.Rows[0]["rpcode"].ToString() + "' and stockcode='" + tdt.Rows[0]["outwh"].ToString() + "' ;";
                                sb.Append(sql4);

                                hb.ExeSql(sb.ToString());
                                sb.Clear();
                                    
                            }
                        }

                        #endregion

                    }

                    //if (i == 0)
                    //{
                    //    if (hb.ExeSql(sb.ToString()))
                    //    {
                    //        i = 0;
                    //    }
                    //    else
                    //    {
                    //        i = 2;
                    //    }
                    //}

                }
            }

            #endregion

            #region  //setpEr 批量异常
            if (dotype == "setpEr")
            {
                i = 0;
                ids = context.Request["ids"].ToString();
                tablename = context.Request["tablename"].ToString();
                states = context.Request["states"].ToString();
                outdate = context.Request["outdate"].ToString();
                ErrorMess = context.Request["mess"].ToString();

                if (ids == "" || tablename == "" || states == "")
                {
                    i = 4;
                }
                else //执行
                {
                    string[] dr = ids.Split(new char[] { ',' });
                    //i=0 成功；i=2 失败；i=4 系统错误，联系管理员；i=5 批量出库 数量不足；

                    for (int j = 0; j < dr.Length; j++)
                    {
                        string[] drr = dr[j].Split(new char[] { '_' });
                        if (drr[1] == "-1") continue;

                        #region 操作

                        //
                        i = 0;

                        //调拨明细
                        DataTable tdt = hb.GetDataSet("select * from [transferslip] where id = '" + drr[1] + "'").Tables[0];

                        //仓库明细
                        DataTable tdtst = hb.GetDataSet("select * from [BaseStore] ").Tables[0];

                        if (tdt.Rows.Count <= 0)
                        {
                            i = 2;
                            mes = "系统错误，联系管理员";
                            break;
                        }
                        else
                        {
                            string tstates = tdt.Rows[0]["states"].ToString();
                            int tQTY = int.Parse(tdt.Rows[0]["QTY"].ToString());

                            //是否有出入库权限
                            if (states == "出库异常")
                            {
                                DataTable temp_right = hb.GetDataSet("select * from [SYS_RightStore] where sp_code = '" + partcode + "' and SR_storecode='" + tdt.Rows[0]["outwh"].ToString() + "'").Tables[0];

                                if (temp_right.Rows.Count < 0)
                                {
                                    i = 6;
                                    mes = "没有出库仓库名为：" + tdt.Rows[0]["outwh"].ToString() + " 的操作权限！请核实";
                                    break;
                                }
                            }
                            if (states == "入库异常")
                            {
                                DataTable temp_right = hb.GetDataSet("select * from [SYS_RightStore] where sp_code = '" + partcode + "' and SR_storecode='" + tdt.Rows[0]["inwh"].ToString() + "'").Tables[0];

                                if (temp_right.Rows.Count < 0)
                                {
                                    i = 6;
                                    mes = "没有入库仓库名为：" + tdt.Rows[0]["inwh"].ToString() + " 的操作权限！请核实";
                                    break;
                                }
                            }


                            if (tstates != "待入库" && tstates != "待出库")
                            {
                                i = 2;
                                mes = "系统错误，联系管理员";
                                break;
                            }
                            else
                            {
                                // 1.更改调拨单状态
                                if (states == "出库异常")
                                {
                                    sb.Append(" update transferslip set states ='" + states + "', OutErrorTime='" + outdate + "' , OutErrorMess='" + ErrorMess + "' ,outopcode='" + username + "'  where id='" + drr[1] + "'  ;");
                                }
                                if (states == "入库异常") 
                                {
                                    sb.Append(" update transferslip set states ='" + states + "', InErrorTime='" + outdate + "' , InErrorMess='" + ErrorMess + "' ,inopcode='" + username + "'  where id='" + drr[1] + "'  ;");
                                }
                            }
                        }

                        #endregion

                    }

                    if (i == 0)
                    {
                        if (hb.ExeSql(sb.ToString()))
                        {
                            i = 0;
                        }
                        else
                        {
                            i = 2;
                        }
                    }

                }
            }

            #endregion

            #region//setpcont 调拨继续执行

            if (dotype == "setpcont")
            {
                

                i = 0;
                ids = context.Request["ids"].ToString();
                tablename = context.Request["tablename"].ToString();
                //states = context.Request["states"].ToString();
                //outdate = context.Request["outdate"].ToString();
                //ErrorMess = context.Request["mess"].ToString();

                if (ids == "" || tablename == "" )
                {
                    i = 4;
                }
                else //执行
                {
                    string[] dr = ids.Split(new char[] { ',' });
                    //i=0 成功；i=2 失败；i=4 系统错误，联系管理员；i=5 批量出库 数量不足；

                    for (int j = 0; j < dr.Length; j++)
                    {
                        if (dr[j] == "-1") continue;

                        #region 操作

                        //
                        i = 0;

                        //调拨明细
                        DataTable tdt = hb.GetDataSet("select * from [transferslip] where id = '" + dr[j] + "'").Tables[0];

                        //仓库明细
                        DataTable tdtst = hb.GetDataSet("select * from [BaseStore] ").Tables[0];

                        if (tdt.Rows.Count <= 0)
                        {
                            i = 2;
                            mes = "系统错误，联系管理员";
                            break;
                        }
                        else
                        {
                            string tstates = tdt.Rows[0]["states"].ToString();
                            int tQTY = int.Parse(tdt.Rows[0]["QTY"].ToString());

                            //是否有出入库权限
                            if (tstates == "出库异常")
                            {
                                DataTable temp_right = hb.GetDataSet("select * from [SYS_RightStore] where sp_code = '" + partcode + "' and SR_storecode='" + tdt.Rows[0]["outwh"].ToString() + "'").Tables[0];

                                if (temp_right.Rows.Count < 0)
                                {
                                    i = 6;
                                    mes = "没有出库仓库名为：" + tdt.Rows[0]["outwh"].ToString() + " 的操作权限！请核实";
                                    break;
                                }
                            }
                            if (tstates == "入库异常")
                            {
                                DataTable temp_right = hb.GetDataSet("select * from [SYS_RightStore] where sp_code = '" + partcode + "' and SR_storecode='" + tdt.Rows[0]["inwh"].ToString() + "'").Tables[0];

                                if (temp_right.Rows.Count < 0)
                                {
                                    i = 6;
                                    mes = "没有入库仓库名为：" + tdt.Rows[0]["inwh"].ToString() + " 的操作权限！请核实";
                                    break;
                                }
                            }


                            if (tstates != "出库异常" && tstates != "入库异常")
                            {
                                i = 2;
                                mes = "系统错误，联系管理员";
                                break;
                            }
                            else
                            {
                                // 1.更改调拨单状态
                                if (tstates == "出库异常")
                                {
                                    sb.Append(" update transferslip set states ='待出库'  where id='" + dr[j] + "'  ;");
                                }
                                if (tstates == "入库异常")
                                {
                                    sb.Append(" update transferslip set states ='待入库'  where id='" + dr[j] + "'  ;");
                                }
                            }
                        }

                        #endregion

                    }

                    if (i == 0)
                    {
                        if (hb.ExeSql(sb.ToString()))
                        {
                            i = 0;
                        }
                        else
                        {
                            i = 2;
                        }
                    }

                }
            }
            #endregion

            #region//setpcont 调拨终止

            if (dotype == "setpstop")
            {
                i = 0;
                ids = context.Request["ids"].ToString();
                tablename = context.Request["tablename"].ToString();
                //states = context.Request["states"].ToString();
                //outdate = context.Request["outdate"].ToString();
                //ErrorMess = context.Request["mess"].ToString();

                if (ids == "" || tablename == "" )
                {
                    i = 4;
                }
                else //执行
                {
                    string[] dr = ids.Split(new char[] { ',' });
                    //i=0 成功；i=2 失败；i=4 系统错误，联系管理员；i=5 批量出库 数量不足；

                    for (int j = 0; j < dr.Length; j++)
                    {
                        if (dr[j] == "-1") continue;

                        #region 操作
                        //
                        i = 0;

                        //调拨明细
                        DataTable tdt = hb.GetDataSet("select * from [transferslip] where id = '" + dr[j] + "'").Tables[0];

                        //仓库明细
                        DataTable tdtst = hb.GetDataSet("select * from [BaseStore] ").Tables[0];

                        if (tdt.Rows.Count <= 0)
                        {
                            i = 2;
                            mes = "系统错误，联系管理员";
                            break;
                        }
                        else
                        {
                            string tstates = tdt.Rows[0]["states"].ToString();

                            //是否有出入库权限
                            if (tstates == "出库异常")
                            {
                                DataTable temp_right = hb.GetDataSet("select * from [SYS_RightStore] where sp_code = '" + partcode + "' and SR_storecode='" + tdt.Rows[0]["outwh"].ToString() + "'").Tables[0];

                                if (temp_right.Rows.Count < 0)
                                {
                                    i = 6;
                                    mes = "没有出库仓库名为：" + tdt.Rows[0]["outwh"].ToString() + " 的操作权限！请核实";
                                    break;
                                }
                            }
                            if (tstates == "入库异常")
                            {
                                DataTable temp_right = hb.GetDataSet("select * from [SYS_RightStore] where sp_code = '" + partcode + "' and SR_storecode='" + tdt.Rows[0]["inwh"].ToString() + "'").Tables[0];

                                if (temp_right.Rows.Count < 0)
                                {
                                    i = 6;
                                    mes = "没有入库仓库名为：" + tdt.Rows[0]["inwh"].ToString() + " 的操作权限！请核实";
                                    break;
                                }
                            }

                            if (tstates != "出库异常" && tstates != "入库异常")
                            {
                                i = 2;
                                mes = "系统错误，联系管理员";
                                break;
                            }
                            else
                            {
                                sb.Append(" update transferslip set states ='调拨失败'  where id='" + dr[j] + "'  ;");
                            }
                        }
                        #endregion
                    }

                    if (i == 0)
                    {
                        if (hb.ExeSql(sb.ToString()))
                        {
                            i = 0;
                        }
                        else
                        {
                            i = 2;
                        }
                    }

                }
            }
            #endregion

            if (i == 0) { mes = "批量成功"; }
            if (i == 2) { mes = "系统错误，联系管理员!"; }

            context.Response.Write("{\"result\":\"" + i + "\",\"mes\":\"" + mes + "\"}");
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