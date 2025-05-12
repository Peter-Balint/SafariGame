using System.Collections;
using NUnit.Framework;
using Safari.Model.Hunters;
using UnityEngine;
using UnityEngine.TestTools;

public class HunterCollectionTests
{
    [Test]
    public void AddingHunterToCollectionTest()
    {
        bool addedEventCalled = false;

        HunterCollection collection = new HunterCollection();
        collection.Added += ((sender, args) => addedEventCalled = true);


        Assert.AreEqual(0, collection.Hunters.Count);

        collection.Add(new Hunter());
        Assert.AreEqual(1, collection.Hunters.Count);

        Assert.IsTrue(addedEventCalled);
    }
    [Test]
    public void RemovingHunterFromCollectionTest()
    {
        bool removedEventCalled = false;
        HunterCollection collection = new HunterCollection();
        collection.Removed += ((sender, args) => removedEventCalled = true);

        Hunter Hunter = new Hunter();
        collection.Add(Hunter);
        Assert.AreEqual(1, collection.Hunters.Count);

        collection.Remove(Hunter);
        Assert.IsTrue(removedEventCalled);
        Assert.AreEqual(0, collection.Hunters.Count);
    }
}
