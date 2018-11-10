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
		private float timer = 2.0f;
		private float timePassed;
		
		public RecordActionsState(GameController controller, int actionsCount)
		{
			this.controller = controller;
			this.attacker = controller.Attacker;
			this.actionsCount = actionsCount;
			timePassed = 0;

			this.timer = (float)controller.Config.GetValue("Config", "AttackTime");
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
					return new PreNegateState(controller, actions);
				}
			}
			return this;
		}
	}
}