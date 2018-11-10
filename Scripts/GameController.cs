using Godot;
using System.Collections.Generic;

namespace WFS
{
    public class GameController : Node
    {
        private State state;

        List<IActionProvider> players = new List<IActionProvider>();
        private int turn;

        private Label fightLabel;
        float fightLabelTimer;
        float fightLabelTimeMax;

        public IActionProvider Attacker
        {
            get
            {
                int index = turn % 2;
                return players[1 - index];
            }
        }

        public IActionProvider Defender
        {
            get
            {
                int index = turn % 2;
                return players[index];
            }
        }

        public int Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        public State CurrentState
        {
            get { return state; }
        }

        public override void _Ready()
        {
            GD.Print("Controller started");

            players.Add((IActionProvider)GetNode("Player1"));
            players.Add((IActionProvider)GetNode("Player2"));

            //Timers TODO: Read Max from config
            fightLabelTimeMax = 1;

            //UI
            fightLabel = (Label)GetNode("FightLabel");

            ResetFightLabel();

            foreach (var player in players)
            {
                player.controller = this;
            }

            turn = 1;

            state = new PreRecordState(this, turn);
        }

        public void ResetFightLabel()
        {
            fightLabel.Show();
            fightLabelTimer = 0;
        }

        public override void _Process(float delta)
        {
            //Process Timers
            fightLabelTimer += delta;

            if (fightLabelTimer > fightLabelTimeMax)
            {
                fightLabel.Hide();
            }

            state = state?.Update(delta);
        }
    }
}