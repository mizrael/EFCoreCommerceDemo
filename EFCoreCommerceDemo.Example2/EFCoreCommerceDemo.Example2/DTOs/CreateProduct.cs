using System;
using System.ComponentModel.DataAnnotations;
using EFCoreCommerceDemo.Example2.Models;

namespace EFCoreCommerceDemo.Example2.DTOs
{
    public class CreateProduct
    {
        [Required, MaxLength(25)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Required, MaxLength(4)]
        public string Currency { get; set; }
    }
}
