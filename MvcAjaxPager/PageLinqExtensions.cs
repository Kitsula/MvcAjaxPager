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
using System.Linq;

namespace MvcAjaxPager {

	public static class PageLinqExtensions {

		public static PagedList<T> ToPagedList<T> (this IQueryable<T> allItems, int pageIndex, int pageSize) {
			if (pageIndex < 1)
				pageIndex = 1;
			var itemIndex = (pageIndex - 1) * pageSize;
			var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);
			var totalItemCount = allItems.Count();
			return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
		}
	}
}