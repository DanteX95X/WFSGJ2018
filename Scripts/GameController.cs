using Godot;
using System.Collections.Generic;

namespace WFS
{
	internal class MockPlayer : IActionProvider
	{
		public bool IsPerformingAction
		{
			get { return false; }
		}
		
		public Action ProvideAction()
		{
			return Action.NegativeFirst;
		}
	}
	
	public class GameController : Node
	{
		private State state;
		private ConfigFile config;
		public ConfigFile Config => config;

		//TODO: make it sensible
		public IActionProvider Attacker => new MockPlayer();
		public IActionProvider Defender => new MockPlayer();
		
		public override void _Ready()
		{
			GD.Print("Controller started");

			config = new ConfigFile();
			config.Load("res://GameConfig.cfg");
			// config.GetValue("Config", "AttackTime");

			state = new RecordActionsState(this, 4);
		}
		
		public override void _Process(float delta)
		{
			state = state?.Update(delta);
		}
	}
}