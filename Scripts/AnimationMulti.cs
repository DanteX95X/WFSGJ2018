using Godot;
using System;

public class AnimationMulti : AnimatedSprite
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
            global.singlePlayer = false;
            global.GotoScene("res://Scenes/HeroSelector.tscn");
        }
    }
}
