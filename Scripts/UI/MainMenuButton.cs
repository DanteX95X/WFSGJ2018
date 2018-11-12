using Godot;
using System;

public class MainMenuButton : Button
{	
	private void _on_MultiPlayerButton_button_up()
	{
		WFS.Global global = (WFS.Global)GetNode("/root/Global");
		global.singlePlayer = false;
		global.GotoScene("res://Scenes/AnimationMulti.tscn");
	}
	
	private void _on_SinglePlayerButton_button_up()
	{
		WFS.Global global = (WFS.Global)GetNode("/root/Global");
		global.singlePlayer = true;
		global.GotoScene("res://Scenes/AnimationSingle.tscn");
	}
	
	private void _on_CreditsButton_button_up()
	{
		WFS.Global global = (WFS.Global)GetNode("/root/Global");
		global.singlePlayer = true;
		global.GotoScene("res://Scenes/Credits.tscn");
	}
	
	private void _on_Button_button_up()
	{
		WFS.Global global = (WFS.Global)GetNode("/root/Global");
		global.singlePlayer = true;
		global.GotoScene("res://Scenes/MainMenu.tscn");
	}
	
	private void _on_QuitButton_button_up()
	{
		GetTree().Quit();
	}
}
