using System.Collections.Generic;
using Godot;

namespace WFS
{
	public class SP_PreNegateState : State
	{
		private SP_NegateActionsState state;
		
		public SP_PreNegateState(BaseController controller, List<Action> actions)
		{
			state = new SP_NegateActionsState(controller, actions);
		}

		public override State Update(float delta)
		{
			if (Input.IsActionJustReleased("ui_accept"))
			{
				return state;
			}

			return this;
		}
	}
}