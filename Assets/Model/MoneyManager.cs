using System;
using UnityEngine;

namespace Safari.Model
{
    public class MoneyManager
    {
        int CurrentBalance;
        int TicketPrice;
       
        public MoneyManager()
        {
            CurrentBalance = 100;
            TicketPrice = 10;
        } 
     


        public void AddToBalance(int cost)
        {
            if (CurrentBalance + cost < 0) return;

            CurrentBalance += cost;
        } 
        public bool CanBuy(int itemPrice)
        {
            if (itemPrice > CurrentBalance) return false;
            return true;
        }

        public int ReadBalance()
        {
            return CurrentBalance;
        }
		public int ReadTicketPrice()
		{
			return TicketPrice;
		}

		public void RaiseTicketPrice()
        {
            TicketPrice++;
        }

		public void LowerTicketPrice()
		{
            if(TicketPrice < 2 ) return;
			TicketPrice--;
		}
	}
}
