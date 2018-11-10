using System.Collections.Generic;
using Godot;

namespace WFS
{
	public class PreNegateState : State
	{
		private NegateActionsState negateState = null;
		private ShaderMaterial material;

		private const float transitionTime = 1.5f;
		private float elapsedTime = 0.0f;

		private bool canChangeState;

		public PreNegateState(GameController controller, List<Action> actions)
		{
			negateState = new NegateActionsState(controller, actions);
			material = (ShaderMaterial)GD.Load("res://NegativeShaderMaterial.material");
			canChangeState = false;
			GD.Print("Transition to negate attacks");
		}
		
		public override State Update(float delta)
		{
			if (!canChangeState)
			{
				elapsedTime += delta;
				float suwaczekValue = elapsedTime / transitionTime;
				material?.SetShaderParam("suwaczek", suwaczekValue);

				if (elapsedTime >= transitionTime)
				{
					canChangeState = true;
				}
			}
			else
			{
				if (Input.IsActionPressed("ui_accept"))
				{
					GD.Print("Negating attacks");
					return negateState;
				}
				
				return this;
			}

			return this;
		}
	}
}