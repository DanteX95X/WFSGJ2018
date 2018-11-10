using Godot;
using System;

namespace WFS
{
	public class HealthBar : ProgressBar
	{
		private IActionProvider Player;
		private int MaxHealthValue = 1;

		public override void _Ready()
		{
			Player = this.GetParent() as IActionProvider;
			MaxHealthValue = (int)Global.config.GetValue("Config", "InitHealth");
		}

		public override void _Process(float delta)
		{
			// GD.Print("Player health: ", Player.Health / MaxHealthValue);
			this.SetValue(Player.Health / MaxHealthValue * 100);	
		}
	}
}