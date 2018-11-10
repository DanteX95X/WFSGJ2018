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

        private Dictionary<Action,string> actionToAnimation;
        public override void _Ready()
        {
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

        private string InputCheck(bool second)
        {
            if (Input.IsActionPressed(second ? "ui_up_second" : "ui_up"))
            {
                movementState = Action.PositiveFirst;
            }
            else if (Input.IsActionPressed(second ? "ui_right_second" : "ui_right"))
            {
                movementState = Action.PositiveSecond;
            }
            else if (Input.IsActionPressed(second ? "ui_left_second" : "ui_left"))
            {
                movementState = Action.NegativeSecond;
            }
            else if (Input.IsActionPressed(second ? "ui_down_second" : "ui_down"))
            {
                movementState = Action.NegativeFirst;
            }
            else
            {
                movementState = Action.Timeout;
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
            return movementState;
        }

        public bool IsPerformingAction => false;

        public int Health
        {
            get { throw new NotImplementedException();}
            set { throw new NotImplementedException();}
            
        }
    }
}