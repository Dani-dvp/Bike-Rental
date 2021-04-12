using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental
{
    public class MenuHandler
    {

        public void DisplayMenu() // huvudmenyn, där användaren först kommer in.
        {
            Console.Clear();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. View..");
            Console.WriteLine("2. Add..");
            Console.WriteLine("3. Edit..");
            Console.WriteLine("4. Delete..");
            Console.WriteLine("5. Exit");
        }

        public void DeleteFromDatabase() // Val nummer 4 i huvudmenyn
        {
            Console.Clear();
            Console.WriteLine("Delete from");
            Console.WriteLine("What do you want to delete?");
            Console.WriteLine("1. Bikes");
            Console.WriteLine("2. Store");
            Console.WriteLine("3. Customer");
            Console.WriteLine("4. Orders");
        }

        public void ViewData() // val nummer 1 i huvudmenyn
        {
            Console.Clear();
            Console.WriteLine("View all");
            Console.WriteLine("1. Bikes");
            Console.WriteLine("2. Orders");
            Console.WriteLine("3. Stores");
            Console.WriteLine("4. Customers");
        }

        public void EditData() // val nummer 3 i huvudmenyn
        {
            Console.Clear();
            Console.WriteLine("Edit");
            Console.WriteLine("1. Orders");
        }

        public void AddData() // val nummer 2 i huvudmenyn
        {
            Console.Clear();
            Console.WriteLine("Add");
            Console.WriteLine("1. Bike");
            Console.WriteLine("2. Order and customer");
            Console.WriteLine("3. Store");
        }

        
    }
}
