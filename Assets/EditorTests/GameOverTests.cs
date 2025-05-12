using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Safari.Model;
using UnityEngine.UIElements;

public class GameOverTests
{
    [Test]
    public void WinTest()
    {
        GameOverLogic logic = new GameOverLogic(0,0,0,0);

        Assert.True(logic.CheckWinConditions(0));
    }
    [Test]
    public void NotWinTest()
    {
        GameOverLogic logic = new GameOverLogic(1, 1, 1,1);

        Assert.False(logic.CheckWinConditions(0));
    }
    [Test]
    public void NotLoseTest()
    {
        GameOverLogic logic = new GameOverLogic(1, 1, 1, 1);

        Assert.False(logic.CheckGameOverByExtinction());

        MoneyManager manager = new MoneyManager();

        manager.AddToBalance(10);

        Assert.False(logic.CheckGameOverByBankruptcy());

        Assert.False(logic.CheckWinConditions(0));

        manager.AddToBalance(manager.ReadBalance());

        Assert.False(logic.CheckWinConditions(0));
    }
    
}
