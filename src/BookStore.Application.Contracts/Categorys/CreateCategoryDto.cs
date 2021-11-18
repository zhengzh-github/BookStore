using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Categorys
{
    public class CreateCategoryDto
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "名称最长50"), Required(ErrorMessage = "名称必填")]
        public string CategoryName { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        [Required(ErrorMessage = "上级必填")]
        public Guid ParentID { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required(ErrorMessage = "排序必填")]
        public int Sort { get; set; }
    }
}
