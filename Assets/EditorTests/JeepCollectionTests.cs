using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Safari.Model.Jeeps;
using Safari.Model.Rangers;

public class JeepCollectionTests
{
    [Test]
    public void AddingJeepToCollectionTest()
    {
        bool addedEventCalled = false;

        JeepCollection collection = new JeepCollection(null,null);
        collection.Added += ((sender, args) => addedEventCalled = true);


        Assert.AreEqual(0, collection.Jeeps.Count);

        collection.Add(new Jeep(Vector3.zero, null, null));
        Assert.AreEqual(1, collection.Jeeps.Count);

        Assert.IsTrue(addedEventCalled);
    }
    [Test]
    public void RemovingJeepFromCollectionTest()
    {
        bool removedEventCalled = false;
        JeepCollection collection = new JeepCollection(null,null);
        collection.Removed += ((sender, args) => removedEventCalled = true);

        Jeep jeep = new Jeep(Vector3.zero,null,null);
        collection.Add(jeep);
        Assert.AreEqual(1, collection.Jeeps.Count);

        collection.Remove(jeep);
        Assert.IsTrue(removedEventCalled);
        Assert.AreEqual(0, collection.Jeeps.Count);
    }
}
