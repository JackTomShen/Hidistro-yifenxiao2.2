namespace Hidistro.UI.Web.Shopadmin
{
    using Hidistro.Entities.Commodities;
    using Hidistro.Subsites.Commodities;
    using Hidistro.UI.Subsites.Utility;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class SelectMyCategory : DistributorPage
    {
        private void DoCallback()
        {
            string str = base.Request.QueryString["action"];
            base.Response.Clear();
            base.Response.ContentType = "application/json";
            if (str.Equals("getlist"))
            {
                int result = 0;
                int.TryParse(base.Request.QueryString["parentCategoryId"], out result);
                IList<CategoryInfo> categories = (result == 0) ? SubsiteCatalogHelper.GetMainCategories() : SubsiteCatalogHelper.GetSubCategories(result);
                if ((categories == null) || (categories.Count == 0))
                {
                    base.Response.Write("{\"Status\":\"0\"}");
                }
                else
                {
                    base.Response.Write(this.GenerateJson(categories));
                }
            }
            else if (str.Equals("getinfo"))
            {
                int num2 = 0;
                int.TryParse(base.Request.QueryString["categoryId"], out num2);
                if (num2 <= 0)
                {
                    base.Response.Write("{\"Status\":\"0\"}");
                }
                else
                {
                    CategoryInfo category = SubsiteCatalogHelper.GetCategory(num2);
                    if (category == null)
                    {
                        base.Response.Write("{\"Status\":\"0\"}");
                    }
                    else
                    {
                        base.Response.Write("{\"Status\":\"OK\", \"Name\":\"" + category.Name + "\", \"Path\":\"" + category.Path + "\"}");
                    }
                }
            }
            base.Response.End();
        }

        private string GenerateJson(IList<CategoryInfo> categories)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.Append("\"Status\":\"OK\",");
            builder.Append("\"Categories\":[");
            foreach (CategoryInfo info in categories)
            {
                builder.Append("{");
                builder.AppendFormat("\"CategoryId\":\"{0}\",", info.CategoryId.ToString(CultureInfo.InvariantCulture));
                builder.AppendFormat("\"HasChildren\":\"{0}\",", info.HasChildren ? "true" : "false");
                builder.AppendFormat("\"CategoryName\":\"{0}\"", info.Name);
                builder.Append("},");
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append("]}");
            return builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(base.Request.QueryString["isCallback"]) && (base.Request.QueryString["isCallback"] == "true"))
            {
                this.DoCallback();
            }
        }
    }
}

