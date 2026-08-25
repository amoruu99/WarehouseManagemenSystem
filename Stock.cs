using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;

namespace WarehouseManagemenSystem
{
    internal class Stock // class for the stock items with their details
    {
        [Required]
        [Key]
        public int MaterialId { get; set; }        
        [Required]
        public string? StockName { get; set; }
        public string? UnitOfMeasurement { get; set; }        
        public List<Batch> Batches { get; set; } = new List<Batch>();
        public List<StockCard> stockCard { get; set; } = new List<StockCard>();

        public void Inbound(decimal quantity, DateTime expiredDate)
        {            
            DateTime transactionDate = DateTime.Today;
            if (quantity > 0.0m)
            {
                AddBatch(MaterialId, quantity, expiredDate);
                stockCard.Add(new StockCard
                {
                    MaterialId = MaterialId,
                    StockName = StockName,
                    Quantity = quantity,
                    TransactionType = "IN",
                    TransactionDate = transactionDate
                });
            }
            else
            {
                throw new ArgumentException("Cannot be less than or equal 0");
            }
        }

        public void OutBound(decimal quantity)
        {
            DateTime transactionDate = DateTime.Today;
            if (quantity > 0)
            {
                RemoveBatch(MaterialId, quantity);
                stockCard.Add(new StockCard
                {
                    MaterialId = MaterialId,
                    StockName = StockName,
                    Quantity = quantity,
                    TransactionType = "OUT",
                    TransactionDate = transactionDate
                });
            }
            else
            {
                throw new ArgumentException("Cannot be less than or equal 0");
            }
        }
        public void AddBatch(int MaterialId,decimal quantity, DateTime ExpiredDate)
        {
            // adds a new batch to the Stock item
            var existingBatch = Batches.Find(b =>
            b.MaterialId == MaterialId && 
            b.ExpiredDate == ExpiredDate
            );

            if (existingBatch != null)
            {
                existingBatch.AddQuantity(quantity);                
            }
            else
            {
                Batches.Add(new Batch
                {
                    MaterialId = MaterialId,
                    Quantity = quantity,
                    ExpiredDate = ExpiredDate,
                });
                Batches.Sort((a, b) => a.ExpiredDate.CompareTo(b.ExpiredDate));
            }
            
        }

        public void RemoveBatch(int MaterialId, decimal quantity)
        {
            // removes a batch from the Stock item return empty list not null
            var existingBatch = Batches.Find(b =>
            b.MaterialId == MaterialId 
            );
            var remaining = quantity;            
            if (existingBatch != null)
            {                
                foreach (var batch in Batches)
                {                    
                    if (batch.MaterialId == MaterialId)
                    {
                        var amountRequested = Math.Min(remaining, batch.Quantity);
                        batch.RemoveQuantity(amountRequested);
                        remaining -= amountRequested;
                        if (remaining == 0)
                        {
                            break;
                        }
                    }
                }
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

        public void GenerateReport()
        {
            Console.WriteLine($"{MaterialId}\t{StockName}");
            foreach (Batch b in Batches)
            {                
                Console.WriteLine($"{b.ExpiredDate:dd/MM/yyyy} = {b.Quantity} {UnitOfMeasurement}");
            }
            Console.WriteLine($"Total: {QuantityCheck()} {UnitOfMeasurement}");

            foreach (StockCard s in stockCard)
            {
                Console.WriteLine($"{s.TransactionDate:dd/MM}\t{s.TransactionType}\t{s.Quantity}");                
            }
            Console.WriteLine();
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
            if (quantity > 0)
            {
                Quantity += quantity;
            }
            else
            {
                throw new ArgumentException("Add Quantity cannot be less than or equal 0");
            }
            // adds to the batch quantity
            
            // if quantity < 0 throw rejection exception
        }

        public void RemoveQuantity(decimal quantity)
        {
            // quantity must be > 0
            if (quantity > 0 && quantity <= Quantity)
            {
                Quantity -= quantity;
            }
            else
            {
                throw new ArgumentException("Insufficient Stock");
            }
            // requested quantity must be <= batch quantity 
            
            // if quantity reach 0, remove batch from the list
        }

        public bool IsEmpty()
        {
            // check if the batch is empty
            if (Quantity == 0)
            {                
                return true;                
            }
            return false;
        }
    }

    internal class StockCard // giving the details of the stock inbound and outbound transactions
    {
        [Required]
        public int TransactionId { get; set; }
        [Required]
        public int MaterialId { get; set; }
        public string? StockName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? TransactionType { get; set; }
        public decimal Quantity { get; set; }
        public string? PicName { get; set; }
        public string? References{ get; set; }

        
    }
}
