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