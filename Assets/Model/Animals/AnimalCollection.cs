#nullable enable
using Safari.Model.Pathfinding;
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

        private PathfindingHelper pathfinding;
        public PathfindingHelper Pathfinding {  get { return pathfinding; } }

        public AnimalCollection(PathfindingHelper pathfinding)
        {
            animals = new List<Animal>();
            
            this.pathfinding = pathfinding;
        }
        public void TestSpawn()
        {
            Wolf wolf = new Wolf(pathfinding,null);
            wolf.Position = new UnityEngine.Vector3(0,0,100);
            Camel camel = new Camel(pathfinding, null);
            camel.Position = new UnityEngine.Vector3(10,0,100);
            AddAnimal(wolf);
            AddAnimal(camel);
        }

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
            animal.Died += OnAnimalDied;
            Added?.Invoke(this, animal);
        }

        public void RemoveAnimal(Animal animal)
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
