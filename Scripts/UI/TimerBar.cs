using Godot;
using System;

namespace WFS
{
	public class TimerBar : ProgressBar
	{
		private IActionProvider Player;
		private float MaxTimerValue = 2.0f;

		public override void _Ready()
		{
			Player = this.GetParent() as IActionProvider;

		}

		public override void _Process(float delta)
		{
			Visible = Player.IsInputAllowed();
			
			this.SetValue(1.0f - Player.Timer / MaxTimerValue);
		}
	}
}
