using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Safari.Model.Animals;

public class AnimalCollectionTests
{

    [Test]
    public void AddingAnimalToCollectionTest()
    {
        bool addedEventCalled = false;
        Animal animal = new Wolf(null, new Group(), null, Vector3.zero);
        AnimalCollection collection = new AnimalCollection(null);
        collection.Added += ((sender, args) => addedEventCalled = true);


        Assert.AreEqual(2, collection.Animals.Count);
        //with testSpawn in the constructor this is the expected behavior
        collection.AddAnimal(animal);
        Assert.AreEqual(3, collection.Animals.Count);

        Assert.IsTrue(addedEventCalled);
    }

    [Test]
    public void KillingAnimalTest()
    {
        bool diedEventCalled = false;
        bool removedEventCalled = false;

        AnimalCollection collection = new AnimalCollection(null);
        collection.Removed += ((sender, args) => removedEventCalled = true);

        Animal animal = collection.Animals[0];
        animal.Died += ((sender, args) => diedEventCalled = true);

        animal.Kill();

        Assert.IsTrue(diedEventCalled);
        Assert.IsTrue(removedEventCalled);
        Assert.AreEqual(1, collection.Animals.Count);

    }

    [Test]
    public void InitialSpawnTest()
    {
        AnimalCollection collection = new AnimalCollection(null);

        collection.TestSpawn();

        Assert.AreEqual(4, collection.Animals.Count);
    }


    
}
