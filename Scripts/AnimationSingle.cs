using Godot;
using System;

public class AnimationSingle : AnimatedSprite
{
    [Export]
    public float timerMax;
    float timer;

    public override void _Ready()
    {
        timer = 0;
        this.Play();
    }

    public override void _Process(float delta)
    {
        timer += delta;
        if (timer >= timerMax)
        {
            WFS.Global global = (WFS.Global)GetNode("/root/Global");
            global.singlePlayer = true;
            global.GotoScene("res://Scenes/HeroSelector.tscn");
        }
    }
}
