using Godot;
using System;

public class StartButton : Button
{
    private void _on_StartButton_button_up()
	{
		WFS.Global global = (WFS.Global)GetNode("/root/Global");
		
	    //if (global.singlePlayer)
			global.GotoScene("res://Scenes/SinglePlayerGame.tscn");
		//else
		//	global.GotoScene("res://Scenes/Root.tscn");
	}
}
