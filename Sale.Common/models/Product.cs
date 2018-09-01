﻿namespace Sale.Common.models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]

        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishOn { get; set; }

        public override string ToString()
        {

            return this.Description;
        }
    }
}