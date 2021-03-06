using System.Collections.Generic;

namespace WFS
{
	public class SP_NegateActionsState : NegateActionsState
	{
		public SP_NegateActionsState(BaseController controller, List<Action> recordedActions)
			: base(controller, recordedActions)
		{
			
		}

		public override State Update(float delta)
		{
			State returnable = base.Update(delta);

			if (controller.Defender.Health <= 0)
			{
				return new DeathState(controller, controller.Defender);
			}
			
			if (returnable is PreRecordState)
			{
				return new SP_RecordActionsState(controller, controller.Turn);
			}

			return this;
		}
	}
}