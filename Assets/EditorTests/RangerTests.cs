using System.Collections;
using NUnit.Framework;
using Safari.Model.Rangers;
using UnityEngine;
using UnityEngine.TestTools;

public class RangerTests
{

    [Test]
    public void RangerDeathTest()
    {
        Ranger ranger = new Ranger();
        bool diedFired = false;
        ranger.Died += (s, e) => diedFired = true;

        ranger.Kill();

        Assert.IsInstanceOf<Dead>(ranger.State);
        Assert.IsTrue(diedFired);
    }
    [Test]
    public void SetStateTest()
    {
        Ranger ranger1 = new Ranger();
        Ranger ranger2 = new Ranger();
        ranger2.Kill();

        ranger1.SetState(ranger2.State);

        Assert.IsInstanceOf<Dead>(ranger1.State);
    }

    
}
