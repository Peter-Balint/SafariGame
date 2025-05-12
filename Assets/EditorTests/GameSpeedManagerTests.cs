using System.Collections;
using NUnit.Framework;
using Safari.Model.GameSpeed;
using UnityEngine;
using UnityEngine.TestTools;

public class GameSpeedManagerTests
{
    [Test]
    public void GameSpeedFactorTest()
    {
        GameSpeedManager manager = new GameSpeedManager();

        Assert.AreEqual(30, manager.CurrentSpeedToNum());

        manager.CurrentSpeed = GameSpeed.Medium;

        Assert.AreEqual(3*60, manager.CurrentSpeedToNum());

        manager.CurrentSpeed = GameSpeed.Fast;

        Assert.AreEqual(30*60, manager.CurrentSpeedToNum());
    }
    [Test]
    public void MovementSpeedFactorTest()
    {
        GameSpeedManager manager = new GameSpeedManager();

        Assert.AreEqual(1, manager.CurrentSpeedToMovementSpeed());

        manager.CurrentSpeed = GameSpeed.Medium;

        Assert.AreEqual(2, manager.CurrentSpeedToMovementSpeed());

        manager.CurrentSpeed = GameSpeed.Fast;

        Assert.AreEqual(6, manager.CurrentSpeedToMovementSpeed());
    }
    [Test]
    public void AddTimeMinutesTest()
    {
        GameSpeedManager manager = new GameSpeedManager();
        manager.minutesToday = 0;

        manager.AddTime(60);

        Assert.AreEqual(manager.CurrentSpeedToNum(), manager.minutesToday);

    }
    [Test]
    public void AddTimedDayTest()
    {
        GameSpeedManager manager = new GameSpeedManager();
        manager.minutesToday = 24 * 60 + 1;

        bool dayPassedFired = false;
        manager.DayPassed += (s, e) => dayPassedFired = true;

        manager.AddTime(0);

        Assert.IsTrue(dayPassedFired);
    }
}
