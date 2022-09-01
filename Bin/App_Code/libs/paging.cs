using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libs
{
    public class paging
    {
		public static string PageList(string link, string searchUrl, int totalCnt, ref int page, int pageSize, int listSize)
		{
			if (page < 1)
			{
				page = 1;
			}
			if (pageSize < 0)
			{
				pageSize = 15;
			}
			if (listSize < 0)
			{
				listSize = 10;
			}
			int num;
			int num3;
			int num4;
			int num5;
			int num6;
			if (totalCnt > 0)
			{
				num = ((totalCnt % pageSize <= 0) ? (totalCnt / pageSize) : (totalCnt / pageSize + 1));
				if (page > num)
				{
					page = num;
				}
				int num2 = ((page % 10 <= 0) ? (page / listSize) : (page / listSize + 1));
				num3 = (num2 - 1) * listSize + 1;
				num4 = num2 * listSize;
				if (num4 > num)
				{
					num4 = num;
				}
				num5 = num3 - 1;
				num6 = num4 + 1;
			}
			else
			{
				num = 1;
				page = 1;
				num3 = 1;
				num4 = 1;
				num5 = 1;
				num6 = 1;
			}
			string str = "";
			str = ((num5 <= 0 || num5 >= num3) ? (str + "<li class=\"disabled\"><a href=\"#\">«</a></li>") : (str + $"<ul><li><a href=\"{link}?page={num5}{searchUrl}\">«</a></li>"));
			for (int i = num3; i <= num4; i++)
			{
				if (page == i)
				{
					object obj = str;
					str = string.Concat(obj, "<li class='active'><a href=\"#\">", i, "</a></li>");
				}
				else
				{
					str += string.Format("<li><a href=\"{0}?page={1}{2}\">{1}</a></li>", link, i, searchUrl);
				}
			}
			if (num6 > num4 && num6 <= num)
			{
				return str + $"<li><a href=\"{link}?page={num6}{searchUrl}\">»</a></li></ul>";
			}
			return str + "<li class=\"disabled\"><a href=\"#\">»</a></li>";
		}
	}
}
