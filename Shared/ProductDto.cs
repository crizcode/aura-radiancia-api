﻿using NSwag.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string Name { get; set; }

        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock del producto es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser igual o mayor que cero.")]
        public int Stock { get; set; }

        public DateTime CreationDate { get; set; }


        [Required(ErrorMessage = "El ID de la categoría es obligatorio.")]
        public int CategoryId { get; set; }

        [SwaggerIgnore]
        public string? CategoryName { get; private set; }

 

        [Required(ErrorMessage = "El ID del proveedor es obligatorio.")]
        public int SupplierId { get; set; }

        [SwaggerIgnore]

        public string? SupplierName { get; private set; }

        // Resto de las propiedades sin cambios

        public ProductDto()
        {
        }

        public ProductDto(int id, string name, string descripcion, decimal precio, int stock, DateTime creationDate, int categoryId, int supplierId)
        {
            Id = id;
            Name = name;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            CreationDate = creationDate;
            CategoryId = categoryId;
            SupplierId = supplierId;
        }
    }
}