using Godot;
using System.Collections.Generic;

namespace WFS
{
//	internal class MockPlayer : IActionProvider
//	{
//		private int health = 3;
//		
//		public bool IsPerformingAction
//		{
//			get { return false; }
//		}
//		
//		public Action ProvideAction()
//		{
//			return Action.NegativeFirst;
//		}
//
//		public int Health
//		{
//			get { return health; }
//			set { health = value; }
//		}
//	}
	
	public class GameController : Node
	{
		private State state;
		private ConfigFile config;
		public ConfigFile Config => config;
		
		List<IActionProvider> players = new List<IActionProvider>();
		private int turn;
		private int attackerIndex;
		

		//TODO: make it sensible
		public IActionProvider Attacker => players[attackerIndex];
		public IActionProvider Defender => players[attackerIndex];
		
		public override void _Ready()
		{
			GD.Print("Controller started");

			config = new ConfigFile();
			config.Load("res://GameConfig.cfg");

			players.Add((IActionProvider) GetNode("Player1"));
			players.Add((IActionProvider) GetNode("Player2"));
			turn = 0;
			attackerIndex = 0;
			
			state = new PreRecordState(this, 4);
		}
		
		public override void _Process(float delta)
		{
			state = state?.Update(delta);
		}
	}
}