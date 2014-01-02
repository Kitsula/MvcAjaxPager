/*
 *  ASP .NET MVC AJAX Pager control 
 *  http://kitsula.com/MvcAjaxPager
 *  
 *  Copyright (c) 2012-2014 Igor Kitsula (http://kitsula.com)
 *  Copyright (c) 2009-2010 Webdiyer (http://en.webdiyer.com)
 *  Source code released under MIT license 
 *  http://kitsula.com/MvcAjaxPager/license
 *
 */
namespace MvcAjaxPager {

	public class PagerOptions {

		public PagerOptions() {
			AutoHide = true;
			PageIndexParameterName = "page";
			NumericPagerItemCount = 10;
			AlwaysShowFirstLastPageNumber = false;
			ShowPrevNext = true;
			PrevPageText = "Prev";
			NextPageText = "Next";
			ShowNumericPagerItems = true;
			ShowFirstLast = true;
			FirstPageText = "First";
			LastPageText = "Last";
			ShowMorePagerItems = true;
			MorePageText = "...";
			ShowDisabledPagerItems = true;
			SeparatorHtml = "&nbsp;&nbsp;";
			ContainerTagName = "div";
			MaximumPageIndexItems = 80;
			ContainerTagName = "div";
			InvalidPageIndexErrorMessage = "Invalid page index";
			PageIndexOutOfRangeErrorMessage = "Page index out of range";
			MaxPageIndex = 0;
			CurrentPageNumberFormatString = "<span data-pageIndex='{0}'>{0}</span>";
		}

		/// <summary>
		/// Whether or not hide control (render nothing) automatically when there's only one page
		/// </summary>
		public bool AutoHide { get; set; }
		
		/// <summary>
		/// PageIndexOutOfRangeErrorMessage
		/// </summary>
		public string PageIndexOutOfRangeErrorMessage { get; set; }
		
		/// <summary>
		/// InvalidPageIndexErrorMessage
		/// </summary>
		public string InvalidPageIndexErrorMessage { get; set; }

		/// <summary>
		/// Page index parameter name in url
		/// </summary>
		public string PageIndexParameterName { get; set; }
		
		/// <summary>
		/// Maximum page index items listed in dropdownlist
		/// </summary>
		public int MaximumPageIndexItems { get; set; }
		
		/// <summary>
		/// Numeric pager item format string
		/// </summary>
		public string PageNumberFormatString { get; set; }
		
		/// <summary>
		/// HTML tag name when control rendered
		/// </summary>
		public string ContainerTagName {
			get {
				return _containerTagName;
			}
			set {
				if (string.IsNullOrEmpty(value))
					throw new System.ArgumentException("ContainerTagName can not be null or empty", "ContainerTagName");
				_containerTagName = value;
			}
		}
		private string _containerTagName;
		
		/// <summary>
		/// All pageritem wrapper format string
		/// </summary>
		public string PagerItemWrapperFormatString { get; set; }
		
		/// <summary>
		/// Current pager item format string
		/// </summary>
		public string CurrentPageNumberFormatString { get; set; }
		
		/// <summary>
		/// NumericPager Item Wrapper Format String
		/// </summary>
		public string NumericPagerItemWrapperFormatString { get; set; }
		
		/// <summary>
		/// Current Pager Item Wrapper Format String
		/// </summary>
		public string CurrentPagerItemWrapperFormatString { get; set; }
		
		/// <summary>
		/// NavigationPager Item Wrapper Format String
		/// </summary>
		public string NavigationPagerItemWrapperFormatString { get; set; }
		
		/// <summary>
		/// MorePagerItem Wrapper Format String
		/// </summary>
		public string MorePagerItemWrapperFormatString { get; set; }
		
		/// <summary>
		/// PageIndexBox Wrapper Format String
		/// </summary>
		public string PageIndexBoxWrapperFormatString { get; set; }
		
		/// <summary>
		/// Whether or not show first and last numeric page number
		/// </summary>
		public bool AlwaysShowFirstLastPageNumber { get; set; }

		/// <summary>
		/// Numeric pager items count
		/// </summary>
		public int NumericPagerItemCount { get; set; }

		/// <summary>
		/// Whether or not show previous and next pager items
		/// </summary>
		public bool ShowPrevNext { get; set; }

		/// <summary>
		/// Previous page text
		/// </summary>
		public string PrevPageText { get; set; }

		/// <summary>
		/// Next page text
		/// </summary>
		public string NextPageText { get; set; }

		/// <summary>
		/// Whether or not show numeric pager items
		/// </summary>
		public bool ShowNumericPagerItems { get; set; }

		/// <summary>
		/// Whether or not show first and last pager items
		/// </summary>
		public bool ShowFirstLast { get; set; }

		/// <summary>
		/// First page text
		/// </summary>
		public string FirstPageText { get; set; }

		/// <summary>
		/// Last page text
		/// </summary>
		public string LastPageText { get; set; }

		/// <summary>
		/// Whether or not show more pager items
		/// </summary>
		public bool ShowMorePagerItems { get; set; }

		/// <summary>
		/// More page text
		/// </summary>
		public string MorePageText { get; set; }

		/// <summary>
		/// Client id of paging control container
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Horizontal alignment
		/// </summary>
		public string HorizontalAlign { get; set; }

		/// <summary>
		/// CSS class to apply
		/// </summary>
		public string CssClass { get; set; }

		/// <summary>
		/// Whether or not show disabled pager items
		/// </summary>
		public bool ShowDisabledPagerItems { get; set; }

		/// <summary>
		/// Seperating item HTML
		/// </summary>
		public string SeparatorHtml { get; set; }
		
		/// <summary>
		/// Maximum page index limitation, default is 0
		/// </summary>
		public int MaxPageIndex { get; set; }
		
		/// <returns>
		/// The ID of the DOM element to update.
		/// </returns>
		public string AjaxUpdateTargetId { get; set; }
		
		/// <returns>
		/// The name of the JavaScript function to call before the page is updated.
		/// </returns>
		public string AjaxOnBegin { get; set; }
		
		/// <returns>
		/// The JavaScript function to call when the response data has been instantiated.
		/// </returns>
		public string AjaxOnComplete { get; set; }

		/// <returns>
		/// The JavaScript function to call if the page update fails.
		/// </returns>
		public string AjaxOnFailure { get; set; }

		/// <returns>
		/// The JavaScript function to call after the page is successfully updated.
		/// </returns>
		public string AjaxOnSuccess { get; set; } 
	}
}