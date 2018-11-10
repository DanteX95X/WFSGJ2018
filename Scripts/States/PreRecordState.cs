using Godot;

namespace WFS
{
	public class PreRecordState : State
	{
		private RecordActionsState state = null;
		
		public PreRecordState(GameController controller, int attacksCount)
		{
			state = new RecordActionsState(controller, attacksCount);
		}
		
		public override State Update(float delta)
		{
			GD.Print("Recording attacks:");
			//TODO: lifes odpierdalaj tutaj
			return state;
		}
	}
}