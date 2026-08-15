using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WarehouseManagemenSystem
{
    internal class Stock // base class for the stock items with their details
    {
        public int MaterialId { get; set; }        
        [Required]
        public string StockName { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal Quantity { get; set; }
        public List<Batch> Batches { get; set; } = new List<Batch>();

        public decimal AddBatch(Batch batch)
        {
            // adds a new batch to the Stock item
            // if batch already exists, add the quantity
            // if different expiration date, create a new batch
            return 0.0m;
        }

        public decimal RemoveBatch(Batch batch)
        {
            // removes a batch from the Stock item return empty list not null
            return 0.0m;
        }

        public decimal QuantityCheck()
        {
            // sum of all batch Quantity
            return 0.0m;
        }
    }

    internal class Batch // adding batches details of the stock items with expiration date and receive date
    {
        public int MaterialId { get; set; }
        
        public decimal Quantity { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        public decimal AddQuantity(decimal quantity)
        {
            // quantity must be > 0
            // adds to the batch quantity
            // if quantity < 0 throw rejection exception
            return 0.0m;
        }

        public decimal RemoveQuantity(decimal quantity)
        {
            // quantity must be > 0
            // requested quantity must be <= batch quantity
            // if quantity reach 0, remove batch from the list
            return 0.0m;
        }

        public bool IsEmpty()
        {
            // check if the batch is empty
            return false;
        }
    }

    internal class StockCard // giving the details of the stock inbound and outbound transactions
    {
        [Required]
        public int TransactionId { get; set; }
        [Required]
        public int MaterialId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal Quantity { get; set; }
        public string? PicName { get; set; }
        public string? References{ get; set; }
    }
}
