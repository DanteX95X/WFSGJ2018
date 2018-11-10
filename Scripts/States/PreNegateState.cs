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
			GD.Print("Negating attacks");
			//TODO: lifes odpierdalaj tutaj
			return negateState;
		}
	}
}