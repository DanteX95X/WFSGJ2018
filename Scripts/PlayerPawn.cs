using Godot;
using System;
using System.Collections;

namespace WFS
{
    public class PlayerPawn : Area2D, IActionProvider
    {
        [Export]
        public bool second;

        protected Action movementState;
        private AnimatedSprite animatedSprite;
        private AnimatedSprite prompt;
        protected AnimationPlayer animationPlayer;
        private int healthCurrent;
        private int healthMax;

        private float timer = 2;
        private float timePassed = 0;

        private bool isAnimating = false;

        public Particles2D Blood { get; set; }

        public BaseController controller { get; set; }

        private Dictionary<Action, string> actionToAnimation;

        public override void _Ready()
        {
            healthMax = (int)Global.config.GetValue("Config", "InitHealth");
            healthCurrent = healthMax;

            actionToAnimation = new Dictionary<Action, string>();

            actionToAnimation[Action.Death] = "Death";
            actionToAnimation[Action.NegativeSecond] = "Left";
            actionToAnimation[Action.NegativeFirst] = "Down";
            actionToAnimation[Action.Timeout] = "Idle";
            actionToAnimation[Action.PositiveFirst] = "Up";
            actionToAnimation[Action.PositiveSecond] = "Right";

            movementState = Action.Timeout;
            animatedSprite = (AnimatedSprite)GetNode("AnimatedSprite");

            WFS.Global global = (WFS.Global)GetNode("/root/Global");

            string path = "res://SpriteFrames/" +
             (second ? global.FirstCharacterSpriteFrameSelection :
             global.SecondCharacterSpriteFrameSelection) + ".tres";

            var spriteFrames = (SpriteFrames)GD.Load(path);

            animatedSprite.SetSpriteFrames(spriteFrames);

            animatedSprite.FlipH = second;
            animatedSprite.FlipV = false;
            animatedSprite.Play();

            prompt = (AnimatedSprite)GetNode("Prompt");
            prompt.FlipH = false;
            prompt.FlipV = false;
            animatedSprite.Play();

            Blood = (Particles2D)GetNode("Blood");

            animationPlayer = (AnimationPlayer)GetNode("animation");
        }

        public override void _Process(float delta)
        {
            timePassed += delta;

            if (!IsPerformingAction && controller.Defender == this && controller.CurrentState is NegateActionsState)
            {
                movementState = Action.Timeout;
            }

            if (movementState == Action.Timeout && IsInputAllowed())
            {
                InputCheck(second);
            }
            AnimateFrame();
        }

        public void Animate(Action action)
        {
            movementState = action;
            isAnimating = true;
        }

        public void AnimateFrame()
        {
            if (IsPerformingAction)
            {
                string animation = actionToAnimation[movementState];
                if (animation != "Idle" && animation != "Death")
                {
                    if (controller.Defender == this)
                    {
                        animation += "Defend";
                    }
                    else
                    {
                        animation += "Attack";
                    }
                }
                SetAnimation(animation);
            }
            else
            {
                isAnimating = false;
                SetAnimation("Idle");
                timePassed = 0;
            }
        }

        protected virtual void InputCheck(bool second)
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

            if (controller.CurrentState is RecordActionsState && controller.Attacker == this)
            {
                animationPlayer.Play("movement");
            }
        }

        public void SetAnimation(string animationStr)
        {
            animatedSprite.Animation = animationStr;
            if (animationStr == "Idle")
            {
                animationStr = "None";
            }
            prompt.Animation = animationStr;
        }

        public void Reset()
        {
            movementState = Action.Timeout;
            isAnimating = false;
            timePassed = 0;
        }

        Action IActionProvider.ProvideAction()
        {
            Action temp = movementState;
            return temp;
        }

        public bool IsPerformingAction
        {
            get { return isAnimating && timePassed <= timer; }
        }

        public int Health
        {
            get { return healthCurrent; }
            set { healthCurrent = value; }
        }

		public float Timer
		{
			get { return timePassed; }
		}

        public bool IsInputAllowed()
        {
            return (controller.Attacker == this && controller.CurrentState is RecordActionsState) || (controller.Defender == this && controller.CurrentState is NegateActionsState);
        }
    }
}