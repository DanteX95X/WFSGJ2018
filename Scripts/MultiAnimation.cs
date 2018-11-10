using Godot;
using System;

public class MultiAnimation : TextureRect
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    [Export]
    public int amount;
    private int current;

    [Export]
    public float timerMax;
    private float timer;

    public override void _Ready()
    {
        var kek = Texture;
        timer = 0;
        current = 0;
    }

    public override void _Process(float delta)
    {
        if (timer > timerMax)
        {

            timer = 0;
        }
    }

    //    public override void _Process(float delta)
    //    {
    //        // Called every frame. Delta is time since last frame.
    //        // Update game logic here.
    //        
    //    }
}
