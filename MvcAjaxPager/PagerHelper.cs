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
using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

namespace MvcAjaxPager {

	public static class PagerHelper {

		private static MvcHtmlString AjaxPager(HtmlHelper html, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes) {
			return new PagerBuilder(html, pagerOptions, htmlAttributes).RenderPager();
		}

		public static MvcHtmlString AjaxPager(this HtmlHelper html, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName,
			string routeName, PagerOptions pagerOptions, object routeValues, object htmlAttributes)
		{
			if (pagerOptions == null)
				pagerOptions = new PagerOptions();

			var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
			var builder = new PagerBuilder(html, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
            return builder.RenderPager();
        }

		public static MvcHtmlString AjaxPager(this HtmlHelper html, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName,
			string routeName, PagerOptions pagerOptions, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			if (pagerOptions == null)
				pagerOptions = new PagerOptions();

			var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
			var builder = new PagerBuilder(html, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, htmlAttributes);
            return builder.RenderPager();
        }

		public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList) {
			return pagedList == null ? AjaxPager(html, (PagerOptions)null, null) 
									 : AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, null, null, null);
		}

		public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string routeName) {
			return pagedList == null ? AjaxPager(html, (PagerOptions)null, null) 
									 : AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, null, null, null);
		}

		public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, PagerOptions pagerOptions) {
			return pagedList == null ? AjaxPager(html, pagerOptions, null) 
									 : AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, null);
		}

		public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, PagerOptions pagerOptions, object htmlAttributes) {
			return pagedList == null ? AjaxPager(html, pagerOptions, new RouteValueDictionary(htmlAttributes)) 
									 : AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, htmlAttributes);
		}

		public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes) {
			return pagedList == null ? AjaxPager(html, pagerOptions, htmlAttributes) : AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, htmlAttributes);
		}

		public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string routeName, object routeValues, PagerOptions pagerOptions) {
			return pagedList == null ? AjaxPager(html, pagerOptions, null) 
									 : AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, null);
		}

		public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string routeName, object routeValues, PagerOptions pagerOptions, object htmlAttributes) {
			return pagedList == null ? AjaxPager(html, pagerOptions, new RouteValueDictionary(htmlAttributes)) : AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, htmlAttributes);
		}

		public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string routeName, RouteValueDictionary routeValues, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes) {
			return pagedList == null ? AjaxPager(html, pagerOptions, htmlAttributes) 
									 : AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, htmlAttributes);
		}

		public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string actionName, string controllerName, PagerOptions pagerOptions)
		{
			return pagedList == null ? AjaxPager(html, pagerOptions, null) 
									 : AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, actionName, controllerName, null, pagerOptions, null, null);
		}
	}
}