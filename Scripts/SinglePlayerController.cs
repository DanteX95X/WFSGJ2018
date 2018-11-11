using Godot;

namespace WFS
{
	public class SinglePlayerController : BaseController
	{
		public override IActionProvider Attacker
		{
			get { return players[0]; }
		}

		public override IActionProvider Defender
		{
			get { return players[1]; }
		}
		
		public override void _Ready()
		{
			players.Add((IActionProvider) GetNode("Player1"));
			players.Add((IActionProvider) GetNode("Player2"));
			
			foreach (var player in players)
			{
				player.controller = this;
			}
			
			fightLabel = (Label)GetNode("FightLabel");
			fightLabelTimeMax = 1.0f;

			defendLabel = (Label)GetNode("DefendLabel");
			defendLabelTimeMax = 1.0f;

			getReadyLabel = (Label) GetNode("GetReadyLabel");
			getReadyLabelTimeMax = 1.0f;

			koLabel = (Label) GetNode("KOLabel");
			koLabelTimeMax = 1.0f;
			
			turn = 1;
			
			state = new SP_RecordActionsState(this, turn);
		}
	}
}