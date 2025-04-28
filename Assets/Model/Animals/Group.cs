using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals
{
    public class Group
    {
        public Animal Leader { get; private set; }

        public List<Animal> Animals { get; private set; }

        public Group(Animal leader, List<Animal> animals)
        {
            Leader = leader;
            Animals = animals;
            foreach (var animal in animals)
            {
                animal.Died += OnAnimalDied;
            }
        }

        public Group(): this(null, new List<Animal>())
        {
            
        }

        public void AddAnimal(Animal animal)
        {
            Leader ??= animal;
            Animals.Add(animal);
            animal.Died += OnAnimalDied;
        }

        private void OnAnimalDied(object sender, EventArgs e)
        {
            if (sender == Leader)
            {
                Leader = Animals.FirstOrDefault(a => a != Leader);
                Animals.Remove((Animal)sender);
            }
        }
    }
}
