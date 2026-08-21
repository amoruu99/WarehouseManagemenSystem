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
            UnitOfMeasurement = "Pcs",
        };
        var stock2 = new Stock()
        {
            MaterialId = 8004691,
            StockName = "INSTANT COFFEE 20 G",
            UnitOfMeasurement = "Pcs",
        };
        var stock3 = new Stock()
        {
            MaterialId = 8005012,
            StockName = "MINERAL WATER 600 ML",
            UnitOfMeasurement = "Pcs",
        };
        var stock4 = new Stock()
        {
            MaterialId = 8005005,
            StockName = "Chitato Lite",
            UnitOfMeasurement = "Pcs"
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
        /*stock4.Batches.Add(new Batch
        {
            MaterialId = 8005005,
            Quantity = 100,
            ExpiredDate = new DateTime(2028, 8, 28),
        });*/


        stock3.AddBatch(8005012, 250, new DateTime (2028, 10, 25));
        stock4.AddBatch(8005005, 355, new DateTime(2028, 8, 28));
        stock4.AddBatch(8005005, 240, new DateTime(2028, 9, 11));
        stock4.AddBatch(8005005, 225, new DateTime(2028, 5, 15));

        stock1.RemoveBatch(8002692, 400);

        stock1.GenerateReport();
        stock2.GenerateReport();
        stock3.GenerateReport();
        stock4.GenerateReport();

    }
}