using System.Collections.Generic;
using Godot;

namespace WFS
{
	public class PreNegateState : State
	{
		private NegateActionsState negateState = null;
		

		private float transitionTime;
		private float elapsedTime = 0.0f;

		static int sign = 1;
		static bool isSignInit = false;
        private BaseController controller;

		public PreNegateState(BaseController controller, List<Action> actions)
		{
			transitionTime = (float)Global.config.GetValue("Config", "TransitionTime");
			negateState = new NegateActionsState(controller, actions);
			GD.Print("Transition to negate attacks");
			controller.Attacker.Animate(Action.Timeout);
			controller.Defender.Animate(Action.Timeout);
            this.controller = controller;
		}
		
		public override State Update(float delta)
		{
			elapsedTime += delta;
			if (elapsedTime >= transitionTime)
			{
				controller.ResetDefendLabel();
				GD.Print("Negating attacks");
				return negateState;
			}
			
			return this;
		}
	}
}