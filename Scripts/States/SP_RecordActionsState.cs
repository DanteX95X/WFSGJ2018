using Godot;

namespace WFS
{
	public class SP_RecordActionsState : RecordActionsState
	{
		public SP_RecordActionsState(BaseController controller, int attacksCount)
			: base(controller, attacksCount)
		{
			GD.Print("Singleplayer");
			controller.ResetFightLabel();
		}

		public override State Update(float delta)
		{
			State returnable = base.Update(delta);
			if (returnable is PreNegateState)
			{
				return new SP_PreNegateState(controller, actions);
			}
			return returnable;
		}
	}
}