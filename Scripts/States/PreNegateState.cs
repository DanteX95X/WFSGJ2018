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

		public PreNegateState(GameController controller, List<Action> actions)
		{
			negateState = new NegateActionsState(controller, actions);
			material = (ShaderMaterial)GD.Load("res://NegativeShaderMaterial.material");
		}
		
		public override State Update(float delta)
		{
			GD.Print("Transition to negate attacks");
			elapsedTime += delta;
			float suwaczekValue = elapsedTime / transitionTime;
			material?.SetShaderParam("suwaczek", suwaczekValue);

			if (elapsedTime >= transitionTime)
				return negateState;
			else
				return this;
		}
	}
}