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
		
	}
}