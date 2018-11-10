using Godot;
using System.Collections.Generic;

namespace WFS
{
	public abstract class BaseController : Node
	{
		protected State state;
		protected List<IActionProvider> players = new List<IActionProvider>();
		protected int turn;
		
		public abstract IActionProvider Attacker { get; }
		public abstract IActionProvider Defender { get; }
		
		public Label fightLabel;
		public float fightLabelTimer;
		public float fightLabelTimeMax;
		
		public void ResetFightLabel()
		{
			fightLabel.Show();
			fightLabelTimer = 0;
		}
		
		public void ProcessFightLabel(float delta)
		{
			//Process Timers
			fightLabelTimer += delta;

			if (fightLabelTimer > fightLabelTimeMax)
			{
				fightLabel.Hide();
			}
		}
		
		public int Turn
		{
			get { return turn; }
			set { turn = value; }
		}

		public State CurrentState
		{
			get => state;
			set => state = value;
		}
		
		public override void _Process(float delta)
		{
			state = state?.Update(delta);
		}
		
	}
}