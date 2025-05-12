using System.Collections;
using NUnit.Framework;
using Safari.Model.Rangers;
using UnityEngine;
using UnityEngine.TestTools;

public class RangerCollectionTests
{

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
}

    
