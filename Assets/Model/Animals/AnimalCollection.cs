#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals
{
    public class AnimalCollection
    {
        private List<Animal> animals;

        public event EventHandler<Animal>? Added;

        public event EventHandler<Animal>? Removed;

        public ReadOnlyCollection<Animal> Animals => animals.AsReadOnly();

        public AnimalCollection()
        {
            animals = new List<Animal>();
            animals.Add(new Wolf(null));
        }

        internal void AddAnimal(Animal animal)
        {
            animals.Add(animal);
            animal.Died += OnAnimalDied;
            Added?.Invoke(this, animal);
        }

        internal void RemoveAnimal(Animal animal)
        {
            animals.Remove(animal);
            Removed?.Invoke(this, animal);
        }

        private void OnAnimalDied(object sender, EventArgs e)
        {
            if (sender is Animal animal)
            {
                RemoveAnimal(animal);
            }
        }


    }
}
