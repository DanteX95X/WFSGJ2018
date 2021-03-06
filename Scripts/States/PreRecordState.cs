using Godot;

namespace WFS
{
	public class PreRecordState : State
	{
		private RecordActionsState state = null;

		private float transitionTime;
		private float elapsedTime = 0.0f;
		
		private bool canChangeState;

		static int sign = 1;
		static bool isSignInit = false;

		private ShaderMaterial material;

		private BaseController controller;

		private AudioStreamPlayer getReadySound;

		public PreRecordState(BaseController controller, int attacksCount)
		{
			this.controller = controller;
			
			canChangeState = false;
			
			transitionTime = (float)Global.config.GetValue("Config", "TransitionTime");
			if (!isSignInit)
			{
				sign = (int)Global.config.GetValue("Config", "InitTransitionSign");
				isSignInit = true;
			}
			else
				sign = -sign;

            controller.ResetGetReadyLabel();

			state = new RecordActionsState(controller, attacksCount);

			if (material == null)
				material = (ShaderMaterial)GD.Load("res://NegativeShaderMaterial.material");

			GD.Print("Transition to record attacks");
			
			controller.Attacker.Animate(Action.Timeout);
			controller.Defender.Animate(Action.Timeout);
			
			getReadySound = (AudioStreamPlayer)controller.GetNode("Sounds").GetNode("GetReadySound");
			getReadySound.Play();
		}
		
		public override State Update(float delta)
		{
			if (!canChangeState)
			{
				elapsedTime += delta;
				float suwaczekValue = 0.0f;
				if (sign == 1)
					suwaczekValue = sign * (elapsedTime / transitionTime);
				else if (sign == -1)
					suwaczekValue = 1 + sign * (elapsedTime / transitionTime);

				material?.SetShaderParam("suwaczek", suwaczekValue);

				if (elapsedTime >= transitionTime)
					canChangeState = true;
			}
			else
			{
				if (Input.IsActionJustReleased("ui_accept") || Input.IsActionJustReleased("ui_accept_second"))
				{
					controller.ResetFightLabel();
					AudioStreamPlayer stream = (AudioStreamPlayer)controller.GetNode("Sounds")?.GetNode("FightSound");
					stream?.Play();
					return state;
				}
			}
			
			return this;
				
		}
	}
}