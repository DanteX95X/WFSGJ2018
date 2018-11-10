using Godot;
using System.Collections.Generic;

namespace WFS
{	
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

		public int Turn
		{
			get { return turn; }
			set { turn = value; }
		}
		
		public override void _Ready()
		{
			GD.Print("Controller started");

			config = new ConfigFile();
			config.Load("res://GameConfig.cfg");

			players.Add((IActionProvider) GetNode("Player1"));
			players.Add((IActionProvider) GetNode("Player2"));
			turn = 1;
			attackerIndex = 0;
			
			state = new PreRecordState(this, turn);
		}
		
		public override void _Process(float delta)
		{
			state = state?.Update(delta);
		}
	}
}