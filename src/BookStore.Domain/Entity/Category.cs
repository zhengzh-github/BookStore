using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.Entity
{
    [Table("Categories")]
    public class Category: FullAuditedEntity<int>
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        [MaxLength(50), Required]
        public string CategoryName { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        [Required]
        public Guid ParentID { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }
        /// <summary>
        /// 属性列表
        /// </summary>
        //public IList<AttributeInfo> AttributeInfoList { get; set; }
        ///// <summary>
        ///// 属性组列表
        ///// </summary>
        //public IList<AttributeGroup> AttributeGroupList { get; set; }
    }
}
