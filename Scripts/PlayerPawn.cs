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
        private ConfigFile config;
        private int healthCurrent;
        private int healthMax;

        private float timer = 1;
        private float timePassed = 0;
        private bool set = false;

        private Dictionary<Action, string> actionToAnimation;
        public override void _Ready()
        {
            config = new ConfigFile();
            config.Load("res://GameConfig.cfg");
            healthMax = (int)config.GetValue("Config", "InitHealth");
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

            //if (movementState == Action.Timeout)
            if(!set)
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
                set = true;
            }
            else if (Input.IsActionJustReleased(second ? "ui_right_second" : "ui_right"))
            {
                movementState = Action.PositiveSecond;
                set = true;
            }
            else if (Input.IsActionJustReleased(second ? "ui_left_second" : "ui_left"))
            {
                movementState = Action.NegativeSecond;
                set = true;
            }
            else if (Input.IsActionJustReleased(second ? "ui_down_second" : "ui_down"))
            {
                movementState = Action.NegativeFirst;
                set = true;
            }
            else
            {
                //movementState = Action.Timeout;
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
            set = false;
        }

        Action IActionProvider.ProvideAction()
        {
            Action temp = movementState;
            movementState = Action.Timeout;
            set = false;
            return temp;
        }

        public bool IsPerformingAction
        {
            get
            {
//                bool value = timePassed <= timer;
//                if (!value)
//                    timePassed = 0;
//                return value;
                return false;
            }
        }

        public int Health
        {
            get { return healthCurrent; }
            set { healthCurrent = value; }
        }
    }
}