using CafeMenu.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal totalBill = 0.00M;
            decimal totalAmount = 0.00M;

            // Create Menu List 

            IDictionary<int, Menu> menuList = new Dictionary<int, Menu>();
            menuList.Add(1, new Menu() { MenuItem = "Cola", Price = 0.50M, ItemType="Drink", ItemCategory= "Cold" });
            menuList.Add(2, new Menu() { MenuItem = "Coffee", Price = 1.00M, ItemType = "Drink", ItemCategory = "Hot" });
            menuList.Add(3, new Menu() { MenuItem = "CheeseSandwich", Price = 2.00M, ItemType = "Food", ItemCategory = "Cold" });
            menuList.Add(4, new Menu() { MenuItem = "SteakSandwich", Price = 4.50M, ItemType = "Food", ItemCategory = "Hot" });

            // Read from Console for Selected List
            Console.WriteLine("Please select the Cafe X Menu using comma seperated: \n 1. Cola - Cold = £0.50 \n 2. Coffee - Hot =  £1.00 \n 3. Cheese Sandwich - Cold =  £2.00 \n 4. Steak Sandwich - Hot = £4.50 \n");
            string selectedMenuEnteredList = Console.ReadLine();

            
            int[] selectedMenuEnteredListArr = selectedMenuEnteredList.Split(',').Select(x => int.Parse(x)).ToArray();

            // if Selected Menu has items
            if (selectedMenuEnteredListArr.Count() > 0)
            {
                IList<Menu> selectedMenuList = new List<Menu>();
                foreach (int menuItem in selectedMenuEnteredListArr)
                {
                    selectedMenuList.Add(menuList[menuItem]);
                }

               
                // calculate charges
                decimal serviceCharges = GetCalculatedServiceCharges(selectedMenuList, out totalBill, out totalAmount);

                Console.WriteLine("\n Total Bill: £{0} \n Service Charge: £{1} \n Total: £{2}", totalBill, serviceCharges, totalAmount);
            }
            else
            {
                Console.WriteLine("Please re-enter Menu Items");
            }


        }

        private static decimal GetCalculatedServiceCharges(IList<Menu> selectedMenuList, out decimal totalBill, out decimal totalAmount)
        {
            totalBill = selectedMenuList.Sum(x => x.Price);
            decimal calculatedServiceCharges = 0.00M;

            // Item has food then add service charge 10%
            if (selectedMenuList.Any(k => k.ItemType.Contains("Food")))
            {
                // Item has hot food then add service charge 20% charge not greater than 20£
                if (selectedMenuList.Any(k => k.ItemCategory.Contains("Hot")))
                {
                    calculatedServiceCharges = (Math.Round((totalBill * 20 / 100), 2) > 20.00M) ? 20.00M: Math.Round((totalBill * 20 / 100), 2);
                } 
                else
                {
                    calculatedServiceCharges = Math.Round((totalBill * 10 / 100), 2);
                }
                
            }

            totalAmount = totalBill + calculatedServiceCharges;

            return calculatedServiceCharges;
        }
    }
}
