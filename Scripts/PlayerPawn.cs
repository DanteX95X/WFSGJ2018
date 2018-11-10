using Godot;
using System;

public class PlayerPawn : Area2D
{
    [Export]
    public bool defend;
    [Export]
    public bool second;

    public override void _Ready()
    {
    }

    public override void _Process(float delta)
    {
        var animatedSprite = (AnimatedSprite)GetNode("AnimatedSprite");

        animatedSprite.FlipH = false;
        animatedSprite.FlipV = false;

        animatedSprite.Play();

        string animationStr = InputCheck(second);

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

    private string InputCheck(bool second){
        
        string animationStr = "";

        if (Input.IsActionPressed(second ? "ui_up_second" : "ui_up"))
        {
            animationStr = "Up";
        }
        else if (Input.IsActionPressed(second ? "ui_right_second" : "ui_right"))
        {
            animationStr = "Right";
        }
        else if (Input.IsActionPressed(second ? "ui_left_second" : "ui_left"))
        {
            animationStr = "Left";
        }
        else if (Input.IsActionPressed(second ? "ui_down_second" : "ui_down"))
        {
            animationStr = "Down";
        }
        else
        {
            animationStr = "Idle";
        }

        return animationStr;
    }
}
