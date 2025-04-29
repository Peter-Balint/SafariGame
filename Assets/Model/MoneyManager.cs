using Safari.Model.Animals;
using System;
using System.Linq;
using UnityEngine;

namespace Safari.Model
{
    public class MoneyManager
    {
        int CurrentBalance;
        int TicketPrice;
        private double VisitDesire; // between 0-1, used as a chance of spawning
        private double diversityFactor;
        private double maxTicketPrice;
        private double parkSizeFactor;

		AnimalCollection AnimalCollection;
       
        public MoneyManager()
        {
            CurrentBalance = 200;
            TicketPrice = 10;
            VisitDesire = 0;
        }
		public MoneyManager(AnimalCollection animals)
		{
			CurrentBalance = 200;
			TicketPrice = 10;
			VisitDesire = 0;
            AnimalCollection = animals;
            CalculateVisitDesire();

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
	
	    public double ReadVisitDesire()
	    {
		    return VisitDesire;
	    }

        public double ReadDiversity()
        {
            return diversityFactor;
        }
		public double ReadParkSize()
		{
			return parkSizeFactor;
		}

		public double ReadMaxTicketPirce()
		{
			return maxTicketPrice;
		}


		public void RaiseTicketPrice()
        {
            TicketPrice++;
            CalculateVisitDesire();
        }

		public void LowerTicketPrice()
		{
            if(TicketPrice < 2 ) return;
			TicketPrice--;
			CalculateVisitDesire();

		}

		public void CalculateVisitDesire()
        {
            int AnimalCount = AnimalCollection.Animals.Count;

			int wolfCount = AnimalCollection.Animals.Count(a => a is Wolf);
			int lionCount = AnimalCollection.Animals.Count(a => a is Lion);
			int camelCount = AnimalCollection.Animals.Count(a => a is Camel);
			int sheepCount = AnimalCollection.Animals.Count(a => a is Sheep);
            Debug.Log("VisitDesireDATA---------------------------------");
            Debug.Log("Animal count: "+AnimalCollection.Animals.Count);



			double predatorCount = wolfCount + lionCount;
            double herbivoreCount = camelCount + sheepCount;

            double lionRatio = lionCount / predatorCount;
            double wolfRatio = wolfCount / predatorCount;
            double sheepRatio = sheepCount / herbivoreCount;
            double camelRatio = camelCount / herbivoreCount;

            
            //Checking diversity 
            diversityFactor = 0.1;

            if (lionCount > 0) diversityFactor += 0.1;
            if (wolfCount > 0) diversityFactor += 0.1;
            if (camelCount > 0) diversityFactor += 0.1;
            if (sheepCount > 0) diversityFactor += 0.1;
            if (lionCount > 0 && lionRatio > 0.4 && lionRatio < 0.6) diversityFactor += 0.3;
            if (camelCount > 0 && camelRatio > 0.4 && camelRatio < 0.6) diversityFactor += 0.3;

			diversityFactor = Math.Max(0.1, diversityFactor);
			diversityFactor = Math.Min(1, diversityFactor);
            Debug.Log("diversity: " + diversityFactor);

			//Checking Animal Count
			parkSizeFactor = 1;
            if (AnimalCount > 0 && AnimalCount < 10) parkSizeFactor = (double)AnimalCount / 10;
            if (AnimalCount > 9 && AnimalCount < 20) parkSizeFactor = (double)AnimalCount / 20;
			if (AnimalCount > 19 && AnimalCount < 50) parkSizeFactor = (double)AnimalCount / 50;
            if (AnimalCount > 49 && AnimalCount < 150) parkSizeFactor = (double)AnimalCount / 150;
            if (AnimalCount > 149) parkSizeFactor = 1;
            parkSizeFactor = Math.Sqrt(parkSizeFactor);
            Debug.Log("Parksizefactor: " + parkSizeFactor);
            parkSizeFactor = Math.Max(0.1, parkSizeFactor);


            //ticketprice
            maxTicketPrice = 10 + AnimalCount / 2.0;
            double maxTicketPriceFactor = 1.0;
            Debug.Log("maxticketprice: " + maxTicketPrice);
            Debug.Log("maxticketprice: " +maxTicketPriceFactor);
			if (TicketPrice>maxTicketPrice)
            {
                maxTicketPriceFactor = 0.2;
			

			}
			if (TicketPrice+10<maxTicketPrice)
            {
                maxTicketPriceFactor = 1.3;
            }


            VisitDesire = 1 * diversityFactor * parkSizeFactor * maxTicketPriceFactor;
            VisitDesire = Math.Min(1, VisitDesire);

			Debug.Log("VisitDesire:" + VisitDesire);
			Debug.Log("---------------------------------");




			/* NOTES about this function::
			 * 
			 *   This function may need to be modified multiple times for the right gameplay-balance
			 * ----------------------------
			 *   diversity factor is a number between 0 and 1
			 *   
			 *   the baseline of the diversity factor is ~0.1, if there is only one type of each animal
			 *   if the diversity is between 40-60% between the two species, visit desire is not reduced
			 *  ---------------------------------------
			 *  
			 *   ParkSizeFactor is a number between 0 and 1,
			 *   as the park grows, so does the number of animals the player should have to get a factor closer to 1
			 *   the baseline here is also at 0.1
			 *   
			 * 
			 * 
			 */


		}
	}
}
