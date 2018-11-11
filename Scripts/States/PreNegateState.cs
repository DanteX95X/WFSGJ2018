using System.Collections.Generic;
using Godot;

namespace WFS
{
	public class PreNegateState : State
	{
		private NegateActionsState negateState = null;
		

		private float transitionTime;
		private float elapsedTime = 0.0f;

		private bool canChangeState;

		static int sign = 1;
		static bool isSignInit = false;
        private BaseController controller;

		public PreNegateState(BaseController controller, List<Action> actions)
		{	
			transitionTime = (float)Global.config.GetValue("Config", "TransitionTime");
			negateState = new NegateActionsState(controller, actions);
			canChangeState = false;
			GD.Print("Transition to negate attacks");
			controller.Attacker.Animate(Action.Timeout);
			controller.Defender.Animate(Action.Timeout);
            this.controller = controller;
			
			controller.ResetGetReadyLabel();
		}
		
		public override State Update(float delta)
		{
			if (!canChangeState)
			{
				elapsedTime += delta;
				if (elapsedTime >= transitionTime)
				{
					canChangeState = true;
				}
			}
			else
			{
				if (Input.IsActionJustReleased("ui_accept"))
				{
                    controller.ResetDefendLabel();
					GD.Print("Negating attacks");
					return negateState;
				}
				
				return this;
			}

			return this;
		}
	}
}