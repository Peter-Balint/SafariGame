using System.Collections;
using NUnit.Framework;
using Safari.Model;
using UnityEngine;
using UnityEngine.TestTools;

public class SafariGameTests
{
    [Test]
    public void CreationTest()
    {
        var game = SafariGame.Instance;

        Assert.IsNotNull(game);
        Assert.IsNotNull(game.Construction);
        Assert.IsNotNull(game.GameSpeedManager);
        Assert.IsNotNull(game.Jeeps);
        Assert.IsNotNull(game.Animals);
        Assert.IsNotNull(game.Rangers);
        Assert.IsNotNull(game.Hunters);
        Assert.IsNotNull(game.Map);
        Assert.IsNotNull(game.MoneyManager);
        Assert.IsNotNull(game.AnimalCreationManager);
        Assert.IsNotNull(game.Visitors);
    }

    [Test]
    public void SingletonTest()
    {
        var game1 = SafariGame.Instance;
        var game2 = SafariGame.Instance;

        Assert.True(game1==game2);

        Assert.True(SafariGame.IsGameStarted);
    }
}
