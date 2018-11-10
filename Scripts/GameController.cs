using Godot;
using System.Collections.Generic;

namespace WFS
{	
	public class GameController : BaseController
	{	
		public override IActionProvider Attacker
		{
			get
			{
				int index = turn % 2;
				return players[1-index];
			}
		}

		public override IActionProvider Defender
		{
			get
			{
				int index = turn % 2;
				return players[index];
			}
		}
		
		public override void _Ready()
		{
			GD.Print("Controller started");

			players.Add((IActionProvider) GetNode("Player1"));
			players.Add((IActionProvider) GetNode("Player2"));

			fightLabel = (Label)GetNode("FightLabel");
            fightLabelTimeMax = 1.0f;

            defendLabel = (Label)GetNode("DefendLabel");
            defendLabelTimeMax = 1.0f;
			
			foreach (var player in players)
			{
				player.controller = this;
			}
			
			turn = 1;
			
			state = new PreRecordState(this, turn);
		}
		
		public override void _Process(float delta)
		{
			ProcessLabels(delta);

			state = state?.Update(delta);
		}
	}
}