/*
 *  ASP .NET MVC AJAX Pager control 
 *  http://kitsula.com/MvcAjaxPager
 *  
 *  Copyright (c) 2012 Igor KitsulaCopyright (c) 2012 Igor Kitsula (http://kitsula.com)
 *  Copyright (c) 2009-2010 Webdiyer (http://en.webdiyer.com)
 *  Source code released under MIT license 
 *  http://kitsula.com/MvcAjaxPager/license
 *
 */
using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Globalization;
using System.Collections.Generic;

namespace MvcAjaxPager {

	internal class PagerBuilder {

		private readonly HtmlHelper _html;
		private readonly string _actionName;
		private readonly string _controllerName;
		private readonly int _totalPageCount = 1;
		private readonly int _pageIndex;
		private readonly PagerOptions _pagerOptions;
		private readonly RouteValueDictionary _routeValues;
		private readonly string _routeName;
		private readonly int _startPageIndex = 1;
		private readonly int _endPageIndex = 1;
		private IDictionary<string, object> _htmlAttributes;
		
		/// <summary>
		/// Used when PagedList is null
		/// </summary>
		internal PagerBuilder(HtmlHelper html, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes) {
			if (pagerOptions == null)
				pagerOptions = new PagerOptions();
			_html = html;
			_pagerOptions = pagerOptions;
			_htmlAttributes = htmlAttributes;
		}
		
		/// <summary>
		/// Pager builder
		/// </summary>
		internal PagerBuilder(HtmlHelper html, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, 
								RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
		    if (string.IsNullOrEmpty(actionName))
	            actionName = (string)html.ViewContext.RouteData.Values["action"];

			if (string.IsNullOrEmpty(controllerName))
	            controllerName = (string)html.ViewContext.RouteData.Values["controller"];
	
			if (pagerOptions == null)
		        pagerOptions = new PagerOptions();
		
		    _html = html;
		    _actionName = actionName;
		    _controllerName = controllerName;
		    
			if (pagerOptions.MaxPageIndex == 0 || pagerOptions.MaxPageIndex > totalPageCount)
		        _totalPageCount = totalPageCount;
		    else
		        _totalPageCount = pagerOptions.MaxPageIndex;
		    
			_pageIndex = pageIndex;
		    _pagerOptions = pagerOptions;
		    _routeName = routeName;
		    _routeValues = routeValues;
		    _htmlAttributes = htmlAttributes;
		
		    // start page index
		    _startPageIndex = pageIndex - (pagerOptions.NumericPagerItemCount / 2);
		    if (_startPageIndex + pagerOptions.NumericPagerItemCount > _totalPageCount)
		        _startPageIndex = _totalPageCount + 1 - pagerOptions.NumericPagerItemCount;
		    if (_startPageIndex < 1)
		        _startPageIndex = 1;
		
		    // end page index
		    _endPageIndex = _startPageIndex + _pagerOptions.NumericPagerItemCount - 1;
		    if (_endPageIndex > _totalPageCount)
		        _endPageIndex = _totalPageCount;
		}

		/// <summary>
		/// Render paging control
		/// </summary>
		internal MvcHtmlString RenderPager() {
		    
			//return null if total page count less than or equal to 1
		    if (_totalPageCount <= 1 && _pagerOptions.AutoHide)
		        return MvcHtmlString.Create(string.Empty);
		    
			//Display error message if pageIndex out of range
		    if ((_pageIndex > _totalPageCount && _totalPageCount > 0) || _pageIndex < 1) {
		        return MvcHtmlString.Create(string.Format("<div style=\"color:red;font-weight:bold\">{0}</div>", _pagerOptions.PageIndexOutOfRangeErrorMessage));
		    }
		
		    var pagerItems = new List<PagerItem>();
		    //First page
		    if (_pagerOptions.ShowFirstLast)
		        AddFirst(pagerItems);
		
		    // Prev page
		    if (_pagerOptions.ShowPrevNext)
		        AddPrevious(pagerItems);
		
		    if (_pagerOptions.ShowNumericPagerItems)
		    {
		        if (_pagerOptions.AlwaysShowFirstLastPageNumber && _startPageIndex > 1)
		            pagerItems.Add(new PagerItem("1", 1, false, PagerItemType.NumericPage));
		
		        // more page before numeric page buttons
		        if (_pagerOptions.ShowMorePagerItems)
		            AddMoreBefore(pagerItems);
		
		        // numeric page
		        AddPageNumbers(pagerItems);
		
		        // more page after numeric page buttons
		        if (_pagerOptions.ShowMorePagerItems)
		            AddMoreAfter(pagerItems);
		
		        if (_pagerOptions.AlwaysShowFirstLastPageNumber && _endPageIndex < _totalPageCount)
		            pagerItems.Add(new PagerItem(_totalPageCount.ToString(CultureInfo.InvariantCulture), _totalPageCount, false, PagerItemType.NumericPage));
		    }
		
		    // Next page
		    if (_pagerOptions.ShowPrevNext)
		        AddNext(pagerItems);
		
		    //Last page
		    if (_pagerOptions.ShowFirstLast)
		        AddLast(pagerItems);
		
		    var sb = new StringBuilder();
	        foreach (var item in pagerItems) {
				sb.Append(GenerateAjaxPagerElement(item));
		    }

			var tb = new TagBuilder(_pagerOptions.ContainerTagName);
		    if (!string.IsNullOrEmpty(_pagerOptions.Id))
		        tb.GenerateId(_pagerOptions.Id);
		    if (!string.IsNullOrEmpty(_pagerOptions.CssClass))
		        tb.AddCssClass(_pagerOptions.CssClass);

		    if (!string.IsNullOrEmpty(_pagerOptions.HorizontalAlign)) {
		        string strAlign = "text-align:" + _pagerOptions.HorizontalAlign.ToLower();
		        if (_htmlAttributes == null)
		            _htmlAttributes = new RouteValueDictionary { { "style", strAlign } };
		        else {
		            if (_htmlAttributes.Keys.Contains("style"))
		                _htmlAttributes["style"] += ";" + strAlign;
		        }
		    }
			tb.MergeAttribute("data-action", GenerateUrl(-1));
			tb.MergeAttribute("data-current", _pageIndex.ToString(CultureInfo.InvariantCulture));
			tb.MergeAttribute("data-updateTargetId", _pagerOptions.AjaxUpdateTargetId);
			if (!string.IsNullOrEmpty(_pagerOptions.AjaxOnBegin))
				tb.MergeAttribute("data-ajax-begin", _pagerOptions.AjaxOnBegin);
			if (!string.IsNullOrEmpty(_pagerOptions.AjaxOnComplete))
				tb.MergeAttribute("data-ajax-complete", _pagerOptions.AjaxOnComplete);
			if (!string.IsNullOrEmpty(_pagerOptions.AjaxOnSuccess))
				tb.MergeAttribute("data-ajax-success", _pagerOptions.AjaxOnSuccess);
			if (!string.IsNullOrEmpty(_pagerOptions.AjaxOnFailure))
				tb.MergeAttribute("data-ajax-failure", _pagerOptions.AjaxOnFailure);
			tb.MergeAttributes(_htmlAttributes, true);
	        sb.Length -= _pagerOptions.SeparatorHtml.Length;
		    tb.InnerHtml = sb.ToString();
		    return MvcHtmlString.Create(tb.ToString(TagRenderMode.Normal));
		}
		
		#region Private members
		private void AddPrevious(ICollection<PagerItem> results) {
		    var item = new PagerItem(_pagerOptions.PrevPageText, _pageIndex - 1, _pageIndex == 1, PagerItemType.PrevPage);
		    if (!item.Disabled || (item.Disabled && _pagerOptions.ShowDisabledPagerItems))
		        results.Add(item);
		}
		
		private void AddFirst(ICollection<PagerItem> results) {
		    var item = new PagerItem(_pagerOptions.FirstPageText, 1, _pageIndex == 1, PagerItemType.FirstPage);
		    if (!item.Disabled || (item.Disabled && _pagerOptions.ShowDisabledPagerItems))
		        results.Add(item);
		}
		
		private void AddMoreBefore(ICollection<PagerItem> results) {
		    if (_startPageIndex > 1 && _pagerOptions.ShowMorePagerItems) {
		        var index = _startPageIndex - 1;
		        if (index < 1) index = 1;
		        var item = new PagerItem(_pagerOptions.MorePageText, index, false, PagerItemType.MorePage);
		        results.Add(item);
		    }
		}
		
		private void AddPageNumbers(ICollection<PagerItem> results) {
			for (var pageIndex = _startPageIndex; pageIndex <= _endPageIndex; pageIndex++) {
				var text = pageIndex.ToString(CultureInfo.InvariantCulture);
				if (pageIndex == _pageIndex && !string.IsNullOrEmpty(_pagerOptions.CurrentPageNumberFormatString))
					text = string.Format(_pagerOptions.CurrentPageNumberFormatString, text);
				else 
					if (!string.IsNullOrEmpty(_pagerOptions.PageNumberFormatString))
						text = string.Format(_pagerOptions.PageNumberFormatString, text);
				var item = new PagerItem(text, pageIndex, false, PagerItemType.NumericPage);
				results.Add(item);
		    }
		}
		
		private void AddMoreAfter(ICollection<PagerItem> results) {
		    if (_endPageIndex < _totalPageCount) {
		        var index = _startPageIndex + _pagerOptions.NumericPagerItemCount;
		        if (index > _totalPageCount) { index = _totalPageCount; }
		        var item = new PagerItem(_pagerOptions.MorePageText, index, false, PagerItemType.MorePage);
		        results.Add(item);
		    }
		}
		
		private void AddNext(ICollection<PagerItem> results) {
		    var item = new PagerItem(_pagerOptions.NextPageText, _pageIndex + 1, _pageIndex >= _totalPageCount, PagerItemType.NextPage);
		    if (!item.Disabled || (item.Disabled && _pagerOptions.ShowDisabledPagerItems))
		        results.Add(item);
		}
		
		private void AddLast(ICollection<PagerItem> results) {
		    var item = new PagerItem(_pagerOptions.LastPageText, _totalPageCount, _pageIndex >= _totalPageCount, PagerItemType.LastPage);
		    if (!item.Disabled || (item.Disabled && _pagerOptions.ShowDisabledPagerItems))
		        results.Add(item);
		}
		
		/// <summary>
		/// Generate paging url
		/// </summary>
		/// <param name="pageIndex">Page index to generate navigate url</param>
		/// <returns>Navigated url for pager item</returns>
		private string GenerateUrl(int pageIndex) {

			var routeValues = GetCurrentRouteValues(_html.ViewContext);

			if (pageIndex == -1) {
				routeValues[_pagerOptions.PageIndexParameterName] = "#";
			} else {
				//return null if  page index larger than total page count or page index is current page index
				if (pageIndex > _totalPageCount || pageIndex == _pageIndex)
					return null;

				// set route value of page index parameter name in url
				routeValues[_pagerOptions.PageIndexParameterName] = pageIndex;
			}

		    // Return link
		    var urlHelper = new UrlHelper(_html.ViewContext.RequestContext);
		    return !string.IsNullOrEmpty(_routeName) ? urlHelper.RouteUrl(_routeName, routeValues) : urlHelper.RouteUrl(routeValues);
		}

		private RouteValueDictionary GetCurrentRouteValues(ViewContext viewContext) {
			var routeValues = _routeValues ?? new RouteValueDictionary();
			var rq = viewContext.HttpContext.Request.QueryString;
			if (rq != null && rq.Count > 0) {
				var invalidParams = new[] { "x-requested-with", "xmlhttprequest", _pagerOptions.PageIndexParameterName.ToLower() };
				foreach (string key in rq.Keys) {
					// add other url query string parameters (exclude PageIndexParameterName parameter value and X-Requested-With=XMLHttpRequest ajax parameter) to route value collection
					if (!string.IsNullOrEmpty(key) && Array.IndexOf(invalidParams, key.ToLower()) < 0) {
						routeValues[key] = rq[key];
					}
				}
			}
			// action
			routeValues["action"] = _actionName;
			// controller
			routeValues["controller"] = _controllerName;
			return routeValues;
		}

		private string GenerateAnchor(PagerItem item) {
			string url = GenerateUrl(item.PageIndex);
			var tag = new TagBuilder("a") { InnerHtml = item.Text };
			tag.MergeAttribute("href", url);
			tag.MergeAttribute("data-page", item.PageIndex.ToString(CultureInfo.InvariantCulture));
			return string.IsNullOrEmpty(url) ? item.Text : tag.ToString(TagRenderMode.Normal);
		}
		
		private MvcHtmlString GenerateAjaxPagerElement(PagerItem item) {
			return CreateWrappedPagerElement(item, item.Disabled ? String.Format("<a disabled=\"disabled\">{0}</a>", item.Text) : GenerateAnchor(item));
		}

		private MvcHtmlString CreateWrappedPagerElement(PagerItem item, string el) {
		    string navStr = el;
		    switch (item.Type) {
		        case PagerItemType.FirstPage:
		        case PagerItemType.LastPage:
		        case PagerItemType.NextPage:
		        case PagerItemType.PrevPage:
		            if ((!string.IsNullOrEmpty(_pagerOptions.NavigationPagerItemWrapperFormatString) ||
		                 !string.IsNullOrEmpty(_pagerOptions.PagerItemWrapperFormatString)))
		                navStr = string.Format(_pagerOptions.NavigationPagerItemWrapperFormatString ?? _pagerOptions.PagerItemWrapperFormatString, el);
		            break;
		        case PagerItemType.MorePage:
		            if ((!string.IsNullOrEmpty(_pagerOptions.MorePagerItemWrapperFormatString) ||
		                 !string.IsNullOrEmpty(_pagerOptions.PagerItemWrapperFormatString)))
		                navStr = string.Format(_pagerOptions.MorePagerItemWrapperFormatString ?? _pagerOptions.PagerItemWrapperFormatString, el);
		            break;
		        case PagerItemType.NumericPage:
		            if (item.PageIndex == _pageIndex && (!string.IsNullOrEmpty(_pagerOptions.CurrentPagerItemWrapperFormatString) 
						|| !string.IsNullOrEmpty(_pagerOptions.PagerItemWrapperFormatString))) //current page
		                navStr = string.Format(_pagerOptions.CurrentPagerItemWrapperFormatString ?? _pagerOptions.PagerItemWrapperFormatString, el);
		            else if (!string.IsNullOrEmpty(_pagerOptions.NumericPagerItemWrapperFormatString) || !string.IsNullOrEmpty(_pagerOptions.PagerItemWrapperFormatString))
		                navStr = string.Format(_pagerOptions.NumericPagerItemWrapperFormatString ??_pagerOptions.PagerItemWrapperFormatString, el);
		            break;
		    }
		    return MvcHtmlString.Create(navStr + _pagerOptions.SeparatorHtml);
		}
		#endregion
	}
}