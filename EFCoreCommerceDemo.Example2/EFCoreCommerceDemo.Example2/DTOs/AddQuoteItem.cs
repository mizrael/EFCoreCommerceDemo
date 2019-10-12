using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreCommerceDemo.Example2.DTOs
{
    public class AddQuoteItem
    {
        [Required]
        public Guid ProductId { get; set; }
        
        public int Quantity { get; set; }
    }
}