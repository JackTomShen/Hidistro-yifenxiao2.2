namespace Hidistro.UI.Web.Shopadmin
{
    using ASPNET.WebControls;
    using Hidistro.Entities.Promotions;
    using Hidistro.Subsites.Promotions;
    using Hidistro.UI.Common.Controls;
    using Hidistro.UI.Subsites.Utility;
    using System;
    using System.Web.UI.WebControls;

    public class AddMyCountDown : DistributorPage
    {
        protected Button btnAddCountDown;
        protected WebCalendar calendarEndDate;
        protected WebCalendar calendarStartDate;
        protected DistributorProductCategoriesDropDownList dropCategories;
        protected DistributorGroupBuyProductDropDownList dropGroupBuyProduct;
        protected HourDropDownList drophours;
        protected HourDropDownList HourDropDownList1;
        protected Label lblPrice;
        protected TextBox txtContent;
        protected TextBox txtMaxCount;
        protected TextBox txtPrice;
        protected TextBox txtSearchText;
        protected TextBox txtSKU;

        private void btnbtnAddCountDown_Click(object sender, EventArgs e)
        {
            int num;
            CountDownInfo countDownInfo = new CountDownInfo();
            string str = string.Empty;
            if (this.dropGroupBuyProduct.SelectedValue > 0)
            {
                if (SubsitePromoteHelper.ProductCountDownExist(this.dropGroupBuyProduct.SelectedValue.Value))
                {
                    this.ShowMsg("已经存在此商品的限时抢购活动", false);
                    return;
                }
                countDownInfo.ProductId = this.dropGroupBuyProduct.SelectedValue.Value;
            }
            else
            {
                str = str + Formatter.FormatErrorMessage("请选择限时抢购商品");
            }
            if (!this.calendarStartDate.SelectedDate.HasValue)
            {
                str = str + Formatter.FormatErrorMessage("请选择开始日期");
            }
            if (!this.calendarEndDate.SelectedDate.HasValue)
            {
                str = str + Formatter.FormatErrorMessage("请选择结束日期");
            }
            else
            {
                countDownInfo.EndDate = this.calendarEndDate.SelectedDate.Value.AddHours((double) this.HourDropDownList1.SelectedValue.Value);
                if (DateTime.Compare(countDownInfo.EndDate, DateTime.Now) <= 0)
                {
                    str = str + Formatter.FormatErrorMessage("结束日期必须要晚于今天日期");
                }
                else if (DateTime.Compare(this.calendarStartDate.SelectedDate.Value.AddHours((double) this.drophours.SelectedValue.Value), countDownInfo.EndDate) >= 0)
                {
                    str = str + Formatter.FormatErrorMessage("开始日期必须要早于结束日期");
                }
                else
                {
                    countDownInfo.StartDate = this.calendarStartDate.SelectedDate.Value.AddHours((double) this.drophours.SelectedValue.Value);
                }
            }
            if (int.TryParse(this.txtMaxCount.Text.Trim(), out num))
            {
                countDownInfo.MaxCount = num;
            }
            else
            {
                str = str + Formatter.FormatErrorMessage("限购数量不能为空，只能为整数");
            }
            if (!string.IsNullOrEmpty(this.txtPrice.Text))
            {
                decimal num2;
                if (decimal.TryParse(this.txtPrice.Text.Trim(), out num2))
                {
                    countDownInfo.CountDownPrice = num2;
                }
                else
                {
                    str = str + Formatter.FormatErrorMessage("价格填写格式不正确");
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                this.ShowMsg(str, false);
            }
            else
            {
                countDownInfo.Content = this.txtContent.Text;
                if (SubsitePromoteHelper.AddCountDown(countDownInfo))
                {
                    this.ShowMsg("添加限时抢购活动成功", true);
                }
                else
                {
                    this.ShowMsg("添加限时抢购活动失败", true);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnAddCountDown.Click += new EventHandler(this.btnbtnAddCountDown_Click);
            if (!this.Page.IsPostBack)
            {
                this.dropCategories.DataBind();
                this.dropGroupBuyProduct.DataBind();
                this.HourDropDownList1.DataBind();
                this.drophours.DataBind();
            }
        }
    }
}

