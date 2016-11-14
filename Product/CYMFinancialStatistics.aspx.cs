using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Mod;
using Newtonsoft.Json;

namespace CheYiMaAdmin
{
    public partial class CYMFinancialStatistics : System.Web.UI.Page
    {
        public CYMUsersMessagesBll CYMUsersMessagesBll { get; set; }
        public CYMUsersCarMessageBll CYMUsersCarMessageBll { get; set; }

        public CYMUsersBll CymUsersBll { get; set; }

        public CYMGarageBll CymGarageBll { get; set; }

        public CheBaoCarCompositeBll CheBaoCarCompositeBll { get; set; }
        public CheBaoCarLineBll CheBaoCarLineBll { get; set; }

        public GOODSParameterDomeBll GOODSParameterDomeBll { get; set; }
        public CheBaoCarBrandBll CheBaoCarBrandBll { get; set; }

        public CYMOWMerchantLoginBll CymowMerchantLoginBll { get; set; }
        public CYMOWMerchantUsersInnerCYMMerchantBll CymowMerchantUsersInnerCymMerchantBll { get; set; }
        public NewCYMUsersFabricOrderBll NewCYMUsersFabricOrderBll { get; set; }

        public NewCYMUsersShoppingInDCBll NewCymUsersShoppingInDcBll { get; set; }
        public NewShoppingCYMUsersMessageBll NewShoppingCymUsersMessageBll { get; set; }
        public T_Cm_CodeBll TCmCodeBll { get; set; }
        public T_Cm_TypeTreeBll TypeTreeBll { get; set; }

        public CYMMerchantBll CymMerchantBll { get; set; }

        public CYMMerchantComBoBll CymMerchantComBoBll { get; set; }

        public CYMClerkInfoBll CymClerkInfoBll { get; set; }
        public ClerkLogInfoBll ClerkLogInfoBll { get; set; }
        public CYMClerkInfo CymClerkInfo { get; set; }
        public ClerkAndOrderBll ClerkAndOrderBll { get; set; }

        public CYMTakeOutBll CymTakeOutBll { get; set; }
        public string TextDiv;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AdminLogin())
            {
                Response.Redirect("/login.html");
            }
            if (Request.ServerVariables["REQUEST_METHOD"] == "POST")
            {
                LoadBllEntity();
                if (string.IsNullOrEmpty(Request["Item"]))
                {
                    Response.Write("发生异常，请刷新重试");
                    Response.End();
                }
                else
                {
                    string item = Request["Item"];
                    switch (item)
                    {
                        case "GetFinancialStatistics":
                            GetFinancialStatistics();
                            break;
                        case "DCGetFinancialStatistics":
                            DCGetFinancialStatistics();
                            break;
                        case "EditFinanceStatusTo1":
                            EditFinanceStatusTo1();
                            break;
                        case "EditFinanceStatusTo2":
                            EditFinanceStatusTo2();
                            break;
                        case "EditFinanceStatusTo3":
                            EditFinanceStatusTo3();
                            break;
                        case "EditFinanceStatusTo0":
                            EditFinanceStatusTo0();
                            break;
                        case "SearchFinancialStatistics":
                            SearchFinancialStatistics();
                            break;
                        case "SearchFinancialStatisticsOrderListInfo":
                            SearchFinancialStatisticsOrderListInfo();
                            break;
                        case "EditFinanceRemark":
                            EditFinanceRemark();
                            break;
                        case "EditServerRemark":
                            EditServerRemark();
                            break;
                        case "EditCarMileage":
                            EditCarMileage();
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 更改车辆里程
        /// </summary>
        private void EditCarMileage()
        {
            float CarMileage = float.Parse(string.IsNullOrEmpty(Request["CarMileage"]) ? "0" : Request["CarMileage"]);
            int NewCYMUsersFabricOrderId = int.Parse(Request["NewCYMUsersFabricOrderId"]);
            ClerkAndOrder clerkAndOrder = ClerkAndOrderBll.LoadEntities(c => c.NewCYMUsersFabricOrderId == NewCYMUsersFabricOrderId).FirstOrDefault();
            if (clerkAndOrder == null)
            {
                clerkAndOrder = new ClerkAndOrder()
                {
                    CarMileage = CarMileage,
                    ClerkId = 0,
                    ClerkName = "无",
                    CYMVoucherFaceValue = 0,
                    FinanceStatus = 0,
                    FinanceRemark = "",
                    NewCYMUsersFabricOrderId = NewCYMUsersFabricOrderId,
                    PaySours = "未知",
                    ServerRemark = ""
                };
                clerkAndOrder = ClerkAndOrderBll.AddEntity(clerkAndOrder);
                if (clerkAndOrder.id > 0)
                {
                    Response.Write("ok");
                    Response.End();
                }
                else
                {
                    Response.Write("no");
                    Response.End();
                }
            }
            else
            {
                clerkAndOrder.CarMileage = CarMileage;
                if (ClerkAndOrderBll.UpdateEntity(clerkAndOrder))
                {
                    Response.Write("ok");
                    Response.End();
                }
                else
                {
                    Response.Write("no");
                    Response.End();
                }

            }
        }
        /// <summary>
        /// 更新服务备注
        /// </summary>
        private void EditServerRemark()
        {
            string ServerRemark = string.IsNullOrEmpty(Request["ServerRemark"]) ? "" : Request["ServerRemark"];
            int NewCYMUsersFabricOrderId = int.Parse(Request["NewCYMUsersFabricOrderId"]);
            ClerkAndOrder clerkAndOrder = ClerkAndOrderBll.LoadEntities(c => c.NewCYMUsersFabricOrderId == NewCYMUsersFabricOrderId).FirstOrDefault();
            if (clerkAndOrder == null)
            {
                clerkAndOrder = new ClerkAndOrder()
                {
                    CarMileage = 0,
                    ClerkId = 0,
                    ClerkName = "无",
                    CYMVoucherFaceValue = 0,
                    FinanceStatus = 0,
                    FinanceRemark = "",
                    NewCYMUsersFabricOrderId = NewCYMUsersFabricOrderId,
                    PaySours = "未知",
                    ServerRemark = ServerRemark
                };
                clerkAndOrder = ClerkAndOrderBll.AddEntity(clerkAndOrder);
                if (clerkAndOrder.id > 0)
                {
                    Response.Write("ok");
                    Response.End();
                }
                else
                {
                    Response.Write("no");
                    Response.End();
                }
            }
            else
            {
                clerkAndOrder.ServerRemark = ServerRemark;
                if (ClerkAndOrderBll.UpdateEntity(clerkAndOrder))
                {
                    Response.Write("ok");
                    Response.End();
                }
                else
                {
                    Response.Write("no");
                    Response.End();
                }

            }
        }
        /// <summary>
        /// 更新财务备注
        /// </summary>
        private void EditFinanceRemark()
        {
            string FinanceRemark = string.IsNullOrEmpty(Request["FinanceRemark"]) ? "" : Request["FinanceRemark"];
            int NewCYMUsersFabricOrderId = int.Parse(Request["NewCYMUsersFabricOrderId"]);
            ClerkAndOrder clerkAndOrder = ClerkAndOrderBll.LoadEntities(c => c.NewCYMUsersFabricOrderId == NewCYMUsersFabricOrderId).FirstOrDefault();
            if (clerkAndOrder == null)
            {
                clerkAndOrder = new ClerkAndOrder()
                {
                    CarMileage = 0,
                    ClerkId = 0,
                    ClerkName = "无",
                    CYMVoucherFaceValue = 0,
                    FinanceStatus = 0,
                    FinanceRemark = FinanceRemark,
                    NewCYMUsersFabricOrderId = NewCYMUsersFabricOrderId,
                    PaySours = "未知",
                    ServerRemark = ""
                };
                clerkAndOrder = ClerkAndOrderBll.AddEntity(clerkAndOrder);
                if (clerkAndOrder.id > 0)
                {
                    Response.Write("ok");
                    Response.End();
                }
                else
                {
                    Response.Write("no");
                    Response.End();
                }
            }
            else
            {
                clerkAndOrder.FinanceRemark = FinanceRemark;
                if (ClerkAndOrderBll.UpdateEntity(clerkAndOrder))
                {
                    Response.Write("ok");
                    Response.End();
                }
                else
                {
                    Response.Write("no");
                    Response.End();
                }

            }
        }
        /// <summary>
        /// 根据手机号，订单号，车牌号搜索
        /// </summary>
        private void SearchFinancialStatisticsOrderListInfo()
        {
            string name = Request["name"];
            string value = Request["value"];
            if (string.IsNullOrEmpty(value))
            {
                GetFinancialStatistics();
            }
            string CYMUsersPhoneNumber = string.Empty;
            int userId = 0;
            string NewCYMUsersFabricOrderNumber = string.Empty;
            List<int> userIds = new List<int>();
            if (name.Equals("CYMUsersPhoneNumber", StringComparison.InvariantCultureIgnoreCase))
            {
                CYMUsersPhoneNumber = value;
                userId = CymUsersBll.LoadEntities(c => c.CYMUsersPhoneNumber == CYMUsersPhoneNumber).FirstOrDefault().CYMUsersId;
            }
            else if (name.Equals("NewCYMUsersFabricOrderNumber", StringComparison.InvariantCultureIgnoreCase))
            {
                NewCYMUsersFabricOrderNumber = value;
            }
            else if (name.Equals("CYMCarNumber", StringComparison.InvariantCultureIgnoreCase))
            {
                userIds = NewShoppingCymUsersMessageBll.LoadEntities(c => c.CarNumber.Contains(value)).Select(c => c.CYMUsersId).ToList();
            }

            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount = 0;



            List<NewCYMUsersFabricOrder> NewCYMUsersFabricOrderList = new List<NewCYMUsersFabricOrder>();
            if (userId != 0)
            {
                NewCYMUsersFabricOrderList = NewCYMUsersFabricOrderBll.LoadPageEntities(pageIndex, pageSize,
               out totalCount,
               c => (c.NewCYMUsersFabricOrderState != "0|1|2|9" && c.NewCYMUsersFabricOrderState != "0" && c.NewCYMUsersFabricOrderState != "0|H1" && c.NewCYMUsersFabricOrderState != "0|3" && !c.NewCYMUsersFabricOrderState.EndsWith("|-3") && !c.NewCYMUsersFabricOrderState.EndsWith("|-0")) && c.CYMUsersId == userId && c.OrderStatusdetails.Where(o => o.OrderStatus == "1").Count() > 0
              , c => c.OrderStatusdetails.Where(o => o.OrderStatus == "1")
                            .Select(o => o.OrderStatusCreateTime)
                            .FirstOrDefault(), false).ToList();
            }
            else if (!string.IsNullOrEmpty(NewCYMUsersFabricOrderNumber))
            {
                double dNewCYMUsersFabricOrderNumber = Convert.ToDouble(NewCYMUsersFabricOrderNumber);
                NewCYMUsersFabricOrderList = NewCYMUsersFabricOrderBll.LoadPageEntities(pageIndex, pageSize,
              out totalCount,
              c => (c.NewCYMUsersFabricOrderState != "0|1|2|9" && c.NewCYMUsersFabricOrderState != "0" && c.NewCYMUsersFabricOrderState != "0|H1" && c.NewCYMUsersFabricOrderState != "0|3" && !c.NewCYMUsersFabricOrderState.EndsWith("|-3") && !c.NewCYMUsersFabricOrderState.EndsWith("|-0")) && c.NewCYMUsersFabricOrderNumber == dNewCYMUsersFabricOrderNumber && c.OrderStatusdetails.Where(o => o.OrderStatus == "1").Count() > 0
             , c => c.OrderStatusdetails.Where(o => o.OrderStatus == "1")
                            .Select(o => o.OrderStatusCreateTime)
                            .FirstOrDefault(), false).ToList();
            }
            else if (userIds.Count > 0)
            {
                NewCYMUsersFabricOrderList = NewCYMUsersFabricOrderBll.LoadPageEntities(pageIndex, pageSize,
              out totalCount,
              c => (c.NewCYMUsersFabricOrderState != "0|1|2|9" && c.NewCYMUsersFabricOrderState != "0" && c.NewCYMUsersFabricOrderState != "0|H1" && c.NewCYMUsersFabricOrderState != "0|3" && !c.NewCYMUsersFabricOrderState.EndsWith("|-3") && !c.NewCYMUsersFabricOrderState.EndsWith("|-0")) && userIds.Contains(c.CYMUsersId) && c.OrderStatusdetails.Where(o => o.OrderStatus == "1").Count() > 0
             , c => c.OrderStatusdetails.Where(o => o.OrderStatus == "1")
                            .Select(o => o.OrderStatusCreateTime)
                            .FirstOrDefault(), false).ToList();
            }


            var VIPCardConsumeInfoList = NewCYMUsersFabricOrderList.Join(NewCymUsersShoppingInDcBll.DbSession.Db.Set<NewCYMUsersShoppingInDC>(),
               c => c.NewCYMUsersFabricOrderId, c => c.NewCYMUsersFabricOrderId, (c1, c2) => new
               {
                   Remarks = c2.Remarks,
                   CYMUsersInvoiceName = c1.CYMUsersInvoiceName,
                   NewCYMUsersFabricOrderNumber = c1.NewCYMUsersFabricOrderNumber,
                   NewCYMUsersFabricOrderName = c1.NewCYMUsersFabricOrderName,
                   CYMUsersId = c1.CYMUsersId,
                   BaoYangType = c2.BaoYangType,
                   Num = c2.Num,
                   CYMUsersShoppingIntegralValues = c1.CYMUsersShoppingIntegralValues,
                   DiscountsPrice = c1.DiscountsPrice,
                   NewCYMUsersFabricOrderState = c1.NewCYMUsersFabricOrderState,
                   NewCYMUsersFabricOrderId = c1.NewCYMUsersFabricOrderId,
                   GOODSParameterListId = c2.GOODSParameterListId,
                   AppointmentTime = c1.AppointmentTime,
                   CYMMerchantId = c1.CYMMerchantId,
                   FirstPutInTime = Convert.ToDateTime(c1.OrderStatusdetails.Where(o => o.OrderStatus == "1")
                            .Select(o => o.OrderStatusCreateTime)
                            .FirstOrDefault()).ToString("yyyy-MM-dd HH:mm:ss"),
                   ClerkInfo = GetClerkInfoByOrderId(c1.NewCYMUsersFabricOrderId),
                   // GoodsList = new List<GOODSParameterDome>(),
               }).Join(NewShoppingCymUsersMessageBll.DbSession.Db.Set<NewShoppingCYMUsersMessage>(), c => c.NewCYMUsersFabricOrderId, c => c.NewCYMUsersFabricOrderId, (c1, c2) => new
                {
                    Remarks = c1.Remarks,
                    NewCYMUsersFabricOrderNumber = c1.NewCYMUsersFabricOrderNumber,
                    NewCYMUsersFabricOrderName = c1.NewCYMUsersFabricOrderName,
                    CYMUsersId = c1.CYMUsersId,
                    DiscountsPrice = c1.DiscountsPrice,
                    CYMUsersShoppingIntegralValues = c1.CYMUsersShoppingIntegralValues,
                    Num = c1.Num,
                    CYMUsersInvoiceName = c1.CYMUsersInvoiceName,
                    BaoYangType = c1.BaoYangType,
                    NewCYMUsersFabricOrderState = c1.NewCYMUsersFabricOrderState,
                    NewCYMUsersFabricOrderId = c1.NewCYMUsersFabricOrderId,
                    GOODSParameterListId = c1.GOODSParameterListId,
                    AppointmentTime = c1.AppointmentTime,
                    BYServeName = c2.BYServeName,
                    CYMMerchantId = c1.CYMMerchantId,
                    CYMMerchant = GetCYMMerchantNameFromCYMMerchantId(c1.CYMMerchantId),
                    CheBaoCarCompositeId = c2.CheBaoCarCompositeId,
                    CheBaoCarCompositeName = GetCheBaoCarCompositeNameForCheBaoCarCompositeId(c2.CheBaoCarCompositeId),
                    CarNumber = c2.CarNumber,
                    BYServeTelPhone = c2.BYServeTelPhone,
                    FirstPutInTime = c1.FirstPutInTime,
                    PushType = GetPushTypeByNewCYMUsersFabricOrderState(c1.NewCYMUsersFabricOrderState, c1.NewCYMUsersFabricOrderName),
                    ClerkName = c1.ClerkInfo.ClerkName,
                    ClerkInfo = c1.ClerkInfo,
                    PaySours = c1.ClerkInfo.PaySours,
                    FinanceRemark = c1.ClerkInfo.FinanceRemark,
                    ServerRemark = c1.ClerkInfo.ServerRemark,
                    CarMileage = c1.ClerkInfo.CarMileage,
                    FinanceStatus = GetFinanceStatusStringForNum(c1.ClerkInfo.FinanceStatus),
                    CYMVoucherFaceValue = c1.ClerkInfo.CYMVoucherFaceValue,
                    CYMTakeOutList = GetCYMTakeOutByOrderId(c1.NewCYMUsersFabricOrderId).Select(c => new
                    {
                        TakeOutId = c.TakeOutId,
                        TakeOutNumber = c.TakeOutNumber,
                        TakeOutMan = c.TakeOutMan,
                        PurchaseState = c.PurchaseState == 0 ? "待审核" : c.PurchaseState == 1 ? "已审核" : c.PurchaseState == 2 ? "已作废" : "未知",
                        CherkMan = c.CherkMan,
                        TakeOutTime = c.TakeOutTime,
                        Remarks = c.Remarks,
                        UpdateTime = c.UpdateTime,
                        StorageName = c.CYMStorage.StorageName,
                        NewCYMUsersFabricOrderId = c.NewCYMUsersFabricOrderId,
                        TakeOutDepartment = c.TakeOutDepartment,

                        CYMTakeOutGoods = c.CYMTakeOutGoods.Select(p => new
                        {
                            TakeOutGoodsId = p.TakeOutGoodsId,
                            GOODSName = p.GOODSParameterDome.GOODSName,
                            TakeOutNum = p.TakeOutNum,
                            AvgPurchasePrice = p.GOODSParameterDome.CYMPurchaseGoods.Where(r => r.CYMPurchas.PurchaseState == 1).Count() > 0 ? (p.GOODSParameterDome.CYMPurchaseGoods.Where(r => r.CYMPurchas.PurchaseState == 1).Select(d => new { SumPrice = d.PurchasePrice * d.PurchaseNum }).Sum(w => w.SumPrice) / (p.GOODSParameterDome.CYMPurchaseGoods.Where(r => r.CYMPurchas.PurchaseState == 1).Sum(q => q.PurchaseNum) == 0 ? -1 : p.GOODSParameterDome.CYMPurchaseGoods.Sum(q => q.PurchaseNum))) : (p.GOODSParameterDome.CYMWarehouses.Where(a => a.StorageId == c.StorageId && a.GOODSParameterId == p.GOODSParameterId).FirstOrDefault().PurchasePrice)
                        })
                    }).Select(c => new
                    {
                        TakeOutId = c.TakeOutId,
                        TakeOutNumber = c.TakeOutNumber,
                        TakeOutMan = c.TakeOutMan,
                        PurchaseState = c.PurchaseState,
                        CherkMan = c.CherkMan,
                        NewCYMUsersFabricOrderId = c.NewCYMUsersFabricOrderId,
                        TakeOutTime = c.TakeOutTime,
                        Remarks = c.Remarks,
                        UpdateTime = c.UpdateTime,
                        TakeOutDepartment = c.TakeOutDepartment,
                        StorageName = c.StorageName,
                        CYMTakeOutGoods = c.CYMTakeOutGoods,
                        SumPrice = c.CYMTakeOutGoods.Select(p => p.AvgPurchasePrice * p.TakeOutNum).Sum(p => p)
                    }),
                    ShouldPrice = new List<decimal>(),
                    GoodsList = new List<GOODSParameterDome>(),

                }).ToList();

            //string s = ExcelHelper.WriteExcel(ExcelHelper.IListOut(VIPCardConsumeInfoList));

            StringBuilder allsb = new StringBuilder();
            StringBuilder qwsb = new StringBuilder();
            StringBuilder qwcsb = new StringBuilder();

            var tempg = VIPCardConsumeInfoList.GroupBy(c => c.PaySours);

            foreach (var item in tempg)
            {
                allsb.Append(item.Key);
                allsb.Append(":￥");
                allsb.Append(item.Sum(c => c.DiscountsPrice) + ",");
                qwsb.Append(item.Key);
                qwsb.Append(":￥");
                qwsb.Append(item.Where(c => c.FinanceStatus != "无效单").Sum(c => c.DiscountsPrice) + "<br/>");
                qwcsb.Append(item.Key);
                qwcsb.Append(":￥");
                qwcsb.Append(item.Where(c => c.FinanceStatus != "无效单" && c.FinanceStatus != "错误单").Sum(c => c.DiscountsPrice) + "<br/>");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();


            decimal yingFuDecimal = 0;
            decimal shiFuDecimal = 0;
            decimal jiFenDecimal = 0;
            decimal hongBaoDecimal = 0;
            //decimal xsshifu = 0;
            //decimal xxshifu = 0;


            decimal qwyingFuDecimal = 0;
            decimal qwshiFuDecimal = 0;
            decimal qwjiFenDecimal = 0;
            decimal qwhongBaoDecimal = 0;
            //decimal qwxsshifu = 0;
            //decimal qwxxshifu = 0;


            decimal qwcyingFuDecimal = 0;
            decimal qwcshiFuDecimal = 0;
            decimal qwcjiFenDecimal = 0;
            decimal qwchongBaoDecimal = 0;
            //decimal qwcxsshifu = 0;
            //decimal qwcxxshifu = 0;


            for (int i = 0; i < VIPCardConsumeInfoList.Count; i++)
            {
                decimal sumPrice = 0;
                List<string> goodidList =
                   VIPCardConsumeInfoList[i].GOODSParameterListId.Split(new char[] { '-' },
                       StringSplitOptions.RemoveEmptyEntries).ToList();
                var group = goodidList.GroupBy(c => c, c => c);
                foreach (var item in group)
                {
                    int goodid = int.Parse(item.Key);
                    GOODSParameterDome goodsParameterDome = new GOODSParameterDome();
                    goodsParameterDome = GOODSParameterDomeBll.LoadEntities(c => c.GOODSParameterId == goodid).FirstOrDefault();

                    string newgoodsParameterDome = JsonConvert.SerializeObject(goodsParameterDome);
                    goodsParameterDome = JsonConvert.DeserializeObject<GOODSParameterDome>(newgoodsParameterDome);
                    goodsParameterDome.Num = item.Count().ToString();
                    sumPrice += (decimal)goodsParameterDome.GOODSPrice * int.Parse(goodsParameterDome.Num);
                    VIPCardConsumeInfoList[i].GoodsList.Add(goodsParameterDome);
                }
                VIPCardConsumeInfoList[i].ShouldPrice.Add(sumPrice * VIPCardConsumeInfoList[i].Num);
                yingFuDecimal += sumPrice * VIPCardConsumeInfoList[i].Num;
                shiFuDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                jiFenDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMUsersShoppingIntegralValues);
                hongBaoDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMVoucherFaceValue == null ? 0 : VIPCardConsumeInfoList[i].CYMVoucherFaceValue);

                //if (VIPCardConsumeInfoList[i].PushType.Contains("APP支付"))
                //{
                //    xsshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                //}
                //else
                //{
                //    xxshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                //}

                if (VIPCardConsumeInfoList[i].FinanceStatus != "无效单")
                {
                    qwyingFuDecimal += sumPrice * VIPCardConsumeInfoList[i].Num;
                    qwshiFuDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    qwjiFenDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMUsersShoppingIntegralValues);
                    qwhongBaoDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMVoucherFaceValue == null ? 0 : VIPCardConsumeInfoList[i].CYMVoucherFaceValue);
                    //if (VIPCardConsumeInfoList[i].PushType.Contains("APP支付"))
                    //{
                    //    qwxsshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    //}
                    //else
                    //{
                    //    qwxxshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    //}
                }

                if (VIPCardConsumeInfoList[i].FinanceStatus != "无效单" && VIPCardConsumeInfoList[i].FinanceStatus != "错误单")
                {
                    qwcyingFuDecimal += sumPrice * VIPCardConsumeInfoList[i].Num;
                    qwcshiFuDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    qwcjiFenDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMUsersShoppingIntegralValues);
                    qwchongBaoDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMVoucherFaceValue == null ? 0 : VIPCardConsumeInfoList[i].CYMVoucherFaceValue);
                    //if (VIPCardConsumeInfoList[i].PushType.Contains("APP支付"))
                    //{
                    //    qwcxsshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    //}
                    //else
                    //{
                    //    qwcxxshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    //}
                }
            }

            var Allfooter =
              new
              {
                  NewCYMUsersFabricOrderNumber = "全部合计",
                  ShouldPrice = new decimal[] { yingFuDecimal },
                  DiscountsPrice = shiFuDecimal,
                  CYMUsersShoppingIntegralValues = jiFenDecimal,
                  CYMVoucherFaceValue = hongBaoDecimal,
                  FirstPutInTime = allsb.ToString()
              };

            var QWfooter =
                 new
                 {
                     NewCYMUsersFabricOrderNumber = "去掉无效单合计",
                     ShouldPrice = new decimal[] { qwyingFuDecimal },
                     DiscountsPrice = qwshiFuDecimal,
                     CYMUsersShoppingIntegralValues = qwjiFenDecimal,
                     CYMVoucherFaceValue = qwhongBaoDecimal,
                     FirstPutInTime = qwsb.ToString()

                 };
            var QWCfooter =
                new
                {
                    NewCYMUsersFabricOrderNumber = "去掉无效|错误单合计",
                    ShouldPrice = new decimal[] { qwcyingFuDecimal },
                    DiscountsPrice = qwcshiFuDecimal,
                    CYMUsersShoppingIntegralValues = qwcjiFenDecimal,
                    CYMVoucherFaceValue = qwchongBaoDecimal,
                    FirstPutInTime = qwcsb.ToString()

                };
            Response.Write(JsonConvert.SerializeObject(new { rows = VIPCardConsumeInfoList, total = totalCount, footer = new List<object> { QWCfooter, QWfooter, Allfooter } }));
            Response.End();











        }
        /// <summary>
        /// 高级搜索，根据时间，店铺是否有交易额，是否出库存
        /// </summary>
        private void SearchFinancialStatistics()
        {
            //DateTime StarPayTime = Convert.ToDateTime(Request["StarPayTime"]);
            // DateTime EndPayTime = Convert.ToDateTime(Request["EndPayTime"]);

            DateTime StarPayTime = Convert.ToDateTime(string.IsNullOrEmpty(Request["StarPayTime"]) ? DateTime.MinValue.ToString() : Request["StarPayTime"]);
            DateTime EndPayTime = Convert.ToDateTime(string.IsNullOrEmpty(Request["EndPayTime"]) ? DateTime.MaxValue.ToString() : Convert.ToDateTime(Request["EndPayTime"]).AddDays(1).AddMilliseconds(-1).ToString());



            int CYMMerchantId = int.Parse(Request["CYMMerchantId"]);
            int IsHaveMoney = int.Parse(Request["IsHaveMoney"]);
            int IsInventoryConsumption = int.Parse(Request["IsInventoryConsumption"]);

            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount;
            IQueryable<NewCYMUsersFabricOrder> temp;
            // if (CYMMerchantId==0)
            // {
            //     temp = NewCYMUsersFabricOrderBll.DbSession.Db.Set<NewCYMUsersFabricOrder>()
            //    .Where(
            //        c =>
            //            c.NewCYMUsersFabricOrderState != "0|1|2|9" && c.NewCYMUsersFabricOrderState != "0" &&
            //            c.NewCYMUsersFabricOrderState != "0|H1" && c.NewCYMUsersFabricOrderState != "0|3" &&
            //            !c.NewCYMUsersFabricOrderState.EndsWith("|-3") && !c.NewCYMUsersFabricOrderState.EndsWith("|-0"))
            //    .Where(
            //                   c =>
            //SqlFunctions.DateDiff("day",
            //    SqlFunctions.Stuff(SqlFunctions.Stuff(c.FirstPutInTime, 1, SqlFunctions.CharIndex("|", c.FirstPutInTime), "") + "123", 20, 1000, ""),
            //    StarPayTime) <= 0 &&
            //SqlFunctions.DateDiff("day",
            //     SqlFunctions.Stuff(SqlFunctions.Stuff(c.FirstPutInTime, 1, SqlFunctions.CharIndex("|", c.FirstPutInTime), "") + "123", 20, 1000, ""),
            //    EndPayTime) >= 0
            //           );
            //     totalCount = temp.Count();
            // }
            // else
            // {

            List<int> toNewCYMUsersFabricOrderId = CymTakeOutBll.LoadEntities(c => true).Select(c => c.NewCYMUsersFabricOrderId).Distinct().ToList();


            temp = NewCYMUsersFabricOrderBll.DbSession.Db.Set<NewCYMUsersFabricOrder>()
                .Where(
                    c =>
                        c.NewCYMUsersFabricOrderState != "0|1|2|9" && c.NewCYMUsersFabricOrderState != "0" &&
                        c.NewCYMUsersFabricOrderState != "0|H1" && c.NewCYMUsersFabricOrderState != "0|3" &&
                        !c.NewCYMUsersFabricOrderState.EndsWith("|-3") && !c.NewCYMUsersFabricOrderState.EndsWith("|-0") && c.OrderStatusdetails.Where(o => o.OrderStatus == "1").Count() > 0 &&
                        (CYMMerchantId == 0 ? true : c.CYMMerchantId == CYMMerchantId) &&
                        (c.OrderStatusdetails.Where(o => o.OrderStatus == "1")
                            .Select(o => o.OrderStatusCreateTime)
                            .FirstOrDefault() >= StarPayTime) &&
                        (c.OrderStatusdetails.Where(o => o.OrderStatus == "1")
                            .Select(o => o.OrderStatusCreateTime)
                            .FirstOrDefault() <= EndPayTime) && (IsHaveMoney == 0 ? true : (IsHaveMoney == 1 ? c.DiscountsPrice > 0.0 : c.DiscountsPrice == 0.0)) && (IsInventoryConsumption == 0 ? true : (IsInventoryConsumption == 1 ? toNewCYMUsersFabricOrderId.Contains(c.NewCYMUsersFabricOrderId) : (!toNewCYMUsersFabricOrderId.Contains(c.NewCYMUsersFabricOrderId)))));
            //    .Where(
            //                   c =>
            //SqlFunctions.DateDiff("day",
            //    SqlFunctions.Stuff(SqlFunctions.Stuff(c.FirstPutInTime, 1, SqlFunctions.CharIndex("|", c.FirstPutInTime), "") + "123", 20, 1000, ""),
            //    StarPayTime) <= 0 &&
            //SqlFunctions.DateDiff("day",
            //     SqlFunctions.Stuff(SqlFunctions.Stuff(c.FirstPutInTime, 1, SqlFunctions.CharIndex("|", c.FirstPutInTime), "") + "123", 20, 1000, ""),
            //    EndPayTime) >= 0
            //           );
            totalCount = temp.Count();
            //}


            var NewCYMUsersFabricOrderList = temp.OrderByDescending(c => c.OrderStatusdetails.Where(o => o.OrderStatus == "1")
                            .Select(o => o.OrderStatusCreateTime)
                            .FirstOrDefault()).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();



            #region 测试

            #endregion



            var VIPCardConsumeInfoList = NewCYMUsersFabricOrderList.Join(NewCymUsersShoppingInDcBll.DbSession.Db.Set<NewCYMUsersShoppingInDC>(),
                c => c.NewCYMUsersFabricOrderId, c => c.NewCYMUsersFabricOrderId, (c1, c2) => new
                {
                    Remarks = c2.Remarks,
                    CYMUsersInvoiceName = c1.CYMUsersInvoiceName,
                    NewCYMUsersFabricOrderNumber = c1.NewCYMUsersFabricOrderNumber,
                    NewCYMUsersFabricOrderName = c1.NewCYMUsersFabricOrderName,
                    CYMUsersId = c1.CYMUsersId,
                    BaoYangType = c2.BaoYangType,
                    Num = c2.Num,
                    CYMUsersShoppingIntegralValues = c1.CYMUsersShoppingIntegralValues,
                    DiscountsPrice = c1.DiscountsPrice,
                    NewCYMUsersFabricOrderState = c1.NewCYMUsersFabricOrderState,
                    NewCYMUsersFabricOrderId = c1.NewCYMUsersFabricOrderId,
                    GOODSParameterListId = c2.GOODSParameterListId,
                    AppointmentTime = c1.AppointmentTime,
                    CYMMerchantId = c1.CYMMerchantId,
                    FirstPutInTime = Convert.ToDateTime(c1.OrderStatusdetails.Where(o => o.OrderStatus == "1")
                            .Select(o => o.OrderStatusCreateTime)
                            .FirstOrDefault()).ToString("yyyy-MM-dd HH:mm:ss"),
                    ClerkInfo = GetClerkInfoByOrderId(c1.NewCYMUsersFabricOrderId),
                    // GoodsList = new List<GOODSParameterDome>(),
                }).Join(NewShoppingCymUsersMessageBll.DbSession.Db.Set<NewShoppingCYMUsersMessage>(), c => c.NewCYMUsersFabricOrderId, c => c.NewCYMUsersFabricOrderId, (c1, c2) => new
                {
                    Remarks = c1.Remarks,
                    NewCYMUsersFabricOrderNumber = c1.NewCYMUsersFabricOrderNumber,
                    NewCYMUsersFabricOrderName = c1.NewCYMUsersFabricOrderName,
                    CYMUsersId = c1.CYMUsersId,
                    DiscountsPrice = c1.DiscountsPrice,
                    CYMUsersShoppingIntegralValues = c1.CYMUsersShoppingIntegralValues,
                    Num = c1.Num,
                    CYMUsersInvoiceName = c1.CYMUsersInvoiceName,
                    BaoYangType = c1.BaoYangType,
                    NewCYMUsersFabricOrderState = c1.NewCYMUsersFabricOrderState,
                    NewCYMUsersFabricOrderId = c1.NewCYMUsersFabricOrderId,
                    GOODSParameterListId = c1.GOODSParameterListId,
                    AppointmentTime = c1.AppointmentTime,
                    BYServeName = c2.BYServeName,
                    CYMMerchantId = c1.CYMMerchantId,
                    CYMMerchant = GetCYMMerchantNameFromCYMMerchantId(c1.CYMMerchantId),
                    CheBaoCarCompositeId = c2.CheBaoCarCompositeId,
                    CheBaoCarCompositeName = GetCheBaoCarCompositeNameForCheBaoCarCompositeId(c2.CheBaoCarCompositeId),
                    CarNumber = c2.CarNumber,
                    BYServeTelPhone = c2.BYServeTelPhone,
                    FirstPutInTime = c1.FirstPutInTime,
                    PushType = GetPushTypeByNewCYMUsersFabricOrderState(c1.NewCYMUsersFabricOrderState, c1.NewCYMUsersFabricOrderName),
                    ClerkName = c1.ClerkInfo.ClerkName,
                    ClerkInfo = c1.ClerkInfo,
                    PaySours = c1.ClerkInfo.PaySours,
                    FinanceRemark = c1.ClerkInfo.FinanceRemark,
                    ServerRemark = c1.ClerkInfo.ServerRemark,
                    CarMileage = c1.ClerkInfo.CarMileage,
                    FinanceStatus = GetFinanceStatusStringForNum(c1.ClerkInfo.FinanceStatus),
                    CYMVoucherFaceValue = c1.ClerkInfo.CYMVoucherFaceValue,
                    CYMTakeOutList = GetCYMTakeOutByOrderId(c1.NewCYMUsersFabricOrderId).Select(c => new
                    {
                        TakeOutId = c.TakeOutId,
                        TakeOutNumber = c.TakeOutNumber,
                        TakeOutMan = c.TakeOutMan,
                        PurchaseState = c.PurchaseState == 0 ? "待审核" : c.PurchaseState == 1 ? "已审核" : c.PurchaseState == 2 ? "已作废" : "未知",
                        CherkMan = c.CherkMan,
                        TakeOutTime = c.TakeOutTime,
                        Remarks = c.Remarks,
                        UpdateTime = c.UpdateTime,
                        StorageName = c.CYMStorage.StorageName,
                        NewCYMUsersFabricOrderId = c.NewCYMUsersFabricOrderId,
                        TakeOutDepartment = c.TakeOutDepartment,

                        CYMTakeOutGoods = c.CYMTakeOutGoods.Select(p => new
                        {
                            TakeOutGoodsId = p.TakeOutGoodsId,
                            GOODSName = p.GOODSParameterDome.GOODSName,
                            TakeOutNum = p.TakeOutNum,
                            AvgPurchasePrice = p.GOODSParameterDome.CYMPurchaseGoods.Where(r => r.CYMPurchas.PurchaseState == 1).Count() > 0 ? (p.GOODSParameterDome.CYMPurchaseGoods.Where(r => r.CYMPurchas.PurchaseState == 1).Select(d => new { SumPrice = d.PurchasePrice * d.PurchaseNum }).Sum(w => w.SumPrice) / (p.GOODSParameterDome.CYMPurchaseGoods.Where(r => r.CYMPurchas.PurchaseState == 1).Sum(q => q.PurchaseNum) == 0 ? -1 : p.GOODSParameterDome.CYMPurchaseGoods.Sum(q => q.PurchaseNum))) : (p.GOODSParameterDome.CYMWarehouses.Where(a => a.StorageId == c.StorageId && a.GOODSParameterId == p.GOODSParameterId).FirstOrDefault().PurchasePrice)
                        })
                    }).Select(c => new
                    {
                        TakeOutId = c.TakeOutId,
                        TakeOutNumber = c.TakeOutNumber,
                        TakeOutMan = c.TakeOutMan,
                        PurchaseState = c.PurchaseState,
                        CherkMan = c.CherkMan,
                        NewCYMUsersFabricOrderId = c.NewCYMUsersFabricOrderId,
                        TakeOutTime = c.TakeOutTime,
                        Remarks = c.Remarks,
                        UpdateTime = c.UpdateTime,
                        TakeOutDepartment = c.TakeOutDepartment,
                        StorageName = c.StorageName,
                        CYMTakeOutGoods = c.CYMTakeOutGoods,
                        SumPrice = c.CYMTakeOutGoods.Select(p => p.AvgPurchasePrice * p.TakeOutNum).Sum(p => p)
                    }),
                    ShouldPrice = new List<decimal>(),
                    GoodsList = new List<GOODSParameterDome>(),

                }).ToList();

            //string s = ExcelHelper.WriteExcel(ExcelHelper.IListOut(VIPCardConsumeInfoList));

            StringBuilder allsb = new StringBuilder();
            StringBuilder qwsb = new StringBuilder();
            StringBuilder qwcsb = new StringBuilder();

            var tempg = VIPCardConsumeInfoList.GroupBy(c => c.PaySours);

            foreach (var item in tempg)
            {
                allsb.Append(item.Key);
                allsb.Append(":￥");
                allsb.Append(item.Sum(c => c.DiscountsPrice) + "<br/>");
                qwsb.Append(item.Key);
                qwsb.Append(":￥");
                qwsb.Append(item.Where(c => c.FinanceStatus != "无效单").Sum(c => c.DiscountsPrice) + "<br/>");
                qwcsb.Append(item.Key);
                qwcsb.Append(":￥");
                qwcsb.Append(item.Where(c => c.FinanceStatus != "无效单" && c.FinanceStatus != "错误单").Sum(c => c.DiscountsPrice) + "<br/>");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();


            decimal yingFuDecimal = 0;
            decimal shiFuDecimal = 0;
            decimal jiFenDecimal = 0;
            decimal hongBaoDecimal = 0;
            //decimal xsshifu = 0;
            //decimal xxshifu = 0;


            decimal qwyingFuDecimal = 0;
            decimal qwshiFuDecimal = 0;
            decimal qwjiFenDecimal = 0;
            decimal qwhongBaoDecimal = 0;
            //decimal qwxsshifu = 0;
            //decimal qwxxshifu = 0;


            decimal qwcyingFuDecimal = 0;
            decimal qwcshiFuDecimal = 0;
            decimal qwcjiFenDecimal = 0;
            decimal qwchongBaoDecimal = 0;
            //decimal qwcxsshifu = 0;
            //decimal qwcxxshifu = 0;


            for (int i = 0; i < VIPCardConsumeInfoList.Count; i++)
            {
                decimal sumPrice = 0;
                List<string> goodidList =
                   VIPCardConsumeInfoList[i].GOODSParameterListId.Split(new char[] { '-' },
                       StringSplitOptions.RemoveEmptyEntries).ToList();
                var group = goodidList.GroupBy(c => c, c => c);
                foreach (var item in group)
                {
                    int goodid = int.Parse(item.Key);
                    GOODSParameterDome goodsParameterDome = new GOODSParameterDome();
                    goodsParameterDome = GOODSParameterDomeBll.LoadEntities(c => c.GOODSParameterId == goodid).FirstOrDefault();

                    string newgoodsParameterDome = JsonConvert.SerializeObject(goodsParameterDome);
                    goodsParameterDome = JsonConvert.DeserializeObject<GOODSParameterDome>(newgoodsParameterDome);
                    goodsParameterDome.Num = item.Count().ToString();
                    sumPrice += (decimal)goodsParameterDome.GOODSPrice * int.Parse(goodsParameterDome.Num);
                    VIPCardConsumeInfoList[i].GoodsList.Add(goodsParameterDome);
                }
                VIPCardConsumeInfoList[i].ShouldPrice.Add(sumPrice * VIPCardConsumeInfoList[i].Num);
                yingFuDecimal += sumPrice * VIPCardConsumeInfoList[i].Num;
                shiFuDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                jiFenDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMUsersShoppingIntegralValues);
                hongBaoDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMVoucherFaceValue == null ? 0 : VIPCardConsumeInfoList[i].CYMVoucherFaceValue);

                //if (VIPCardConsumeInfoList[i].PushType.Contains("APP支付"))
                //{
                //    xsshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                //}
                //else
                //{
                //    xxshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                //}

                if (VIPCardConsumeInfoList[i].FinanceStatus != "无效单")
                {
                    qwyingFuDecimal += sumPrice * VIPCardConsumeInfoList[i].Num;
                    qwshiFuDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    qwjiFenDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMUsersShoppingIntegralValues);
                    qwhongBaoDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMVoucherFaceValue == null ? 0 : VIPCardConsumeInfoList[i].CYMVoucherFaceValue);
                    //if (VIPCardConsumeInfoList[i].PushType.Contains("APP支付"))
                    //{
                    //    qwxsshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    //}
                    //else
                    //{
                    //    qwxxshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    //}
                }

                if (VIPCardConsumeInfoList[i].FinanceStatus != "无效单" && VIPCardConsumeInfoList[i].FinanceStatus != "错误单")
                {
                    qwcyingFuDecimal += sumPrice * VIPCardConsumeInfoList[i].Num;
                    qwcshiFuDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    qwcjiFenDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMUsersShoppingIntegralValues);
                    qwchongBaoDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMVoucherFaceValue == null ? 0 : VIPCardConsumeInfoList[i].CYMVoucherFaceValue);
                    //if (VIPCardConsumeInfoList[i].PushType.Contains("APP支付"))
                    //{
                    //    qwcxsshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    //}
                    //else
                    //{
                    //    qwcxxshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    //}
                }
            }

            var Allfooter =
              new
              {
                  NewCYMUsersFabricOrderNumber = "全部合计",
                  ShouldPrice = new decimal[] { yingFuDecimal },
                  DiscountsPrice = shiFuDecimal,
                  CYMUsersShoppingIntegralValues = jiFenDecimal,
                  CYMVoucherFaceValue = hongBaoDecimal,
                  FirstPutInTime = allsb.ToString()
              };

            var QWfooter =
                 new
                 {
                     NewCYMUsersFabricOrderNumber = "去掉无效单合计",
                     ShouldPrice = new decimal[] { qwyingFuDecimal },
                     DiscountsPrice = qwshiFuDecimal,
                     CYMUsersShoppingIntegralValues = qwjiFenDecimal,
                     CYMVoucherFaceValue = qwhongBaoDecimal,
                     FirstPutInTime = qwsb.ToString()

                 };
            var QWCfooter =
                new
                {
                    NewCYMUsersFabricOrderNumber = "去掉无效|错误单合计",
                    ShouldPrice = new decimal[] { qwcyingFuDecimal },
                    DiscountsPrice = qwcshiFuDecimal,
                    CYMUsersShoppingIntegralValues = qwcjiFenDecimal,
                    CYMVoucherFaceValue = qwchongBaoDecimal,
                    FirstPutInTime = qwcsb.ToString()

                };
            Response.Write(JsonConvert.SerializeObject(new { rows = VIPCardConsumeInfoList, total = totalCount, footer = new List<object> { QWCfooter, QWfooter, Allfooter } }));
            Response.End();







        }
        /// <summary>
        /// 将订单状态改为为核单
        /// </summary>
        private void EditFinanceStatusTo0()
        {
            int NewCYMUsersFabricOrderId = int.Parse(Request["NewCYMUsersFabricOrderId"]);
            // int rowIndex = int.Parse(Request["rowIndex"]);
            ClerkAndOrder clerkAndOrder = ClerkAndOrderBll.LoadEntities(c => c.NewCYMUsersFabricOrderId == NewCYMUsersFabricOrderId).FirstOrDefault();
            bool res = false;
            if (clerkAndOrder == null)
            {
                clerkAndOrder = new ClerkAndOrder()
                {
                    FinanceStatus = 0,
                    NewCYMUsersFabricOrderId = NewCYMUsersFabricOrderId,
                    ClerkId = 0,
                };
                clerkAndOrder = ClerkAndOrderBll.AddEntity(clerkAndOrder);
                res = clerkAndOrder.id > 0;
            }
            else
            {
                clerkAndOrder.FinanceStatus = 0;
                res = ClerkAndOrderBll.UpdateEntity(clerkAndOrder);
            }
            if (res)
            {
                Response.Write(JsonConvert.SerializeObject(new { msg = "ok" }));
            }
            else
            {
                Response.Write(JsonConvert.SerializeObject(new { msg = "no" }));
            }

            Response.End();
        }
        /// <summary>
        /// 将订单状态改为有错误
        /// </summary>
        private void EditFinanceStatusTo3()
        {
            int NewCYMUsersFabricOrderId = int.Parse(Request["NewCYMUsersFabricOrderId"]);
            // int rowIndex = int.Parse(Request["rowIndex"]);
            ClerkAndOrder clerkAndOrder = ClerkAndOrderBll.LoadEntities(c => c.NewCYMUsersFabricOrderId == NewCYMUsersFabricOrderId).FirstOrDefault();
            bool res = false;
            if (clerkAndOrder == null)
            {
                clerkAndOrder = new ClerkAndOrder()
                {
                    FinanceStatus = 3,
                    NewCYMUsersFabricOrderId = NewCYMUsersFabricOrderId,
                    ClerkId = 0,
                };
                clerkAndOrder = ClerkAndOrderBll.AddEntity(clerkAndOrder);
                res = clerkAndOrder.id > 0;
            }
            else
            {
                clerkAndOrder.FinanceStatus = 3;
                res = ClerkAndOrderBll.UpdateEntity(clerkAndOrder);
            }
            if (res)
            {
                Response.Write(JsonConvert.SerializeObject(new { msg = "ok" }));
            }
            else
            {
                Response.Write(JsonConvert.SerializeObject(new { msg = "no" }));
            }

            Response.End();
        }
        /// <summary>
        /// 将订单状态改为已核单
        /// </summary>
        private void EditFinanceStatusTo2()
        {
            int NewCYMUsersFabricOrderId = int.Parse(Request["NewCYMUsersFabricOrderId"]);
            // int rowIndex = int.Parse(Request["rowIndex"]);
            ClerkAndOrder clerkAndOrder = ClerkAndOrderBll.LoadEntities(c => c.NewCYMUsersFabricOrderId == NewCYMUsersFabricOrderId).FirstOrDefault();
            bool res = false;
            if (clerkAndOrder == null)
            {
                clerkAndOrder = new ClerkAndOrder()
                {
                    FinanceStatus = 2,
                    NewCYMUsersFabricOrderId = NewCYMUsersFabricOrderId,
                    ClerkId = 0,
                };
                clerkAndOrder = ClerkAndOrderBll.AddEntity(clerkAndOrder);
                res = clerkAndOrder.id > 0;
            }
            else
            {
                clerkAndOrder.FinanceStatus = 2;
                res = ClerkAndOrderBll.UpdateEntity(clerkAndOrder);
            }
            if (res)
            {
                Response.Write(JsonConvert.SerializeObject(new { msg = "ok" }));
            }
            else
            {
                Response.Write(JsonConvert.SerializeObject(new { msg = "no" }));
            }

            Response.End();
        }
        /// <summary>
        /// 将订单状态改为未核单
        /// </summary>
        private void EditFinanceStatusTo1()
        {
            int NewCYMUsersFabricOrderId = int.Parse(Request["NewCYMUsersFabricOrderId"]);
            // int rowIndex = int.Parse(Request["rowIndex"]);
            ClerkAndOrder clerkAndOrder = ClerkAndOrderBll.LoadEntities(c => c.NewCYMUsersFabricOrderId == NewCYMUsersFabricOrderId).FirstOrDefault();
            bool res = false;
            if (clerkAndOrder == null)
            {
                clerkAndOrder = new ClerkAndOrder()
                {
                    FinanceStatus = 1,
                    NewCYMUsersFabricOrderId = NewCYMUsersFabricOrderId,
                    ClerkId = 0,
                };
                clerkAndOrder = ClerkAndOrderBll.AddEntity(clerkAndOrder);
                res = clerkAndOrder.id > 0;
            }
            else
            {
                clerkAndOrder.FinanceStatus = 1;
                res = ClerkAndOrderBll.UpdateEntity(clerkAndOrder);
            }
            if (res)
            {
                Response.Write(JsonConvert.SerializeObject(new { msg = "ok" }));
            }
            else
            {
                Response.Write(JsonConvert.SerializeObject(new { msg = "no" }));
            }

            Response.End();
        }
        /// <summary>
        /// 没用
        /// </summary>
        private void DCGetFinancialStatistics()
        {
            var execlLists = NewCYMUsersFabricOrderBll.LoadEntities(
                  c =>
                      c.NewCYMUsersFabricOrderState != "0|1|2|9" && c.NewCYMUsersFabricOrderState != "0" &&
                      !c.NewCYMUsersFabricOrderState.EndsWith("|-3") && c.OrderStatusdetails.Where(o => o.OrderStatus == "1").Count() > 0 && !c.NewCYMUsersFabricOrderState.EndsWith("|-0")).OrderByDescending(c => c.NewCYMUsersFabricOrderId).Take(260)
                  .ToList();
            var VIPCardConsumeInfoList = execlLists.Join(NewCymUsersShoppingInDcBll.DbSession.Db.Set<NewCYMUsersShoppingInDC>(),
                c => c.NewCYMUsersFabricOrderId, c => c.NewCYMUsersFabricOrderId, (c1, c2) => new
                {
                    Remarks = c2.Remarks,
                    CYMUsersInvoiceName = c1.CYMUsersInvoiceName,
                    NewCYMUsersFabricOrderNumber = c1.NewCYMUsersFabricOrderNumber,
                    NewCYMUsersFabricOrderName = c1.NewCYMUsersFabricOrderName,
                    CYMUsersId = c1.CYMUsersId,
                    BaoYangType = c2.BaoYangType,
                    Num = c2.Num,
                    CYMUsersShoppingIntegralValues = c1.CYMUsersShoppingIntegralValues,
                    DiscountsPrice = c1.DiscountsPrice,
                    NewCYMUsersFabricOrderState = c1.NewCYMUsersFabricOrderState,
                    NewCYMUsersFabricOrderId = c1.NewCYMUsersFabricOrderId,
                    GOODSParameterListId = c2.GOODSParameterListId,
                    AppointmentTime = c1.AppointmentTime,
                    CYMMerchantId = c1.CYMMerchantId,
                    FirstPutInTime = Convert.ToDateTime(c1.OrderStatusdetails.Where(o => o.OrderStatus == "1")
                            .Select(o => o.OrderStatusCreateTime)
                            .FirstOrDefault()).ToString("yyyy-MM-dd HH:mm:ss"),
                    ClerkInfo = GetClerkInfoByOrderId(c1.NewCYMUsersFabricOrderId),
                    // GoodsList = new List<GOODSParameterDome>(),
                })
                .Join(NewShoppingCymUsersMessageBll.DbSession.Db.Set<NewShoppingCYMUsersMessage>(),
                    c => c.NewCYMUsersFabricOrderId, c => c.NewCYMUsersFabricOrderId, (c1, c2) => new
                    {
                        Remarks = c1.Remarks,
                        NewCYMUsersFabricOrderNumber = c1.NewCYMUsersFabricOrderNumber,
                        NewCYMUsersFabricOrderName = c1.NewCYMUsersFabricOrderName,
                        CYMUsersId = c1.CYMUsersId,
                        DiscountsPrice = c1.DiscountsPrice,
                        CYMUsersShoppingIntegralValues = c1.CYMUsersShoppingIntegralValues,
                        Num = c1.Num,
                        CYMUsersInvoiceName = c1.CYMUsersInvoiceName,
                        BaoYangType = c1.BaoYangType,
                        NewCYMUsersFabricOrderState = c1.NewCYMUsersFabricOrderState,
                        NewCYMUsersFabricOrderId = c1.NewCYMUsersFabricOrderId,
                        GOODSParameterListId = c1.GOODSParameterListId,
                        AppointmentTime = c1.AppointmentTime,
                        BYServeName = c2.BYServeName,
                        CYMMerchantId = c1.CYMMerchantId,
                        CYMMerchant = GetCYMMerchantNameFromCYMMerchantId(c1.CYMMerchantId),
                        CheBaoCarCompositeId = c2.CheBaoCarCompositeId,
                        CheBaoCarCompositeName =
                            GetCheBaoCarCompositeNameForCheBaoCarCompositeId(c2.CheBaoCarCompositeId),
                        CarNumber = c2.CarNumber,
                        BYServeTelPhone = c2.BYServeTelPhone,
                        FirstPutInTime = c1.FirstPutInTime,
                        PushType =
                            GetPushTypeByNewCYMUsersFabricOrderState(c1.NewCYMUsersFabricOrderState,
                                c1.NewCYMUsersFabricOrderName),
                        ClerkName = c1.ClerkInfo.ClerkName,
                        PaySours = c1.ClerkInfo.PaySours,
                        FinanceRemark = c1.ClerkInfo.FinanceRemark,
                        ServerRemark = c1.ClerkInfo.ServerRemark,
                        CarMileage = c1.ClerkInfo.CarMileage,
                        CYMVoucherFaceValue = c1.ClerkInfo.CYMVoucherFaceValue,
                        FinanceStatus = GetFinanceStatusStringForNum(c1.ClerkInfo.FinanceStatus),
                        CYMTakeOutList = GetCYMTakeOutByOrderId(c1.NewCYMUsersFabricOrderId).Select(c => new
                        {
                            TakeOutId = c.TakeOutId,
                            TakeOutNumber = c.TakeOutNumber,
                            TakeOutMan = c.TakeOutMan,
                            PurchaseState = c.PurchaseState == 0 ? "待审核" : c.PurchaseState == 1 ? "已审核" : c.PurchaseState == 2 ? "已作废" : "未知",
                            CherkMan = c.CherkMan,
                            TakeOutTime = c.TakeOutTime,
                            Remarks = c.Remarks,
                            UpdateTime = c.UpdateTime,
                            StorageName = c.CYMStorage.StorageName,
                            NewCYMUsersFabricOrderId = c.NewCYMUsersFabricOrderId,
                            TakeOutDepartment = c.TakeOutDepartment,

                            CYMTakeOutGoods = c.CYMTakeOutGoods.Select(p => new
                            {
                                TakeOutGoodsId = p.TakeOutGoodsId,
                                GOODSName = p.GOODSParameterDome.GOODSName,
                                TakeOutNum = p.TakeOutNum,
                                AvgPurchasePrice = p.GOODSParameterDome.CYMPurchaseGoods.Where(r => r.CYMPurchas.PurchaseState == 1).Count() > 0 ? (p.GOODSParameterDome.CYMPurchaseGoods.Where(r => r.CYMPurchas.PurchaseState == 1).Select(d => new { SumPrice = d.PurchasePrice * d.PurchaseNum }).Sum(w => w.SumPrice) / (p.GOODSParameterDome.CYMPurchaseGoods.Where(r => r.CYMPurchas.PurchaseState == 1).Sum(q => q.PurchaseNum) == 0 ? -1 : p.GOODSParameterDome.CYMPurchaseGoods.Sum(q => q.PurchaseNum))) : (p.GOODSParameterDome.CYMWarehouses.Where(a => a.StorageId == c.StorageId && a.GOODSParameterId == p.GOODSParameterId).FirstOrDefault().PurchasePrice)
                            })
                        }).Select(c => new
                        {
                            TakeOutId = c.TakeOutId,
                            TakeOutNumber = c.TakeOutNumber,
                            TakeOutMan = c.TakeOutMan,
                            PurchaseState = c.PurchaseState,
                            CherkMan = c.CherkMan,
                            NewCYMUsersFabricOrderId = c.NewCYMUsersFabricOrderId,
                            TakeOutTime = c.TakeOutTime,
                            Remarks = c.Remarks,
                            UpdateTime = c.UpdateTime,
                            TakeOutDepartment = c.TakeOutDepartment,
                            StorageName = c.StorageName,
                            CYMTakeOutGoods = c.CYMTakeOutGoods,
                            SumPrice = c.CYMTakeOutGoods.Select(p => p.AvgPurchasePrice * p.TakeOutNum).Sum(p => p)
                        }),
                        ShouldPrice = new List<decimal>(),
                        GoodsList = new List<string>(),

                    }).ToList();

            JavaScriptSerializer js = new JavaScriptSerializer();

            for (int i = 0; i < VIPCardConsumeInfoList.Count; i++)
            {


                decimal sumPrice = 0;
                string goodsInfos = "";
                List<string> goodidList =
                   VIPCardConsumeInfoList[i].GOODSParameterListId.Split(new char[] { '-' },
                       StringSplitOptions.RemoveEmptyEntries).ToList();
                var group = goodidList.GroupBy(c => c, c => c);
                foreach (var item in group)
                {
                    int goodid = int.Parse(item.Key);
                    GOODSParameterDome goodsParameterDome = new GOODSParameterDome();
                    goodsParameterDome = GOODSParameterDomeBll.LoadEntities(c => c.GOODSParameterId == goodid).FirstOrDefault();

                    string newgoodsParameterDome = JsonConvert.SerializeObject(goodsParameterDome);
                    goodsParameterDome = JsonConvert.DeserializeObject<GOODSParameterDome>(newgoodsParameterDome);
                    goodsParameterDome.Num = item.Count().ToString();
                    sumPrice += (decimal)goodsParameterDome.GOODSPrice * int.Parse(goodsParameterDome.Num);
                    goodsInfos += "产品名:" + goodsParameterDome.GOODSName + ",价格:" + goodsParameterDome.GOODSPrice + ",数量:" + goodsParameterDome.Num + "、";
                }
                VIPCardConsumeInfoList[i].GoodsList.Add(goodsInfos.TrimEnd('、'));
                VIPCardConsumeInfoList[i].ShouldPrice.Add(sumPrice * VIPCardConsumeInfoList[i].Num);
            }



            var execlList = VIPCardConsumeInfoList.Select(c1 => new
            {
                订单号 = "'" + c1.NewCYMUsersFabricOrderNumber.ToString(),
                服务项目名称 = c1.NewCYMUsersFabricOrderName,
                服务类型 = c1.BaoYangType,
                购买店铺 = c1.CYMMerchant,
                应付 = c1.ShouldPrice[0],
                红包抵现 = c1.CYMVoucherFaceValue,
                使用积分 = c1.CYMUsersShoppingIntegralValues,
                实付 = c1.DiscountsPrice,
                支付时间 = c1.FirstPutInTime,
                支付方式 = c1.PaySours,
                购买方式 = c1.PushType,
                发票抬头 = c1.CYMUsersInvoiceName,
                姓名 = c1.BYServeName,
                服务手机号 = "'" + c1.BYServeTelPhone,
                车牌号 = c1.CarNumber,
                车型 = c1.CheBaoCarCompositeName,
                预约时间 = c1.AppointmentTime,
                操作员 = c1.ClerkName,
                备注 = c1.Remarks,
                数量 = c1.Num,
                产品详情 = c1.GoodsList[0],
                财务状态 = c1.FinanceStatus
            }).ToList();


            string content = ExcelHelper.WriteExcelString(ExcelHelper.IListOut(execlList));
            Response.Write(JsonConvert.SerializeObject(new { content = content, fileName = "GetFinancialStatistics" }));
            Response.End();

        }
        /// <summary>
        /// 根据财务状态吗获取状态描述
        /// </summary>
        /// <param name="financeStatus"></param>
        /// <returns></returns>
        private string GetFinanceStatusStringForNum(int? financeStatus)
        {
            switch (financeStatus)
            {
                case 1:
                    return "已核单";
                case 2:
                    return "错误单";
                case 3:
                    return "无效单";
                default:
                    return "未核单";
            }
        }

        /// <summary>
        /// 获得订单数据
        /// </summary>
        private void GetFinancialStatistics()
        {

            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount;
            var NewCYMUsersFabricOrderList = NewCYMUsersFabricOrderBll.LoadPageEntities(pageIndex, pageSize,
               out totalCount,
               c => c.NewCYMUsersFabricOrderState != "0|1|2|9" && c.NewCYMUsersFabricOrderState != "0" && c.NewCYMUsersFabricOrderState != "0|H1" && c.NewCYMUsersFabricOrderState != "0|3" && !c.NewCYMUsersFabricOrderState.EndsWith("|-3") && !c.NewCYMUsersFabricOrderState.EndsWith("|-0") && c.OrderStatusdetails.Where(o => o.OrderStatus == "1").Count() > 0
              , c => c.OrderStatusdetails.Where(o => o.OrderStatus == "1").Select(o => o.OrderStatusCreateTime).FirstOrDefault(), false).ToList();
            var VIPCardConsumeInfoList = NewCYMUsersFabricOrderList.Join(NewCymUsersShoppingInDcBll.DbSession.Db.Set<NewCYMUsersShoppingInDC>(),
                c => c.NewCYMUsersFabricOrderId, c => c.NewCYMUsersFabricOrderId, (c1, c2) => new
                {
                    Remarks = c2.Remarks,
                    CYMUsersInvoiceName = c1.CYMUsersInvoiceName,
                    NewCYMUsersFabricOrderNumber = c1.NewCYMUsersFabricOrderNumber,
                    NewCYMUsersFabricOrderName = c1.NewCYMUsersFabricOrderName,
                    CYMUsersId = c1.CYMUsersId,
                    BaoYangType = c2.BaoYangType,
                    Num = c2.Num,
                    CYMUsersShoppingIntegralValues = c1.CYMUsersShoppingIntegralValues,
                    DiscountsPrice = c1.DiscountsPrice,
                    NewCYMUsersFabricOrderState = c1.NewCYMUsersFabricOrderState,
                    NewCYMUsersFabricOrderId = c1.NewCYMUsersFabricOrderId,
                    GOODSParameterListId = c2.GOODSParameterListId,
                    AppointmentTime = c1.AppointmentTime,
                    CYMMerchantId = c1.CYMMerchantId,
                    Orderproductdetails = c1.Orderproductdetails,

                    FirstPutInTime = Convert.ToDateTime(c1.OrderStatusdetails.Where(o => o.OrderStatus == "1")
                            .Select(o => o.OrderStatusCreateTime)
                            .FirstOrDefault()).ToString("yyyy-MM-dd HH:mm:ss"),
                    ClerkInfo = GetClerkInfoByOrderId(c1.NewCYMUsersFabricOrderId),
                    // GoodsList = new List<GOODSParameterDome>(),
                }).Join(NewShoppingCymUsersMessageBll.DbSession.Db.Set<NewShoppingCYMUsersMessage>(), c => c.NewCYMUsersFabricOrderId, c => c.NewCYMUsersFabricOrderId, (c1, c2) => new
                {
                    Remarks = c1.Remarks,
                    NewCYMUsersFabricOrderNumber = c1.NewCYMUsersFabricOrderNumber,
                    NewCYMUsersFabricOrderName = c1.NewCYMUsersFabricOrderName,
                    CYMUsersId = c1.CYMUsersId,
                    DiscountsPrice = c1.DiscountsPrice,
                    CYMUsersShoppingIntegralValues = c1.CYMUsersShoppingIntegralValues,
                    Num = c1.Num,
                    CYMUsersInvoiceName = c1.CYMUsersInvoiceName,
                    BaoYangType = c1.BaoYangType,
                    NewCYMUsersFabricOrderState = c1.NewCYMUsersFabricOrderState,
                    NewCYMUsersFabricOrderId = c1.NewCYMUsersFabricOrderId,
                    GOODSParameterListId = c1.GOODSParameterListId,
                    AppointmentTime = c1.AppointmentTime,
                    BYServeName = c2.BYServeName,
                    CYMMerchantId = c1.CYMMerchantId,
                    CYMMerchant = GetCYMMerchantNameFromCYMMerchantId(c1.CYMMerchantId),//商家名字
                    CheBaoCarCompositeId = c2.CheBaoCarCompositeId,
                    CheBaoCarCompositeName = GetCheBaoCarCompositeNameForCheBaoCarCompositeId(c2.CheBaoCarCompositeId),//车型
                    CarNumber = c2.CarNumber,
                    BYServeTelPhone = c2.BYServeTelPhone,
                    FirstPutInTime = c1.FirstPutInTime,
                    PushType = GetPushTypeByNewCYMUsersFabricOrderState(c1.NewCYMUsersFabricOrderState, c1.NewCYMUsersFabricOrderName),//订单方式
                    ClerkName = c1.ClerkInfo.ClerkName,
                    ClerkInfo = c1.ClerkInfo,
                    PaySours = c1.ClerkInfo.PaySours,
                    FinanceRemark = c1.ClerkInfo.FinanceRemark,
                    ServerRemark = c1.ClerkInfo.ServerRemark,
                    CarMileage = c1.ClerkInfo.CarMileage,
                    FinanceStatus = GetFinanceStatusStringForNum(c1.ClerkInfo.FinanceStatus),//财务
                    //领料单
                    CYMTakeOutList = GetCYMTakeOutByOrderId(c1.NewCYMUsersFabricOrderId).Select(c => new
                    {
                        TakeOutId = c.TakeOutId,
                        TakeOutNumber = c.TakeOutNumber,
                        TakeOutMan = c.TakeOutMan,
                        PurchaseState = c.PurchaseState == 0 ? "待审核" : c.PurchaseState == 1 ? "已审核" : c.PurchaseState == 2 ? "已作废" : "未知",
                        CherkMan = c.CherkMan,
                        TakeOutTime = c.TakeOutTime,
                        Remarks = c.Remarks,
                        UpdateTime = c.UpdateTime,
                        StorageName = c.CYMStorage.StorageName,
                        NewCYMUsersFabricOrderId = c.NewCYMUsersFabricOrderId,
                        TakeOutDepartment = c.TakeOutDepartment,

                        CYMTakeOutGoods = c.CYMTakeOutGoods.Select(p => new
                        {
                            TakeOutGoodsId = p.TakeOutGoodsId,
                            GOODSName = p.GOODSParameterDome.GOODSName,
                            TakeOutNum = p.TakeOutNum,
                            AvgPurchasePrice = p.GOODSParameterDome.CYMPurchaseGoods.Where(r => r.CYMPurchas.PurchaseState == 1).Count() > 0 ? (p.GOODSParameterDome.CYMPurchaseGoods.Where(r => r.CYMPurchas.PurchaseState == 1).Select(d => new { SumPrice = d.PurchasePrice * d.PurchaseNum }).Sum(w => w.SumPrice) / (p.GOODSParameterDome.CYMPurchaseGoods.Where(r => r.CYMPurchas.PurchaseState == 1).Sum(q => q.PurchaseNum) == 0 ? -1 : p.GOODSParameterDome.CYMPurchaseGoods.Sum(q => q.PurchaseNum))) : (p.GOODSParameterDome.CYMWarehouses.Where(a => a.StorageId == c.StorageId && a.GOODSParameterId == p.GOODSParameterId).FirstOrDefault().PurchasePrice)
                        })
                    }).Select(c => new
                    {
                        TakeOutId = c.TakeOutId,
                        TakeOutNumber = c.TakeOutNumber,
                        TakeOutMan = c.TakeOutMan,
                        PurchaseState = c.PurchaseState,
                        CherkMan = c.CherkMan,
                        NewCYMUsersFabricOrderId = c.NewCYMUsersFabricOrderId,
                        TakeOutTime = c.TakeOutTime,
                        Remarks = c.Remarks,
                        UpdateTime = c.UpdateTime,
                        TakeOutDepartment = c.TakeOutDepartment,
                        StorageName = c.StorageName,
                        CYMTakeOutGoods = c.CYMTakeOutGoods,
                        SumPrice = c.CYMTakeOutGoods.Select(p => p.AvgPurchasePrice * p.TakeOutNum).Sum(p => p)
                    }),
                    CYMVoucherFaceValue = c1.ClerkInfo.CYMVoucherFaceValue,
                    ShouldPrice = new List<decimal>(),
                    GoodsList = new List<GOODSParameterDome>(),

                }).ToList();

            //string s = ExcelHelper.WriteExcel(ExcelHelper.IListOut(VIPCardConsumeInfoList));
            StringBuilder allsb = new StringBuilder();
            StringBuilder qwsb = new StringBuilder();
            StringBuilder qwcsb = new StringBuilder();

            var temp = VIPCardConsumeInfoList.GroupBy(c => c.PaySours);

            foreach (var item in temp)
            {
                allsb.Append(item.Key);
                allsb.Append(":￥");
                allsb.Append(item.Sum(c => c.DiscountsPrice) + "<br/>");
                qwsb.Append(item.Key);
                qwsb.Append(":￥");
                qwsb.Append(item.Where(c => c.FinanceStatus != "无效单").Sum(c => c.DiscountsPrice) + "<br/>");
                qwcsb.Append(item.Key);
                qwcsb.Append(":￥");
                qwcsb.Append(item.Where(c => c.FinanceStatus != "无效单" && c.FinanceStatus != "错误单").Sum(c => c.DiscountsPrice) + "<br/>");
            }






            JavaScriptSerializer js = new JavaScriptSerializer();


            decimal yingFuDecimal = 0;
            decimal shiFuDecimal = 0;
            decimal jiFenDecimal = 0;
            decimal hongBaoDecimal = 0;



            decimal qwyingFuDecimal = 0;
            decimal qwshiFuDecimal = 0;
            decimal qwjiFenDecimal = 0;
            decimal qwhongBaoDecimal = 0;



            decimal qwcyingFuDecimal = 0;
            decimal qwcshiFuDecimal = 0;
            decimal qwcjiFenDecimal = 0;
            decimal qwchongBaoDecimal = 0;



            for (int i = 0; i < VIPCardConsumeInfoList.Count; i++)
            {
                decimal sumPrice = 0;
                List<string> goodidList =
                   VIPCardConsumeInfoList[i].GOODSParameterListId.Split(new char[] { '-' },
                       StringSplitOptions.RemoveEmptyEntries).ToList();
                var group = goodidList.GroupBy(c => c, c => c);
                foreach (var item in group)
                {
                    int goodid = int.Parse(item.Key);
                    GOODSParameterDome goodsParameterDome = new GOODSParameterDome();
                    goodsParameterDome = GOODSParameterDomeBll.LoadEntities(c => c.GOODSParameterId == goodid).FirstOrDefault();

                    // string newgoodsParameterDome = js.Serialize(goodsParameterDome);
                    string newgoodsParameterDome = JsonConvert.SerializeObject(goodsParameterDome);
                    goodsParameterDome = JsonConvert.DeserializeObject<GOODSParameterDome>(newgoodsParameterDome);
                    goodsParameterDome.Num = item.Count().ToString();
                    sumPrice += (decimal)goodsParameterDome.GOODSPrice * int.Parse(goodsParameterDome.Num);
                    VIPCardConsumeInfoList[i].GoodsList.Add(goodsParameterDome);
                }
                VIPCardConsumeInfoList[i].ShouldPrice.Add(sumPrice * VIPCardConsumeInfoList[i].Num);
                yingFuDecimal += sumPrice * VIPCardConsumeInfoList[i].Num;
                shiFuDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                jiFenDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMUsersShoppingIntegralValues);
                hongBaoDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMVoucherFaceValue == null ? 0 : VIPCardConsumeInfoList[i].CYMVoucherFaceValue);
                //if (VIPCardConsumeInfoList[i].PushType.Contains("APP支付"))
                //{
                //    xsshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                //}
                //else
                //{
                //    xxshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                //}

                if (VIPCardConsumeInfoList[i].FinanceStatus != "无效单")
                {
                    qwyingFuDecimal += sumPrice * VIPCardConsumeInfoList[i].Num;
                    qwshiFuDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    qwjiFenDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMUsersShoppingIntegralValues);
                    qwhongBaoDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMVoucherFaceValue == null ? 0 : VIPCardConsumeInfoList[i].CYMVoucherFaceValue);
                    //if (VIPCardConsumeInfoList[i].PushType.Contains("APP"))
                    //{
                    //    qwxsshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    //}
                    //else
                    //{
                    //    qwxxshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    //}
                }

                if (VIPCardConsumeInfoList[i].FinanceStatus != "无效单" && VIPCardConsumeInfoList[i].FinanceStatus != "错误单")
                {
                    qwcyingFuDecimal += sumPrice * VIPCardConsumeInfoList[i].Num;
                    qwcshiFuDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    qwcjiFenDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMUsersShoppingIntegralValues);
                    qwchongBaoDecimal += Convert.ToDecimal(VIPCardConsumeInfoList[i].CYMVoucherFaceValue == null ? 0 : VIPCardConsumeInfoList[i].CYMVoucherFaceValue);
                    //if (VIPCardConsumeInfoList[i].PushType.Contains("APP"))
                    //{
                    //    qwcxsshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    //}
                    //else
                    //{
                    //    qwcxxshifu += Convert.ToDecimal(VIPCardConsumeInfoList[i].DiscountsPrice);
                    //}
                }
            }

            var Allfooter =
              new
              {
                  NewCYMUsersFabricOrderNumber = "全部合计",
                  ShouldPrice = new decimal[] { yingFuDecimal },
                  DiscountsPrice = shiFuDecimal,
                  CYMUsersShoppingIntegralValues = jiFenDecimal,
                  CYMVoucherFaceValue = hongBaoDecimal,
                  FirstPutInTime = allsb.ToString()
              };

            var QWfooter =
                 new
                 {
                     NewCYMUsersFabricOrderNumber = "去掉无效单合计",
                     ShouldPrice = new decimal[] { qwyingFuDecimal },
                     DiscountsPrice = qwshiFuDecimal,
                     CYMUsersShoppingIntegralValues = qwjiFenDecimal,
                     CYMVoucherFaceValue = qwhongBaoDecimal,
                     FirstPutInTime = qwsb.ToString(),

                 };
            var QWCfooter =
                new
                {
                    NewCYMUsersFabricOrderNumber = "去掉无效|错误单合计",
                    ShouldPrice = new decimal[] { qwcyingFuDecimal },
                    DiscountsPrice = qwcshiFuDecimal,
                    CYMUsersShoppingIntegralValues = qwcjiFenDecimal,
                    CYMVoucherFaceValue = qwchongBaoDecimal,
                    FirstPutInTime = qwcsb.ToString()

                };
            List<string> tex = new List<string>();

            foreach (var item in VIPCardConsumeInfoList)
            {
                if (tex.Contains(item.NewCYMUsersFabricOrderName))
                {
                    break;
                }
                else
                {
                    tex.Add(item.NewCYMUsersFabricOrderName);
                }
            }
            string htmls;
            htmls = "服务项目：<select> <option>------全部------</option> ";
            foreach (var item in tex)
            {
                htmls += "<option>" + item + "</option> ";
            }
            htmls += "</select>";
            TextDiv = htmls;
            FabricOrderNames.InnerHtml = htmls;


            Response.Write("<script>  $(function () { $('#FabricOrderNames').html(" + htmls + ") })</script>");
            Response.Write(JsonConvert.SerializeObject(new { rows = VIPCardConsumeInfoList, total = totalCount, footer = new List<object> { QWCfooter, QWfooter, Allfooter } }));
            Response.End();
        }

        /// <summary>
        /// 根据订单id获得clerkandorder
        /// </summary>
        /// <param name="NewCYMUsersFabricOrderId"></param>
        /// <returns></returns>
        private ClerkAndOrder GetClerkInfoByOrderId(int NewCYMUsersFabricOrderId)
        {
            ClerkAndOrder clerkAndOrder = ClerkAndOrderBll.LoadEntities(c => c.NewCYMUsersFabricOrderId == NewCYMUsersFabricOrderId).FirstOrDefault();
            if (clerkAndOrder != null)
            {
                if (clerkAndOrder.ClerkId == null)
                {
                    clerkAndOrder.ClerkName = "用户自主";
                }
                else
                {
                    CYMClerkInfo cymClerkInfo = CymClerkInfoBll.LoadEntities(c => c.ClerkId == clerkAndOrder.ClerkId).FirstOrDefault();
                    if (cymClerkInfo == null)
                    {
                        clerkAndOrder.ClerkName = "Admin";
                    }
                    else
                    {
                        clerkAndOrder.ClerkName = cymClerkInfo.ClerkJob + ":" + cymClerkInfo.ClerkName;
                    }
                }
                return clerkAndOrder;
            }
            else
            {
                return new ClerkAndOrder() { ClerkName = "用户自主", PaySours = "车姨妈APP" };
            }

        }
        /// <summary>
        /// 根据订单状态码获得订单方式
        /// </summary>
        /// <param name="newCymUsersFabricOrderState"></param>
        /// <param name="NewCYMUsersFabricOrderName"></param>
        /// <returns></returns>
        private string GetPushTypeByNewCYMUsersFabricOrderState(string newCymUsersFabricOrderState, string NewCYMUsersFabricOrderName)
        {
            if (newCymUsersFabricOrderState.StartsWith("0|1|7"))
            {
                return "会员卡消次服务";
            }
            else if (newCymUsersFabricOrderState.StartsWith("0|1|6"))
            {
                if (NewCYMUsersFabricOrderName.StartsWith("会员卡推送"))
                {
                    return "门店推送门店支付办新卡";
                }
                else
                {
                    return "客户手机下单APP支付办新卡";
                }
            }
            else if (newCymUsersFabricOrderState.StartsWith("0|1|-6"))
            {
                return "门店补老卡";
            }
            else if (newCymUsersFabricOrderState.StartsWith("0|1|8"))
            {
                if (NewCYMUsersFabricOrderName.StartsWith("会员卡推送"))
                {
                    return "门店推送手机APP支付";
                }
                else
                {
                    return "客户手机下单APP支付";
                }
                ;
            }
            else if (newCymUsersFabricOrderState.StartsWith("0|H1|2"))
            {
                if (NewCYMUsersFabricOrderName.StartsWith("商家推送"))
                {
                    return "门店推送门店支付";
                }
                else
                {
                    return "手机下单门店支付";
                }
            }
            else if (newCymUsersFabricOrderState.StartsWith("0|H1") && newCymUsersFabricOrderState.Contains("|1"))
            {
                if (NewCYMUsersFabricOrderName.StartsWith("商家推送"))
                {
                    return "门店推送门店支付";
                }
                else
                {
                    return "手机下单门店支付";
                }
            }
            else if (newCymUsersFabricOrderState == "0|3")
            {
                return "门店推送挂单状态";
            }
            else if (newCymUsersFabricOrderState.StartsWith("0|3|1"))
            {
                return "门店推送门店支付";
            }
            else if (newCymUsersFabricOrderState == "0")
            {
                return "未支付";
            }
            else if (newCymUsersFabricOrderState.StartsWith("0|1"))
            {
                if (NewCYMUsersFabricOrderName.StartsWith("商家推送"))
                {
                    return "门店推送APP支付";
                }
                else
                {
                    return "手机下单APP支付";
                }

            }
            else
            {
                return "未知状态:" + newCymUsersFabricOrderState;
            }
        }
        /// <summary>
        /// 根据商家id获得商家名字
        /// </summary>
        /// <param name="CYMMerchantId"></param>
        /// <returns></returns>
        private string GetCYMMerchantNameFromCYMMerchantId(int CYMMerchantId)
        {
            return CymMerchantBll.LoadEntities(c => c.CYMMerchantId == CYMMerchantId).FirstOrDefault().business_name;
        }
        /// <summary>
        /// 根据车型id获得车型名字
        /// </summary>
        /// <param name="CheBaoCarCompositeId"></param>
        /// <returns></returns>
        private string GetCheBaoCarCompositeNameForCheBaoCarCompositeId(int CheBaoCarCompositeId)
        {
            CheBaoCarComposite cheBaoCarComposite = CheBaoCarCompositeBll.LoadEntities(b => b.CheBaoCarCompositeId == CheBaoCarCompositeId).FirstOrDefault();
            if (cheBaoCarComposite == null)
            {
                return "";
            }
            return cheBaoCarComposite.CheBaoCarBrandName + " " + cheBaoCarComposite.CheBaoCarLineName + " " + cheBaoCarComposite.CheBaoCarMJName + " " +
                   cheBaoCarComposite.CheBaoCarDisplacementName;
        }
        /// <summary>
        /// 根据订单id获得领料单
        /// </summary>
        /// <param name="NewCYMUsersFabricOrderId"></param>
        /// <returns></returns>
        private List<CYMTakeOut> GetCYMTakeOutByOrderId(int NewCYMUsersFabricOrderId)
        {

            List<CYMTakeOut> cymTakeOuts =
                CymTakeOutBll.LoadEntities(c => c.NewCYMUsersFabricOrderId == NewCYMUsersFabricOrderId).ToList();

            return cymTakeOuts;

        }

        private void LoadBllEntity()
        {
            BllCheBaoSession chebaobllSession = BllSessionFactory.CreateCheBaoBllSession();
            BllSession CQbllSession = BllSessionFactory.CreateBllSession();
            BllB2CSession B2CbllSession = BllSessionFactory.CreateB2CBllSession();
            this.CymUsersBll = CQbllSession.CYMUsersBll;
            this.CymGarageBll = CQbllSession.CYMGarageBll;
            this.CheBaoCarCompositeBll = chebaobllSession.CheBaoCarCompositeBll;
            this.CYMUsersCarMessageBll = CQbllSession.CYMUsersCarMessageBll;
            this.CYMUsersMessagesBll = CQbllSession.CYMUsersMessagesBll;
            this.CheBaoCarBrandBll = chebaobllSession.CheBaoCarBrandBll;
            this.CheBaoCarLineBll = chebaobllSession.CheBaoCarLineBll;
            this.GOODSParameterDomeBll = CQbllSession.GOODSParameterDomeBll;
            this.CymowMerchantLoginBll = CQbllSession.CYMOWMerchantLoginBll;
            this.CymowMerchantUsersInnerCymMerchantBll = CQbllSession.CYMOWMerchantUsersInnerCYMMerchantBll;
            this.NewCYMUsersFabricOrderBll = CQbllSession.NewCYMUsersFabricOrderBll;
            this.NewCymUsersShoppingInDcBll = CQbllSession.NewCYMUsersShoppingInDCBll;
            this.NewShoppingCymUsersMessageBll = CQbllSession.NewShoppingCYMUsersMessageBll;
            this.CymMerchantBll = CQbllSession.CYMMerchantBll;
            this.TCmCodeBll = B2CbllSession.T_Cm_CodeBll;
            this.TypeTreeBll = B2CbllSession.T_Cm_TypeTreeBll;
            this.CymMerchantComBoBll = CQbllSession.CYMMerchantComBoBll;
            this.ClerkAndOrderBll = CQbllSession.ClerkAndOrderBll;
            this.CymClerkInfoBll = CQbllSession.CYMClerkInfoBll;
            this.ClerkLogInfoBll = CQbllSession.ClerkLogInfoBll;
            this.CymTakeOutBll = CQbllSession.CYMTakeOutBll;
        }




        #region 登录验证
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <returns></returns>
        private bool AdminLogin()
        {
            try
            {
                string CYMAdminUserName = Session["username"].ToString();
                string CYMAdminPassWord = Session["password"].ToString();
                if ("true" == CYMAdminLoginBll.SelectCYMAdminLoginBll(
                    new CYMAdminLogin()
                    {
                        CYMAdminUserName = CYMAdminUserName,
                        CYMAdminPassWord = CYMAdminPassWord
                    }
                    ))
                {
                    Session.Add("username", CYMAdminUserName);
                    Session.Add("password", CYMAdminPassWord);
                    return true;
                }
            }
            catch
            {
                return false;

            }
            return false;
        }
        #endregion
    }
}