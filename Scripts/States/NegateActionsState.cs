using System.Collections.Generic;
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
		private float timer = 1;
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
			if (defender.IsPerformingAction)
				return this;
			
			Action negativeAction = (Action)(-(int)defender.ProvideAction());
			if (negativeAction != Action.Timeout && negativeAction == recordedActions[iterator])
			{
				GD.Print("OK");
			}
			else
			{
				GD.Print("Raptot zjebałeś!");
			}

			++iterator;
			if (iterator >= recordedActions.Count)
			{
				return null;
			}
			
			timePassed += delta;
			if (timePassed >= timer)
			{
				timePassed = 0;
				GD.Print("Raptot zjebałeś!");
			}

			return this;
		}
	}
}