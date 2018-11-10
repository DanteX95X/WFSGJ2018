using System.Collections.Generic;
using Godot;

namespace WFS
{
	public class RecordActionsState : State
	{
		private int actionsCount;
		private IActionProvider attacker;
		private List<Action> actions = new List<Action>();
		
		//temp hack
		private float timer = 2.0f;
		private float timePassed;
		
		public RecordActionsState(IActionProvider attacker, int actionsCount)
		{
			this.attacker = attacker;
			this.actionsCount = actionsCount;
			timePassed = 0;
		}
		
		public override State Update(float delta)
		{
			timePassed += delta;
			if (timePassed >= timer)
			{
				timePassed = 0;

				Action currentAction = attacker.ProvideAction();
				GD.Print("New action " + currentAction.ToString());
				--actionsCount;
				actions.Add(currentAction);
				
				if (actionsCount <= 0)
				{
					return new NegateActionsState(actions, attacker);
				}
			}
			return this;
		}
	}
}