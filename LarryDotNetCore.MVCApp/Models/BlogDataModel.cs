using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarryDotNetCore.MVCApp.Models
{
    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Key]
        [Column("Blog_Id")]
        public int Blog_Id { get; set; }

        public string? Blog_Title { get; set; }

        public string? Blog_Author { get; set; }

        public string? Blog_Content { get; set; }
    }

    public class BlogDataResponseModel
    {
        public PageSettingModel PageSetting { get; set; }
        public List<BlogDataModel> Blogs { get; set; }
    }

    public class BlogListResponseModel
    {
        public string Message { get; set; }

        public int IsSuccess { get; set; }

        public List<BlogDataModel> Blogs { get; set; }
    }

    public class BlogResponseModel
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public BlogDataModel Data { get; set; }
    }

    public class PageSettingModel
    {
        public PageSettingModel()
        {
        }
        public PageSettingModel(int pageNo, int pageSize, int pageCount, string pageUrl)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            PageCount = pageCount;
            PageUrl = pageUrl;
        }

        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; set; }

        public string PageUrl { get; set; }
    }
}
