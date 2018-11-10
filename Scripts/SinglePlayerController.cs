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
			
			turn = 1;
		}

		public override void _Process(float delta)
		{
			
		}
	}
}