namespace MXIC_PCCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("InputGenerate")]
    public partial class InputGenerate
    {   [Key]
    
        public int InputGenerateID { get; set; }
        [Display(Name ="資料表名稱")]
        public string TableName { get; set; }
        [Display(Name = "欄位名稱")]
        public string COLUMN_NAME { get; set; }
        [Display(Name = "畫面顯示名稱")]
        public string Remarks { get; set; }
        [Display(Name = "畫面自動生成")]
        public int Generate { get; set; }
        [Display(Name = "彈出畫面生成")]
        public int PopGenerate { get; set; }
        [Display(Name = "畫面生成type")]
        public string GenerateType { get; set; }
        [Display(Name = "新增畫面type")]
        public string AddPopGenerate { get; set; }
        [Display(Name = "修改畫面type")]
        public string EditPopGenerate { get; set; }
        [Display(Name = "Grid顯示")]
        public int? GridTitleGenerate { get; set; }
        [Display(Name = "Grid顯示方法")]
        public string GridFormatter { get; set; }
        [Display(Name = "排序")]
        public int? Sequence { get; set; }

        public bool Admin { get; set; }

    }
}
