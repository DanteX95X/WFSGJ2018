using Godot;
using System;
using System.Collections;

namespace WFS
{
	public class PlayerPawn : Area2D, IActionProvider
	{
		[Export]
		public bool defend;
		[Export]
		public bool second;

		private Action movementState;
		private AnimatedSprite animatedSprite;
		private int healthCurrent;
		private int healthMax;

		private float timer = 1;
		private float timePassed = 0;

		private Dictionary<Action, string> actionToAnimation;
		public override void _Ready()
		{
			healthMax = (int)Global.config.GetValue("Config", "InitHealth");
			healthCurrent = healthMax;

			actionToAnimation = new Dictionary<Action, string>();

			actionToAnimation[Action.NegativeSecond] = "Left";
			actionToAnimation[Action.NegativeFirst] = "Down";
			actionToAnimation[Action.Timeout] = "Idle";
			actionToAnimation[Action.PositiveFirst] = "Up";
			actionToAnimation[Action.PositiveSecond] = "Right";

			movementState = Action.Timeout;
			animatedSprite = (AnimatedSprite)GetNode("AnimatedSprite");

			animatedSprite.FlipH = false;
			animatedSprite.FlipV = false;

			animatedSprite.Play();
		}

		public override void _Process(float delta)
		{
			timePassed += delta;

			if (movementState == Action.Timeout)
			{
				string animationStr = InputCheck(second);

				if (animationStr != "Idle")
				{
					if (defend == true)
					{
						animationStr += "Defend";
					}
					else
					{
						animationStr += "Attack";
					}
				}

				SetAnimation(animationStr);
			}
		}

		private string InputCheck(bool second)
		{
			if (Input.IsActionJustReleased(second ? "ui_up_second" : "ui_up"))
			{
				movementState = Action.PositiveFirst;
			}
			else if (Input.IsActionJustReleased(second ? "ui_right_second" : "ui_right"))
			{
				movementState = Action.PositiveSecond;
			}
			else if (Input.IsActionJustReleased(second ? "ui_left_second" : "ui_left"))
			{
				movementState = Action.NegativeSecond;
			}
			else if (Input.IsActionJustReleased(second ? "ui_down_second" : "ui_down"))
			{
				movementState = Action.NegativeFirst;
			}
			
			return actionToAnimation[movementState];
		}

		public void SetAnimation(string animationStr)
		{
			animatedSprite.Animation = animationStr;
		}

		public void Reset()
		{
			movementState = Action.Timeout;
		}

		Action IActionProvider.ProvideAction()
		{
			Action temp = movementState;
			movementState = Action.Timeout;
			return temp;
		}

		public bool IsPerformingAction
		{
			get { return timePassed <= timer; }
		}

		public int Health
		{
			get { return healthCurrent; }
			set { healthCurrent = value; }
		}
	}
}