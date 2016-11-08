namespace Hidistro.UI.Web.Shopadmin.sales
{
    using ASPNET.WebControls;
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Core.Enums;
    using Hidistro.Entities.Sales;
    using Hidistro.Subsites.Sales;
    using Hidistro.UI.Common.Controls;
    using Hidistro.UI.Subsites.Utility;
    using System;
    using System.Collections.Specialized;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ReturnsApply : DistributorPage
    {
        protected Button btnAcceptReturn;
        protected Button btnRefuseReturn;
        protected Button btnSearchButton;
        protected DropDownList ddlHandleStatus;
        protected Repeater dlstReturns;
        protected HtmlInputHidden hidOrderId;
        protected HtmlInputHidden hidOrderTotal;
        protected HtmlInputHidden hidRefundType;
        protected PageSize hrefPageSize;
        protected Label Label1;
        protected Label Label2;
        protected Label Label3;
        protected Label Label4;
        protected Label Label5;
        protected Label Label6;
        protected Label Label7;
        protected Label Label8;
        protected Label lblStatus;
        protected ImageLinkButton lkbtnDeleteCheck;
        protected Pager pager;
        protected Pager pager1;
        protected Label return_lblAddress;
        protected Label return_lblContacts;
        protected Label return_lblEmail;
        protected Label return_lblOrderId;
        protected Label return_lblOrderTotal;
        protected Label return_lblRefundType;
        protected Label return_lblReturnRemark;
        protected Label return_lblTelephone;
        protected HtmlTextArea return_txtAdminRemark;
        protected TextBox return_txtRefundMoney;
        protected TextBox TextBox1;
        protected TextBox TextBox2;
        protected TextBox txtOrderId;

        private void BindReturns()
        {
            ReturnsApplyQuery returnsQuery = this.GetReturnsQuery();
            DbQueryResult returnsApplys = SubsiteSalesHelper.GetReturnsApplys(returnsQuery);
            this.dlstReturns.DataSource = returnsApplys.Data;
            this.dlstReturns.DataBind();
            this.pager.TotalRecords = returnsApplys.TotalRecords;
            this.pager1.TotalRecords = returnsApplys.TotalRecords;
            this.txtOrderId.Text = returnsQuery.OrderId;
            this.ddlHandleStatus.SelectedIndex = 0;
            if (returnsQuery.HandleStatus.HasValue && (returnsQuery.HandleStatus.Value > -1))
            {
                this.ddlHandleStatus.SelectedValue = returnsQuery.HandleStatus.Value.ToString();
            }
        }

        protected void btnAcceptReturn_Click(object sender, EventArgs e)
        {
            decimal num;
            if (!decimal.TryParse(this.return_txtRefundMoney.Text, out num))
            {
                this.ShowMsg("退款金额需为数字格式", false);
            }
            else
            {
                decimal num2;
                decimal.TryParse(this.hidOrderTotal.Value, out num2);
                if (num > num2)
                {
                    this.ShowMsg("退款金额不能大于订单金额", false);
                }
                else
                {
                    SubsiteSalesHelper.CheckReturn(SubsiteSalesHelper.GetOrderInfo(this.hidOrderId.Value), num, this.return_txtAdminRemark.Value, int.Parse(this.hidRefundType.Value), true);
                    this.BindReturns();
                    this.ShowMsg("成功的确认了订单退货", true);
                }
            }
        }

        protected void btnRefuseReturn_Click(object sender, EventArgs e)
        {
            SubsiteSalesHelper.CheckReturn(SubsiteSalesHelper.GetOrderInfo(this.hidOrderId.Value), 0M, this.return_txtAdminRemark.Value, int.Parse(this.hidRefundType.Value), false);
            this.BindReturns();
            this.ShowMsg("成功的拒绝了订单退货", true);
        }

        private void btnSearchButton_Click(object sender, EventArgs e)
        {
            this.ReloadReturns(true);
        }

        private void dlstReturns_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                HtmlAnchor anchor = (HtmlAnchor) e.Item.FindControl("lkbtnCheckReturns");
                Label label = (Label) e.Item.FindControl("lblHandleStatus");
                if (label.Text == "0")
                {
                    anchor.Visible = true;
                    label.Text = "待处理";
                }
                else if (label.Text == "1")
                {
                    label.Text = "已处理";
                }
                else
                {
                    label.Text = "已拒绝";
                }
            }
        }

        private ReturnsApplyQuery GetReturnsQuery()
        {
            ReturnsApplyQuery query = new ReturnsApplyQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderId"]))
            {
                query.OrderId = Globals.UrlDecode(this.Page.Request.QueryString["OrderId"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["HandleStatus"]))
            {
                int result = 0;
                if (int.TryParse(this.Page.Request.QueryString["HandleStatus"], out result) && (result > -1))
                {
                    query.HandleStatus = new int?(result);
                }
            }
            query.PageIndex = this.pager.PageIndex;
            query.PageSize = this.pager.PageSize;
            query.SortBy = "ApplyForTime";
            query.SortOrder = SortAction.Desc;
            return query;
        }

        private void lkbtnDeleteCheck_Click(object sender, EventArgs e)
        {
            string str = "";
            if (!string.IsNullOrEmpty(base.Request["CheckBoxGroup"]))
            {
                str = base.Request["CheckBoxGroup"];
            }
            if (str.Length <= 0)
            {
                this.ShowMsg("请选要删除的退货申请单", false);
            }
            else
            {
                int num;
                string format = "成功删除了{0}个退货申请单";
                if (SubsiteSalesHelper.DelReturnsApply(str.Split(new char[] { ',' }), out num))
                {
                    format = string.Format(format, num);
                }
                else
                {
                    format = string.Format(format, num) + ",待处理的申请不能删除";
                }
                this.BindReturns();
                this.ShowMsg(format, true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.dlstReturns.ItemDataBound += new RepeaterItemEventHandler(this.dlstReturns_ItemDataBound);
            this.btnSearchButton.Click += new EventHandler(this.btnSearchButton_Click);
            this.lkbtnDeleteCheck.Click += new EventHandler(this.lkbtnDeleteCheck_Click);
            this.btnAcceptReturn.Click += new EventHandler(this.btnAcceptReturn_Click);
            this.btnRefuseReturn.Click += new EventHandler(this.btnRefuseReturn_Click);
            if (!base.IsPostBack)
            {
                this.BindReturns();
            }
        }

        private void ReloadReturns(bool isSearch)
        {
            NameValueCollection queryStrings = new NameValueCollection();
            queryStrings.Add("OrderId", this.txtOrderId.Text);
            queryStrings.Add("PageSize", this.pager.PageSize.ToString());
            if (!isSearch)
            {
                queryStrings.Add("pageIndex", this.pager.PageIndex.ToString());
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["GroupBuyId"]))
            {
                queryStrings.Add("GroupBuyId", this.Page.Request.QueryString["GroupBuyId"]);
            }
            if (!string.IsNullOrEmpty(this.ddlHandleStatus.SelectedValue))
            {
                queryStrings.Add("HandleStatus", this.ddlHandleStatus.SelectedValue);
            }
            base.ReloadPage(queryStrings);
        }
    }
}

