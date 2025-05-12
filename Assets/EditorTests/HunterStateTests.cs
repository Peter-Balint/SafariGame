using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Safari.Model.Hunters;
using Safari.Model.Map;
using Safari.Model;

public class HunterStateTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void TargetChangeTest()
    {
        var game = SafariGame.Instance;

        Hunter hunter = new Hunter();
        State state = new Hunting(hunter);
        hunter.SetState(state);

        Assert.IsInstanceOf<Hunting>(state);

        bool commandStartedFired = false;
        hunter.Movement.CommandStarted += (s, e) => commandStartedFired = true;

        hunter.Target = new GridPosition(1, 1);
        
        Assert.True(commandStartedFired);
    }

    [Test]
    public void ShiftToLeavingTest()
    {
        Hunter hunter = new Hunter();
        State state = new Hunting(hunter);
        hunter.SetState(state);

        hunter.Target = new GridPosition(1, 1);

        bool gridPosChangedFired = false;
        hunter.Movement.GridPositionChanged += (s, e) => gridPosChangedFired = true;

        hunter.Movement.ReportLocation(hunter.Target);

        state.Update();

        Assert.IsInstanceOf<Leaving>(hunter.State);

        Assert.True(gridPosChangedFired);
    }

    [Test]
    public void ShiftToDeadTest()
    {
        Map map = SafariGame.Instance.Map;

        Hunter hunter = new Hunter();
        State state = new Leaving(hunter);
        
        hunter.SetState(state);

        hunter.Movement.ReportLocation(map.ExitCoords);

        state.Update();

        Assert.IsInstanceOf<Dead>(hunter.State);
    }
}
