using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Transactions;
using System.Numerics;

namespace WarehouseManagemenSystem
{
    internal class Stock // class for the stock items with their details
    {
        [Required]
        [Key]
        public int MaterialId { get; set; }        
        [Required]
        public string StockName { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal Quantity { get; set; }
        public static List<Batch> Batches { get; set; } = new List<Batch>();

        public void AddBatch(Batch batch)
        {
            // adds a new batch to the Stock item
            var existingBatch = Batches.Find(b=>
            b.MaterialId == batch.MaterialId && 
            b.ExpiredDate == batch.ExpiredDate
            );

            if (existingBatch != null)
            {
                existingBatch.AddQuantity(batch.Quantity);
            }
            else 
            {
                Batches.Add(batch);
            }
        }

        public void RemoveBatch(Batch batch)
        {
            // removes a batch from the Stock item return empty list not null            
            var existingBatch = Batches.Find(b =>
                b.MaterialId == batch.MaterialId &&
                b.ExpiredDate == batch.ExpiredDate
            );

            if (existingBatch != null)
            {
                Batches.Remove(existingBatch);
            }
            else
            {
                throw new ArgumentException("Batch is empty");
            }
        }

        public decimal QuantityCheck()
        {
            decimal total = 0;
            // sum of all batch Quantity
            foreach (var b in Batches)
            {
                total += b.Quantity;
            }
            return total;
        }
    }

    internal class Batch // add batches details of the stock items with expiration date and receive date
    {
        public int MaterialId { get; set; }        
        public decimal Quantity { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        public void AddQuantity(decimal quantity)
        {
            // quantity must be > 0
            foreach (var b in Stock.Batches)
            {               
                if (quantity > 0 && quantity == b.Quantity)
                {
                    Stock.Batches.Add(b);
                }
                else
                {
                    throw new ArgumentException("Quantity must be greater than 0");
                }
            }
            // adds to the batch quantity
            // if quantity < 0 throw rejection exception
        }

        public void RemoveQuantity(decimal quantity)
        {
            // quantity must be > 0            
            foreach (var b in Stock.Batches)
            {
                if (quantity <= 0 && quantity == b.Quantity)
                {
                    Stock.Batches.Remove(b);
                }
                for (int i = 0; i < b.Quantity; i++)
                {
                    if (b.Quantity > quantity)
                    {
                        quantity++;
                    }
                }
            }
            // requested quantity must be <= batch quantity
            // if quantity reach 0, remove batch from the list
        }

        public bool IsEmpty()
        {
            // check if the batch is empty
            foreach (var b in Stock.Batches)
            {
                if (b.Quantity == 0)
                {
                    throw new ArgumentException("Batch is empty, Ask for more PO in Purchasing");
                }
            }
            return true;
        }
    }

    internal class StockCard // giving the details of the stock inbound and outbound transactions
    {
        [Required]
        public int TransactionId { get; set; }
        [Required]
        public int MaterialId { get; set; }
        public string StockName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal Quantity { get; set; }
        public string? PicName { get; set; }
        public string? References{ get; set; }

        public string TransactionInfo()
        {
            return $"Date: {TransactionDate}\tType: {TransactionType}\tMaterial ID: {MaterialId}\tMaterial Description: {StockName}\tQuantity: {Quantity}\t";
        }

        
    }
}
