﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LarryDotNetCore.MVC.Models
{
    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Key]
        [Column("Blog_Id")]
        public int Blog_Id { get; set; }
        public string Blog_Title { get; set; }
        public string Blog_Author { get; set; }
        public string Blog_Content { get; set; }
    }

    public class BlogDataResponseModel
    {
        public PageSettingModel PageSetting { get; set; }
        public List<BlogDataModel> Blogs { get; set; }
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

    public class MessageModel
    {
        public MessageModel()
        {
        }
        public MessageModel(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}