using System.Collections.Generic;
using Godot;

namespace WFS
{
	public class PreNegateState : State
	{
		private NegateActionsState negateState = null;
		
		public PreNegateState(GameController controller, List<Action> actions)
		{
			negateState = new NegateActionsState(controller, actions);
		}
		
		public override State Update(float delta)
		{
			//TODO: lifes odpierdalaj tutaj
			if (Input.IsActionPressed("ui_accept"))
			{
				GD.Print("Negating attacks");
				return negateState;
			}
			else
			{
				return this;
			}
		}
	}
}