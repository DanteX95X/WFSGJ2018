using Godot;

namespace WFS
{
	public class PreRecordState : State
	{
		private RecordActionsState state = null;
		private ShaderMaterial material;

		private const float transitionTime = 1.5f;
		private float elapsedTime = transitionTime;

		public PreRecordState(GameController controller, int attacksCount)
		{
			state = new RecordActionsState(controller, attacksCount);
			material = (ShaderMaterial)GD.Load("res://NegativeShaderMaterial.material");
		}
		
		public override State Update(float delta)
		{
			GD.Print("Transition to record attacks");
			elapsedTime -= delta;
			float suwaczekValue = elapsedTime / transitionTime;
			material?.SetShaderParam("suwaczek", suwaczekValue);

			if (elapsedTime <= 0.0f)
				return state;
			else
				return this;
		}
	}
}