using Godot;
using System.Collections.Generic;

namespace WFS
{
	internal class MockPlayer : IActionProvider
	{
		public Action ProvideAction()
		{
			return Action.NegativeFirst;
		}
	}
	
	public class GameController : Node
	{
		private State state;
		private ConfigFile config;
		
		public override void _Ready()
		{
			GD.Print("Controller started");
			
			state = new RecordActionsState(new MockPlayer(), 4);

			config = new ConfigFile();
			config.Load("res://GameConfig.cfg");

			// config.GetValue("Config", "AttackTime");
		}
		
		public override void _Process(float delta)
		{
			state = state?.Update(delta);
		}
	}
}