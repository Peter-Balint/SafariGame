using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Safari.Model.Animals;
using Safari.Model.Rangers;
using Safari.Model.GameSpeed;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;

public class InitialTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void InitialTestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    [Test]
    public void AddingAnimalToCollectionTest()
    {
        bool addedEventCalled = false;
        Animal animal = new Wolf(null, null);
        AnimalCollection collection = new AnimalCollection(null);
        collection.Added += ((sender, args) => addedEventCalled = true);


        Assert.AreEqual(2, collection.Animals.Count);
        //with testSpawn in the constructor this is the expected behavior, to be changed later
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
    public void AddingRangerToCollectionTest()
    {
        bool addedEventCalled = false;
        
        RangerCollection collection = new RangerCollection();
        collection.Added += ((sender, args) => addedEventCalled = true);


        Assert.AreEqual(0, collection.Rangers.Count);
        
        collection.Add(new Ranger());
        Assert.AreEqual(1, collection.Rangers.Count);

        Assert.IsTrue(addedEventCalled);
    }
    [Test]
    public void RemovingRangerFromCollectionTest()
    {
        bool removedEventCalled = false;
        RangerCollection collection = new RangerCollection();
        collection.Removed += ((sender, args) => removedEventCalled = true);

        Ranger ranger = new Ranger();
        collection.Add(ranger);
        Assert.AreEqual(1, collection.Rangers.Count);

        collection.Remove(ranger);
        Assert.IsTrue(removedEventCalled);
        Assert.AreEqual(0, collection.Rangers.Count);
    }
    [Test]
    public void GameSpeedToNumTest()
    {
        GameSpeedManager manager = new GameSpeedManager();
        Assert.AreEqual(1, manager.CurrentSpeedToNum());

        manager.CurrentSpeed = GameSpeed.Medium;
        Assert.AreEqual(3, manager.CurrentSpeedToNum());

        manager.CurrentSpeed = GameSpeed.Fast;
        Assert.AreEqual(10, manager.CurrentSpeedToNum());
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator InitialTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
