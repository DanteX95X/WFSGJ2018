using System.Collections.Generic;
using Godot;

namespace WFS
{
	public class RecordActionsState : State
	{
		private int actionsCount;
		private GameController controller;
		private IActionProvider attacker;
		private List<Action> actions = new List<Action>();
		
		//temp hack
		private float timer = 0;
		private float timePassed;
		
		public RecordActionsState(GameController controller, int actionsCount)
		{
			this.controller = controller;
			this.attacker = controller.Attacker;
			this.actionsCount = actionsCount;
			timePassed = 0;

			this.timer = (float)Global.config.GetValue("Config", "AttackTime");
		}
		
		public override State Update(float delta)
		{
			timePassed += delta;
			if (timePassed >= timer)
			{
				timePassed = 0;

				Action currentAction = attacker.ProvideAction();
				GD.Print("Recorded action " + currentAction.ToString());
				--actionsCount;
				
				if(currentAction != Action.Timeout)
					actions.Add(currentAction);
				
				if (actionsCount <= 0)
				{
					return new PreNegateState(controller, actions);
				}
			}
			return this;
		}
	}
}