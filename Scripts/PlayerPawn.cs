using Godot;
using System;

public class PlayerPawn : Area2D
{
    [Export]
    public bool defend;

    public override void _Ready()
    {
    }

    public override void _Process(float delta)
    {
        var animatedSprite = (AnimatedSprite)GetNode("AnimatedSprite");

        animatedSprite.FlipH = false;
        animatedSprite.FlipV = false;

        animatedSprite.Play();

        string animationStr = "";

        if (Input.IsActionPressed("ui_up"))
        {
            animationStr = "Up";
        }
        else if (Input.IsActionPressed("ui_right"))
        {
            animationStr = "Right";
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            animationStr = "Left";
        }
        else if (Input.IsActionPressed("ui_down"))
        {
            animationStr = "Down";
        }
        else
        {
            animationStr = "Idle";
        }

        if (animationStr != "Idle")
        {
            if (defend == true)
            {
                animationStr += "Defend";
            }
            else
            {
                animationStr += "Attack";
            }
        }

        animatedSprite.Animation = animationStr;

    }
}
