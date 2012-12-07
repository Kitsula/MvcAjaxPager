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
using System.Collections.Generic;

namespace MvcAjaxPager {

	public class PagedList<T> : List<T>, IPagedList {

		public PagedList(IList<T> items,int pageIndex,int pageSize) {
			PageSize = pageSize;
			TotalItemCount = items.Count;
			TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
			CurrentPageIndex = pageIndex;
			StartRecordIndex=(CurrentPageIndex - 1) * PageSize + 1;
			EndRecordIndex = TotalItemCount > pageIndex * pageSize ? pageIndex * pageSize : TotalItemCount;
			for (int i = StartRecordIndex-1; i < EndRecordIndex;i++ ) {
			    Add(items[i]);
			}
		}	
			
		public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount) {
			AddRange(items);
			TotalItemCount = totalItemCount;
			TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
			CurrentPageIndex = pageIndex;
			PageSize = pageSize;
			StartRecordIndex = (pageIndex - 1) * pageSize + 1;
			EndRecordIndex = TotalItemCount > pageIndex * pageSize ? pageIndex * pageSize : totalItemCount;
		}
		
		public int CurrentPageIndex { get; set; }
		public int PageSize { get; set; }
		public int TotalItemCount { get; set; }
		public int TotalPageCount{get; private set;}
		public int StartRecordIndex{get; private set;}
		public int EndRecordIndex{get; private set;}
	}
}