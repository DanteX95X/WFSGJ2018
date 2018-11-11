using System;
using System.Collections.Generic;
using Godot;

namespace WFS
{
	public class SinglePlayerPawn : PlayerPawn
	{
		List<Action> availableActions = new List<Action>() {Action.NegativeSecond, Action.NegativeFirst, Action.PositiveFirst, Action.PositiveSecond };

		private bool startCounting = false;
		private float timer = 0.3f;
		private float timePassed = 0;
		
		protected override void InputCheck(bool second)
		{
			if (!startCounting)
			{
				startCounting = true;
			}
			else if(timePassed >= timer)
			{
				timePassed = 0;
				startCounting = false;
				Random random = new Random();
				int index = random.Next(0, availableActions.Count);
				movementState = availableActions[index];
				
				if (IsInputAllowed())//controller.CurrentState is RecordActionsState && controller.Attacker == this)
				{
					animationPlayer.Play("movement");
				}
			}
		}

		public override void _Process(float delta)
		{
			base._Process(delta);

			if(startCounting)
				timePassed += delta;
		}
	}
}