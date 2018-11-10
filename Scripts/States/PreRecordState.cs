using Godot;

namespace WFS
{
	public class PreRecordState : State
	{
		private RecordActionsState state = null;

		private float transitionTime;
		private float elapsedTime = 0.0f;

		static int sign = 1;
		static bool isSignInit = false;

		private ShaderMaterial material;

		public PreRecordState(GameController controller, int attacksCount)
		{
			transitionTime = (float)Global.config.GetValue("Config", "TransitionTime");
			if (!isSignInit)
			{
				sign = (int)Global.config.GetValue("Config", "InitTransitionSign");
				isSignInit = true;
			}
			else
				sign = -sign;

			state = new RecordActionsState(controller, attacksCount);

			if (material == null)
				material = (ShaderMaterial)GD.Load("res://NegativeShaderMaterial.material");

			GD.Print("Transition to record attacks");
			
			controller.Attacker.Animate(Action.Timeout);
			controller.Defender.Animate(Action.Timeout);
		}
		
		public override State Update(float delta)
		{
			elapsedTime += delta;
			float suwaczekValue = 0.0f;
			if (sign == 1)
				suwaczekValue = sign * (elapsedTime / transitionTime);
			else if (sign == -1)
				suwaczekValue = 1 + sign * (elapsedTime / transitionTime);

			material?.SetShaderParam("suwaczek", suwaczekValue);

			if (elapsedTime >= transitionTime)
				return state;
			else
				return this;
		}
	}
}