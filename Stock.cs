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

        public void AddBatch(Batch batch)
        {
           // adds a new batch to the Stock item

        }

        public void RemoveBatch(Batch batch)
        {
            // removes a batch from the Stock item return empty not null
        }

        public void QuantityCheck(Batch batch)
        {
            // sum of all batch Quantity
        }
    }

    internal class Batch // adding batches details of the stock items with expiration date and receive date
    {
        public int MaterialId { get; set; }
        
        public decimal Quantity { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        public void AddQuantity()
        {
            // quantity must be > 0
            // adds to the batch quantity
            // if quantity < 0 throw rejection exception
        }

        public void RemoveQuantity()
        {
            // quantity must be > 0
            // requested quantity must be <= batch quantity
            // if quantity reach 0, remove batch from the list
        }

        public void isEmpty()
        {
            // check if the batch is empty
        }
    }

    internal class StockCard // giving the details of the stock inbound and outbound transactions
    {
        [Required]
        public int TransactionId { get; set; }
        [Required]
        public int MaterialId { get; set; }
        public int BatchId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal Quantity { get; set; }
        public string? PicName { get; set; }
        public string? References{ get; set; }
    }
}
