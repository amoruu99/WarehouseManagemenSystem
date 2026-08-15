# Warehouse Management System

**Built with C# and .NET Framework**, this project is a streamlined Warehouse Management System (WMS) designed to manage stock items, track inventory levels, and generate operational reports. 
The system provides a user-friendly interface for executing CRUD operations (adding, updating, and deleting stock items), monitoring real-time stock details, and ensuring smooth supply chain 
continuity.

## Future Build Plan

1. **Implement Role-Based Access Control (RBAC):** Add user authentication and authorization to restrict access to certain features based on user roles.
   * **Roles:** Admin, Warehouse Officer, Viewer, Distribution Officer.
   * **Admin** can manage users and assign roles.
2. **Database Integration:** Implement a relational database to systematically store stock items and inventory levels based on Expiration Date, Quantity, and other relevant metadata.
3. **Operational Stock Cards:** Generate detailed Stock Cards per item, tracking specific logistics metadata such as Item Name, Quantity, Expiration Date, and Train Number.
4. **FEFO Automation:** Implement automated First-Expired-First-Out (FEFO) logic to prioritize the dispatching of items nearing their expiration dates, preventing physical stock loss.
5. **Data Reconciliation Engine:** Build a reconciliation feature to easily compare dispatched stock on trains against physical returned stock logged by On-Board Crews, ensuring strict data integrity.
6. **Reporting and Analytics:** Develop a reporting module to generate insights on stock levels, dispatch trends, and inventory turnover rates, aiding in strategic decision-making by flagging
	the most frequently dispatched and/or most slow-moving items and those nearing expiration.
7. **Mobile Accessibility:** Create a mobile-friendly version of the WMS for on-the-go access by warehouse staff and distribution officers.
8. **Generate Whatsapp Reports:** Implement a feature to generate and send stock reports via WhatsApp for quick communication with supervisors and Managers.
9. **Generate Sales Requests:** Implement a feature to generate Sales Requests for items that are dispatched from warehouse to Train and ensuring update on Stock levels in the system.

Batch lifecycle:
- Batch.Quantity represents the currently available quantity in that batch.
- When receiving stock, Batch.Quantity increases.
- When dispatching stock, Batch.Quantity decreases.
- When Batch.Quantity reaches 0, the batch is removed from active inventory.
- StockCard records remain as permanent transaction history even after the batch is removed.
- Stock.Quantity represents the total current quantity across active batches.

// Stock.Quantity = SUM(Batch.Quantity) for the same MaterialId \\



What happens if the material doesn't exist yet?
Adding "Add New Stock" that will create a new stock item with the provided details and new batch

What happens if the same batch already exists?
Adding it to the existing Batch.Quantity and updating the Stock.Quantity

What happens if quantity is 0 or negative?
if quantity is 0 or negative, it should be returned as an error (as if negative) and rejected as 0

Who provides the receiving date?
the receiving date is provided by the user when new batch of stock is added

Who provides the expiry date?
same as above

When is the StockCard created?
when there is a transaction (either inbounds or outbounds), it should be created with the transaction details.
who receives, where the transaction is from or to, the quality (defect or good) and the quantity.

Write down these three rules:

What uniquely identifies a batch?
Something like combination of materialId and BathcId (8000468-001) something like that

What exactly does "From/To" represent in a StockCard?
it can be writedown in references as a string

What values can "Quality" have?
it can be damaged or good. and maybe i can make another class as Defect to track the defect items.

DefectReport
    │
    ├── DefectReportId
    ├── Date
    ├── ReportedBy
    └── DefectItems
          │
          ├── Batch 8000468-001 → Quantity 5
          ├── Batch 8000469-001 → Quantity 4
          ├── Batch 8000470-001 → Quantity 6
          ├── Batch 8000471-001 → Quantity 5
          ├── Batch 8000475-001 → Quantity 4
          └── Batch 8000479-001 → Quantity 1


## Stock system model

### `Stock`

Represents the **global/current quantity of a material**.

```text
Stock
├── MaterialId
├── StockName
├── UnitOfMeasurement
└── Quantity
```

Rule:

> `Stock.Quantity` represents the total physical quantity of that material across its batches.

---

### `Batch`

Represents a **specific batch/lot of a material**.

```text
Batch
├── BatchId
├── MaterialId
├── Quantity
├── ReceiveDate
└── ExpiredDate
```

A batch is uniquely identified by:

```
MaterialId + BatchId
```

Example:

```
8000468 + 001
      ↓
8000468-001
```

`Batch.Quantity` represents the **physical quantity belonging to that batch**.

Example:

```
Batch: 8000468-001
Quantity: 100
```

---

### Relationship between Stock and Batch

For a material:

```
Stock.Quantity
=
SUM(all active Batch.Quantity for that MaterialId)
```

Example:

```
Stock 8000468 = 1,000

Batch 001 = 300
Batch 002 = 450
Batch 003 = 250

300 + 450 + 250 = 1,000
```

This is an important invariant.

---

## Receiving stock

When receiving stock:

```
Quantity <= 0
    ↓
Reject transaction
```

Valid quantity:

```
Quantity > 0
    ↓
Process receiving
```

If the material doesn't exist:

```
Create Stock
+
Create Batch
```

If the material already exists:

```
Find Stock
+
Find Batch
```

If the same batch exists:

```
Batch.Quantity += received quantity
Stock.Quantity += received quantity
```

If the batch doesn't exist:

```
Create new Batch
Stock.Quantity += received quantity
```

The user provides:

* Receive date
* Expiry date

---

## Batch lifecycle

When stock is dispatched:

```
Batch.Quantity decreases
Stock.Quantity decreases
```

When:

```
Batch.Quantity == 0
```

the batch is removed from **active inventory**.

But its transaction history is **not deleted**.

---

## `StockCard`

Represents the **transaction history/audit trail**.

```
StockCard
├── TransactionId
├── MaterialId
├── BatchId
├── TransactionDate
├── TransactionType
├── Quantity
├── PicName
└── References
```

A StockCard is created whenever there is a stock transaction:

```
IN
OUT
RETURN
...
```

It records things such as:

* Material
* Batch
* Quantity
* Transaction type
* Date
* PIC
* Reference/source/destination information
* Quality information

`References` can currently contain the human-readable source/destination information.

Example:

```
Supplier ABC → Warehouse A
```

---

# Defect system
**defects should be separate from Batch quantity**.

`Batch.Quantity` represents physical quantity.

A defect report identifies quantities that are defective.

### `DefectReport`

**whole defect report/incident**.

Conceptually:

```
DefectReport
├── DefectReportId
├── Date
├── ReportedBy
└── DefectItems
```

### `DefectItem`

**line within the defect report**.

Example:

```
DefectReport: DEF-001

Batch             Quantity
8000468-001       5
8000469-001       4
8000470-001       6
8000471-001       5
8000475-001       4
8000479-001       1
```

Total defective:

```
5 + 4 + 6 + 5 + 4 + 1 = 25
```

So:

```
One DefectReport
        │
        ├── DefectItem → Batch A, Qty 5
        ├── DefectItem → Batch B, Qty 4
        ├── DefectItem → Batch C, Qty 6
        └── ...
```

This matches your real warehouse workflow much better than creating one report per defective unit.

---

## One thing intentionally NOT decided yet

We haven't finalized how defective quantity affects **available quantity for dispatch/FEFO**.

For example:

```
Batch.Quantity = 100
DefectItem.Quantity = 5
```

We still need to decide whether the system considers:

```text
Available = 95
```

for dispatch purposes.

That's an important rule, but we haven't officially defined it yet, so I'm leaving it open rather than sneaking a decision into the design.

### Current mental model

```text
                    STOCK
                      │
             Global physical qty
                      │
          ┌───────────┴───────────┐
          │                       │
       BATCH 1                 BATCH 2
       qty 100                 qty 200
          │                       │
          │                       │
      DefectItems              DefectItems
          │
          ▼
     DefectReport
```

And separately:

```text
Stock changes
      │
      ▼
 StockCard
      │
      ▼
Permanent transaction history
```

Inventory batches are separated by expiry date. Multiple receipts of the same material with the same expiry 
date are combined into the same batch. Stock dispatch follows FEFO, prioritizing the earliest expiry date 
rather than the earliest receiving date.