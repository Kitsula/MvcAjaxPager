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

	public interface IPagedList {

		int CurrentPageIndex { get; set; }
		int PageSize { get; set; }
		int TotalItemCount { get; set; }
	}
}