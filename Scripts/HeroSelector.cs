using Godot;
using System;
using System.Collections.Generic;

namespace WFS
{
    public class HeroSelector : Node
    {
        [Export]
        public int amount;
        public bool singlePlayerMode;
        int playerOneIndex = 0;
        bool playerOneLock = false;
        int playerTwoIndex = 0;
        bool playerTwoLock = false;

        List<CharacterChoice> characterChoices;

        public override void _Ready()
        {
            characterChoices = new List<CharacterChoice>();

            WFS.Global global = (WFS.Global)GetNode("/root/Global");
            singlePlayerMode = global.singlePlayer;

//             if (singlePlayerMode)
//             {
//                 playerTwoLock = false;
//                 characterChoices[playerTwoIndex].Unchosen(PlayerChoice.Two);
//             }
            foreach (var node in GetChildren())
            {
                if (node != null && node is CharacterChoice)
                    characterChoices.Add((CharacterChoice)node);
            }
        }

        public override void _Process(float delta)
        {
            if (!playerOneLock && playerOneIndex < amount - 1 && (Input.IsActionJustReleased("ui_up_second") || Input.IsActionJustReleased("ui_right_second")))
            {
                characterChoices[playerOneIndex++].Unchosen(PlayerChoice.One);
            }
            else if (!playerOneLock && playerOneIndex > 0 && (Input.IsActionJustReleased("ui_left_second") || Input.IsActionJustReleased("ui_down_second")))
            {
                characterChoices[playerOneIndex--].Unchosen(PlayerChoice.One);
            }
            if (!singlePlayerMode)
            {
                if (!playerTwoLock && playerTwoIndex < amount - 1 && (Input.IsActionJustReleased("ui_up") || Input.IsActionJustReleased("ui_right")))
                {
                    characterChoices[playerTwoIndex++].Unchosen(PlayerChoice.Two);
                }
                else if (!playerTwoLock && playerTwoIndex > 0 && (Input.IsActionJustReleased("ui_left") || Input.IsActionJustReleased("ui_down")))
                {
                    characterChoices[playerTwoIndex--].Unchosen(PlayerChoice.Two);
                }
            }

            characterChoices[playerOneIndex].Chosen(PlayerChoice.One);
            characterChoices[playerTwoIndex].Chosen(PlayerChoice.Two);

            //if (Input.IsActionJustReleased("ui_accept_second"))
            //{
            //    characterChoices[playerOneIndex].ConfirmChoice(PlayerChoice.One);
            //    playerOneLock = true;
            //}
            if (Input.IsActionJustReleased("ui_accept"))
            {
				characterChoices[playerOneIndex].ConfirmChoice(PlayerChoice.One);
                characterChoices[playerTwoIndex].ConfirmChoice(PlayerChoice.Two);
                playerTwoLock = true;
				playerOneLock = true;
            }

            if (playerOneLock && (playerTwoLock || singlePlayerMode))
            {
                WFS.Global global = (WFS.Global)GetNode("/root/Global");
                global.FirstCharacterSpriteFrameSelection = characterChoices[playerOneIndex].name;
                global.SecondCharacterSpriteFrameSelection = characterChoices[playerTwoIndex].name;

                global.GotoScene("res://Scenes/Root.tscn");
            }
        }
		
		private void _on_Button_button_up()
		{
			characterChoices[playerOneIndex].ConfirmChoice(PlayerChoice.One);
			characterChoices[playerTwoIndex].ConfirmChoice(PlayerChoice.Two);
			playerTwoLock = true;
			playerOneLock = true;
			
			if (playerOneLock && (playerTwoLock || singlePlayerMode))
			{
				WFS.Global global = (WFS.Global)GetNode("/root/Global");
				global.FirstCharacterSpriteFrameSelection = characterChoices[playerOneIndex].name;
				global.SecondCharacterSpriteFrameSelection = characterChoices[playerTwoIndex].name;
				global.GotoScene("res://Scenes/Root.tscn");
			}
		}
    }
}
