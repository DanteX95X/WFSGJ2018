using System.Collections.Generic;
using Godot;

namespace WFS
{
	public class SP_PreNegateState : State
	{
		private SP_NegateActionsState state;
		private BaseController controller;
		
		public SP_PreNegateState(BaseController controller, List<Action> actions)
		{
			this.controller = controller;
			state = new SP_NegateActionsState(controller, actions);
		}

		public override State Update(float delta)
		{
			if (Input.IsActionJustReleased("ui_accept") || Input.IsActionJustReleased("ui_accept"))
			{
				controller.ResetDefendLabel();
				return state;
			}

			return this;
		}
	}
}