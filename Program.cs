using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WarehouseManagemenSystem;

public class Program
{
    public static void Main()
    {
        var stock1 = new Stock()
        {
            MaterialId = 8002692,
            StockName = "RESKA TEH PREMIUM 12 OZ",
            UnitOfMeasurement = "PCS",
        };
        var stock2 = new Stock()
        {
            MaterialId = 8004691,
            StockName = "INSTANT COFFEE 20 G",
            UnitOfMeasurement = "PCS",
        };
        var stock3 = new Stock()
        {
            MaterialId = 8005012,
            StockName = "MINERAL WATER 600 ML",
            UnitOfMeasurement = "PCS",
        };
        stock1.Batches.Add(new Batch
        {
            MaterialId = 8002692,
            Quantity = 300,
            ExpiredDate = new DateTime(2028, 1, 16),
        });
        stock1.Batches.Add(new Batch
        {
            MaterialId = 8002692,
            Quantity = 3750,
            ExpiredDate = new DateTime(2028, 5, 5),
        });
        stock2.Batches.Add(new Batch
        {
            MaterialId = 8004691,
            Quantity = 500,
            ExpiredDate = new DateTime(2028, 2, 20),
        });
        stock2.Batches.Add(new Batch
        {
            MaterialId = 8004691,
            Quantity = 1500,
            ExpiredDate = new DateTime(2028, 7, 15),
        });
        stock3.Batches.Add(new Batch
        {
            MaterialId = 8005012,
            Quantity = 2000,
            ExpiredDate = new DateTime(2028, 3, 10),
        });
        stock3.Batches.Add(new Batch
        {
            MaterialId = 8005012,
            Quantity = 1500,
            ExpiredDate = new DateTime(2028, 9, 25),
        });      

        stock1.AddBatch(new Batch
        {
            MaterialId = 8002692,
            Quantity = 400,
            ExpiredDate = new DateTime(2028, 5, 5),
        });

        Console.WriteLine($"{stock1.MaterialId} {stock1.StockName}"); 
        foreach (var b in stock1.Batches) 
        { 
            Console.WriteLine($"{b.ExpiredDate.ToString("dd/MM/yyyy")} = {b.Quantity} {stock1.UnitOfMeasurement.ToString()}"); 
        }

    }
}