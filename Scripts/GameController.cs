using Godot;
using System.Collections.Generic;

namespace WFS
{	
	public class GameController : Node
	{
		private State state;
		
		List<IActionProvider> players = new List<IActionProvider>();
		private int turn;
		
		public IActionProvider Attacker
		{
			get
			{
				int index = turn % 2;
				return players[1-index];
			}
		}

		public IActionProvider Defender
		{
			get
			{
				int index = turn % 2;
				return players[index];
			}
		}

		public int Turn
		{
			get { return turn; }
			set { turn = value; }
		}

		public State CurrentState
		{
			get { return state; }
		}
		
		public override void _Ready()
		{
			GD.Print("Controller started");

			players.Add((IActionProvider) GetNode("Player1"));
			players.Add((IActionProvider) GetNode("Player2"));

			foreach (var player in players)
			{
				player.controller = this;
			}
			
			turn = 1;
			
			state = new PreRecordState(this, turn);
		}
		
		public override void _Process(float delta)
		{
			state = state?.Update(delta);
		}
	}
}