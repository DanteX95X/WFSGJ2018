using System.Collections.Generic;
using System.Linq;
using Godot;

namespace WFS
{
	public class NegateActionsState : State
	{
		protected BaseController controller;
		private List<Action> recordedActions;
		private IActionProvider defender;
		private IActionProvider attacker;
		private int iterator;
		
		private float timer;
		private float timePassed;
		
		public NegateActionsState(BaseController controller, List<Action> recordedActions)
		{
			this.controller = controller;
			this.defender = controller.Defender;
			this.attacker = this.controller.Attacker;
			this.recordedActions = recordedActions;
			timePassed = 0;
			iterator = 0;

			this.timer = (float)Global.config.GetValue("Config", "DefendTime");
		}
		
		public override State Update(float delta)
		{
			if (recordedActions.Count == 0)
			{
				return new PreRecordState(controller, ++controller.Turn);
			}
			
			if (defender.IsPerformingAction)
				return this;
			
			if (iterator >= recordedActions.Count)
			{
				return new PreRecordState(controller, ++controller.Turn);
			}
			
			Action negativeAction = (Action)(-(int)defender.ProvideAction());
			if (negativeAction != Action.Timeout)
			{
				defender.Animate(defender.ProvideAction());
				attacker.Animate(recordedActions[iterator]);
				
				timePassed = 0;
				if (negativeAction == recordedActions[iterator])
				{
					GD.Print("OK");
				}
				else
				{
					GD.Print("Wrong action! " + negativeAction.ToString());
					--defender.Health;
					GD.Print("HP " + defender.Health);
					if (defender.Health <= 0)
					{
						GD.Print("Game over");
						return null;
					}
				}

				++iterator;
				return this;

			}
			else
			{
				timePassed += delta;
				if (timePassed >= timer)
				{
					timePassed = 0;
					GD.Print("Timeout!");
					--defender.Health;
					GD.Print("HP " + defender.Health);
					if (defender.Health <= 0)
					{
						GD.Print("Game over");
						return null;
					}
					
					++iterator;
					if (iterator >= recordedActions.Count)
					{
						return new PreRecordState(controller, ++controller.Turn);
					}
				}
			}

			return this;
		}
	}
}