using Godot;
using System;

public class PlayerPawn : Area2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    [Export]
    public bool defend;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        GD.Print("XD");
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
            //animatedSprite.Animation = "RightDefend";
        }
        else if (Input.IsActionPressed("ui_right"))
        {
            animationStr = "Right";
            //animatedSprite.Animation = "right";
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            animationStr = "Left";
            //animatedSprite.Animation = "right";
        }
        else if (Input.IsActionPressed("ui_down"))
        {
            animationStr = "Down";
            //animatedSprite.Animation = "right";
        }
        else
        {
            animationStr = "Idle";
        }
        // else
        // {
        //     animatedSprite.Animation = "Idle";
        // }

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
    //    public override void _Process(float delta)
    //    {
    //        // Called every frame. Delta is time since last frame.
    //        // Update game logic here.
    //        
    //    }

    // func _process(delta):
    //     var velocity = Vector2() # The player's movement vector.
    //     if Input.is_action_pressed("ui_right"):
    //         velocity.x += 1
    //     if Input.is_action_pressed("ui_left"):
    //         velocity.x -= 1
    //     if Input.is_action_pressed("ui_down"):
    //         velocity.y += 1
    //     if Input.is_action_pressed("ui_up"):
    //         velocity.y -= 1
    //     if velocity.length() > 0:
    //         velocity = velocity.normalized() * speed
    //         $AnimatedSprite.play()
    //     else:
    //         $AnimatedSprite.stop()
}
