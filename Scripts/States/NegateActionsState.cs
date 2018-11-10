using System.Collections.Generic;
using System.Linq;
using Godot;

namespace WFS
{
	public class NegateActionsState : State
	{
		private GameController controller;
		private List<Action> recordedActions;
		private IActionProvider defender;
		private int iterator;
		
		//temp hack
		private float timer;
		private float timePassed;
		
		public NegateActionsState(GameController controller, List<Action> recordedActions)
		{
			this.controller = controller;
			this.defender = controller.Defender;
			this.recordedActions = recordedActions;
			timePassed = 0;
			iterator = 0;

			this.timer = (float)controller.Config.GetValue("Config", "DefendTime");
		}
		
		public override State Update(float delta)
		{
			if (recordedActions.Count == 0)
			{
				return new PreRecordState(controller, ++controller.Turn);
			}
			
			if (defender.IsPerformingAction)
				return this;
			
			Action negativeAction = (Action)(-(int)defender.ProvideAction());
			if (negativeAction != Action.Timeout)
			{
				timePassed = 0;
				defender.Reset();
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
				if (iterator >= recordedActions.Count)
				{
					return new PreRecordState(controller, ++controller.Turn);
				}
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