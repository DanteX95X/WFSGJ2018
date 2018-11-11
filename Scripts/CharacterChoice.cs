using Godot;
using System;

namespace WFS
{
    public enum PlayerChoice
    {
        One = 0,
        Two
    }

    public class CharacterChoice : Node2D
    {
        [Export]
        public string name;
        [Export]
        public bool P1Check;
        [Export]
        public bool P2Check;
        [Export]
        public bool locked;
        [Export]
        public Texture portait;
        ColorRect colorRect;

        TextureRect choice_one;
        TextureRect choice_two;
        TextureRect lockRect;
        TextureRect portaitRect;

        public override void _Ready()
        {
            colorRect = (ColorRect)GetNode("Border");
            choice_one = (TextureRect)GetNode("P1Select");
            choice_two = (TextureRect)GetNode("P2Select");
            lockRect = (TextureRect)GetNode("Lock");
            portaitRect = (TextureRect)GetNode("Portait");

            portaitRect.Texture = portait;

            if (P1Check)
            {
                choice_one.Show();
            }
            if (P2Check)
            {
                choice_two.Show();
            }

            if (locked)
            {
                lockRect.Show();
            }
        }

        public void Chosen(PlayerChoice playerChoice)
        {
            //colorRect.Color = new Color(255,255,0); may be useful
            if (playerChoice == PlayerChoice.One)
            {
                choice_one.Show();
            }
            else if (playerChoice == PlayerChoice.Two)
            {
                choice_two.Show();
            }
        }

        public void Unchosen(PlayerChoice playerChoice)
        {
            if (playerChoice == PlayerChoice.One)
            {
                choice_one.Hide();
            }
            else if (playerChoice == PlayerChoice.Two)
            {
                choice_two.Hide();
            }
        }

        public void ConfirmChoice(PlayerChoice playerChoice)
        {
            //choice_one.AddColorOverride("", new Color(255, 0, 0));
        }
    }
}