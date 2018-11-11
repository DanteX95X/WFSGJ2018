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
		public Label defendLabel;
        public float defendLabelTimer;
        public float defendLabelTimeMax;
		public Label getReadyLabel;
		public float getReadyLabelTimer;
		public float getReadyLabelTimeMax;
		public Label koLabel;
		public float koLabelTimer;
		public float koLabelTimeMax;

        public void ResetFightLabel()
        {
            fightLabel.Show();
	        defendLabel.Hide();
	        getReadyLabel.Hide();
	        koLabel.Hide();
            fightLabelTimer = 0;
        }

		public void ResetGetReadyLabel()
		{
			getReadyLabel.Show();
			fightLabel.Hide();
			defendLabel.Hide();
			koLabel.Hide();
			getReadyLabelTimer = 0;
		}

        public void ResetDefendLabel()
        {
            defendLabel.Show();
	        fightLabel.Hide();
	        getReadyLabel.Hide();
	        koLabel.Hide();
            defendLabelTimer = 0;
        }

		public void ResetKOLabel()
		{
			koLabel.Show();
			fightLabel.Hide();
			defendLabel.Hide();
			getReadyLabel.Hide();
			koLabelTimer = 0;
		}
		
		public void ProcessLabels(float delta)
		{
			//Process Timers
			fightLabelTimer += delta;

			if (fightLabelTimer > fightLabelTimeMax)
			{
				fightLabel.Hide();
			}

            defendLabelTimer += delta;

			if (defendLabelTimer > defendLabelTimeMax)
			{
				defendLabel.Hide();
			}

			getReadyLabelTimer += delta;

			if (getReadyLabelTimer > getReadyLabelTimeMax)
			{
				getReadyLabel.Hide();
			}

			koLabelTimer += delta;

			if (koLabelTimer > koLabelTimeMax)
			{
				koLabel.Hide();
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