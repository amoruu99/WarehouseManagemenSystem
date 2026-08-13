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
        public double Quantity { get; set; }
    }

    internal class batch // adding batches details of the stock items with expiration date and receive date
    {
        public int BatchId { get; set; }
        public int MaterialId { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }

    internal class StockCard // giving the details of the stock inbound and outbound transactions
    {
        [Required]
        public int TransactionId { get; set; }
        [Required]
        public int MaterialId { get; set; }
        public string BatchId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public double Quantity { get; set; }
        public string? PicName { get; set; }
        public string? References{ get; set; }
    }
}
