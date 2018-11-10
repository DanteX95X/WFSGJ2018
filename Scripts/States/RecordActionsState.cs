using System.Collections.Generic;
using Godot;

namespace WFS
{
	public class RecordActionsState : State
	{
		private int actionsCount;
		private IActionProvider actionProvider;
		private List<Action> actions = new List<Action>();
		
		//temp hack
		private float timer = 2.0f;
		private float timePassed;
		
		public RecordActionsState(IActionProvider actionProvider, int actionsCount, ref List<Action> actions)
		{
			this.actions = actions;
			this.actionProvider = actionProvider;
			this.actionsCount = actionsCount;
			timePassed = 0;
		}
		
		public override State Update(float delta)
		{
			timePassed += delta;
			if (timePassed >= timer)
			{
				timePassed = 0;
				if (actionsCount <= 0)
				{
					return null;
				}

				Action currentAction = actionProvider.ProvideAction();
				GD.Print("New action " + currentAction.ToString());
				--actionsCount;
				actions.Add(currentAction);
			}
			return this;
		}
	}
}