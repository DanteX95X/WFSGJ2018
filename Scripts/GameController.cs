using Godot;
using System.Collections.Generic;

internal class MockPlayer : IActionProvider
{
    public Action ProvideAction()
    {
        return Action.Timeout;
    }
}

public class GameController : Node
{
    public override void _Ready()
    {
        GD.Print("Controller started");
        
        List<Action> actions = new List<Action>();
        MockPlayer player = new MockPlayer();
        for(int i = 0; i < 5; ++i)
            actions.Add(player.ProvideAction());

        foreach (Action action in actions)
        {
            GD.Print(action.ToString());
        }
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        
    }
}
