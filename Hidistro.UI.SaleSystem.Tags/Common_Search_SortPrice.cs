namespace Hidistro.UI.SaleSystem.Tags
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Common_Search_SortPrice : LinkButton
    {
        
        private string _Alt;
        
        private string _AscImageUrl;
        
        private string _DefaultImageUrl;
        
        private string _DescImageUrl;
        private string imageFormat = "<img border=\"0\" src=\"{0}\" alt=\"{1}\" />";
        private Hidistro.UI.SaleSystem.Tags.ImagePosition position;
        private bool showText = true;
        private Common_Search_SortTime.SortingHandler Sorting;
        public const string TagID = "btn_Common_Search_SortPrice";

        public event Common_Search_SortTime.SortingHandler _Sorting
        {
            add
            {
                Common_Search_SortTime.SortingHandler handler2;
                Common_Search_SortTime.SortingHandler sorting = this.Sorting;
                do
                {
                    handler2 = sorting;
                    Common_Search_SortTime.SortingHandler handler3 = (Common_Search_SortTime.SortingHandler) Delegate.Combine(handler2, value);
                    sorting = Interlocked.CompareExchange<Common_Search_SortTime.SortingHandler>(ref this.Sorting, handler3, handler2);
                }
                while (sorting != handler2);
            }
            remove
            {
                Common_Search_SortTime.SortingHandler handler2;
                Common_Search_SortTime.SortingHandler sorting = this.Sorting;
                do
                {
                    handler2 = sorting;
                    Common_Search_SortTime.SortingHandler handler3 = (Common_Search_SortTime.SortingHandler) Delegate.Remove(handler2, value);
                    sorting = Interlocked.CompareExchange<Common_Search_SortTime.SortingHandler>(ref this.Sorting, handler3, handler2);
                }
                while (sorting != handler2);
            }
        }

        public Common_Search_SortPrice()
        {
            base.ID = "btn_Common_Search_SortPrice";
        }

        private void Common_Search_SortPrice_Click(object sender, EventArgs e)
        {
            string sortOrder = string.Empty;
            if (this.Page.Request.QueryString["sortOrder"] == "Desc")
            {
                sortOrder = "Asc";
            }
            else
            {
                sortOrder = "Desc";
            }
            this.OnSorting(sortOrder, "SalePrice");
        }

        private string GetImageTag()
        {
            if (string.IsNullOrEmpty(this.ImageUrl))
            {
                return string.Empty;
            }
            return string.Format(CultureInfo.InvariantCulture, this.imageFormat, new object[] { this.ImageUrl, this.Alt });
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.Click += new EventHandler(this.Common_Search_SortPrice_Click);
        }

        private void OnSorting(string sortOrder, string sortOrderBy)
        {
            if (this.Sorting != null)
            {
                this.Sorting(sortOrder, sortOrderBy);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["sortOrder"]) && (this.Page.Request.QueryString["sortOrderBy"] == "SalePrice"))
            {
                if (this.Page.Request.QueryString["sortOrder"] == "Desc")
                {
                    this.ImageUrl = this.DescImageUrl;
                    this.Alt = "按价格升序排序";
                }
                else
                {
                    this.ImageUrl = this.AscImageUrl;
                    this.Alt = "按价格降序排序";
                }
                this.ToolTip = this.Alt;
            }
            else
            {
                this.ImageUrl = this.DefaultImageUrl;
                this.ToolTip = "按价格排序";
            }
            base.Attributes.Add("name", this.NamingContainer.UniqueID + "$" + this.ID);
            string imageTag = this.GetImageTag();
            if (!this.ShowText)
            {
                base.Text = "";
            }
            if (this.ImagePosition == Hidistro.UI.SaleSystem.Tags.ImagePosition.Right)
            {
                base.Text = base.Text + imageTag;
            }
            else
            {
                base.Text = imageTag + base.Text;
            }
            base.Render(writer);
        }

        public string Alt
        {
            
            get
            {
                return _Alt;
            }
            
            set
            {
                _Alt = value;
            }
        }

        public string AscImageUrl
        {
            
            get
            {
                return _AscImageUrl;
            }
            
            set
            {
                _AscImageUrl = value;
            }
        }

        public string DefaultImageUrl
        {
            
            get
            {
                return _DefaultImageUrl;
            }
            
            set
            {
                _DefaultImageUrl = value;
            }
        }

        public string DescImageUrl
        {
            
            get
            {
                return _DescImageUrl;
            }
            
            set
            {
                _DescImageUrl = value;
            }
        }

        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
            }
        }

        public Hidistro.UI.SaleSystem.Tags.ImagePosition ImagePosition
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                if (this.ViewState["Src"] != null)
                {
                    return (string) this.ViewState["Src"];
                }
                return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.ViewState["Src"] = value;
                }
                else
                {
                    this.ViewState["Src"] = null;
                }
            }
        }

        public bool ShowText
        {
            get
            {
                return this.showText;
            }
            set
            {
                this.showText = value;
            }
        }
    }
}

