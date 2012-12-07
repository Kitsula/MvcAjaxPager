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
namespace MvcAjaxPager {

	internal class PagerItem {
		
		public PagerItem(string text, int pageIndex, bool disabled, PagerItemType type) {
			Text = text;
			PageIndex = pageIndex;
			Disabled = disabled;
			Type = type;
		}

		internal string Text { get; set; }
		internal int PageIndex { get; set; }
		internal bool Disabled { get; set; }
		internal PagerItemType Type { get; set; }
	}
}