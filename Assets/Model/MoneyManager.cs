using UnityEngine;

namespace Safari.Model
{
    public class MoneyManager
    {
        int CurrentBalance;
       
        public MoneyManager()
        {
            CurrentBalance = 100;
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
    }
}
