using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameActionsScript : MonoBehaviour
{
    //GameTextPanel Components
    public GameObject GameTextPanel;
    public TMP_Text GameText;

    //MainMenu Components
    public Button MoveButton;
    public Button BackpackButton;

    //MoveSubMenu Components
    public GameObject MoveSubMenu;
    public Button LeftButton;
    public Button RightButton;
    public Button ForwardButton;

    //BackPackSubMenu Components
    public GameObject BackpackSubMenu;
    public Button FlashlightButton;
    public Button BatteriesButton;
    public Button KeysButton;

    //FlashlightSubMenu Components
    public GameObject FlashlightSubMenu;
    public Button FlashlightEquipButton;
    public Button FlashlightUnequipButton;

    //BatteriesSubMenu Components;
    public GameObject BatteriesSubMenu;
    public Button BatteriesUseButton;

    //KeysSubMenu Components;
    public GameObject KeysSubMenu;
    public Button KeysUseButton;
    public Button KeysEquipButton;
    public Button KeysUnequipButton;

    //Player state
    string playerLatestMove = "";

    //Tile class (makes up the map)
    public class Tile
    {
        string tileName;
        string pressForwardGameText;
        string pressRightGameText;
        string pressLeftGameText;
        string entryCondition;
        string entryConditionAchievedGameText;
        Tile adjacentTile;

        public Tile(string tileName_, string pressForwardGameText_, string pressRightGameText_, string pressLeftGameText_, string entryCondition_, string entryConditionAchievedGameText_, Tile adjacentTile_)
        {
            tileName = tileName_;
            pressForwardGameText = pressForwardGameText_;
            pressRightGameText = pressRightGameText_;
            pressLeftGameText = pressLeftGameText_;
            entryCondition = entryCondition_;
            entryConditionAchievedGameText = entryConditionAchievedGameText_;
            adjacentTile = adjacentTile_;
        }

        public string TileName
        {
            get { return tileName; }
            set { tileName = value; }
        }

        public string PressForwardGameText
        {
            get { return pressForwardGameText; }
            set { pressForwardGameText = value; }
        }

        public string PressRightGameText
        {
            get { return pressRightGameText; }
            set { pressRightGameText = value; }
        }

        public string PressLeftGameText
        {
            get { return pressLeftGameText; }
            set { pressLeftGameText = value; }
        }

        public string EntryCondition
        {
            get { return entryCondition; }
            set { entryCondition = value; }
        }

        public string EntryConditionAchievedGameText
        {
            get { return entryConditionAchievedGameText; }
            set { entryConditionAchievedGameText = value; }
        }

        public Tile AdjacentTile
        {
            get { return adjacentTile; }
            set { adjacentTile = value; }
        }

    }

    // Map and player location
    public static class Map
    {
        public static Tile NullTile = new Tile("NullTile", "", "", "", "", "", null);
        public static Tile Tile3 = new Tile("Tile3", "You try to move forward but a door is in your way. One of your keys might be able to open this door.", "You try to move right but you encounter a wall.", "You try to move left but you encounter a wall.", "Left", "You turn left and walk a few steps.", NullTile);
        public static Tile Tile2 = new Tile("Tile2", "You try to move forward but you encounter a wall.", "You try to move right but you encounter a wall.", "", "Right", "You turn right and walk a few steps. You find keys on the ground.", Tile3);
        public static Tile Tile1 = new Tile("Tile1", "You try to move forward but you encounter a wall.", "", "You try to move left but you encounter a wall.", "Forward", "You move a few steps forward.", Tile2);
        public static Tile Tile0 = new Tile("Tile0", "" , "You try to move right but you encounter a wall." , "You try to move left but you encounter a wall." , "" , "" , Tile1);
        
        public static Tile playerLocation = Tile0;
    }

    public static class FlashlightState
    {
        public static bool flashlightHasBatteries = false;
        public static bool flashlightIsActivated = false;
    }

    public void MovePlayer() //Event that happens when a movement is made
    {

        if (FlashlightState.flashlightIsActivated == true)
        {

            string lastButtonPressed = EventSystem.current.currentSelectedGameObject.name;

            if (lastButtonPressed == "LeftButton")
            {
                playerLatestMove = "Left";
                if(Map.playerLocation.AdjacentTile.EntryCondition == playerLatestMove)
                {
                    //Game Text Update
                    GameTextPanel.SetActive(false);
                    GameText.text = Map.playerLocation.AdjacentTile.EntryConditionAchievedGameText;
                    GameTextPanel.SetActive(true);

                    //Change player position
                    Map.playerLocation = Map.playerLocation.AdjacentTile;

                    //Gamestate changes
                    GiveKeys();

                    // Hide menus after button press.
                    MoveSubMenu.SetActive(false);

                    //Re-enable buttons
                    LeftButton.interactable = true;
                    RightButton.interactable = true;
                    ForwardButton.interactable = true;
                    
                

                } else
                {
                    //Game Text Update
                    GameTextPanel.SetActive(false);
                    GameText.text = Map.playerLocation.PressLeftGameText;
                    GameTextPanel.SetActive(true);

                    // Hide menus after button press
                    MoveSubMenu.SetActive(false);

                    //disable buttons
                    LeftButton.interactable = false;

                }
            } else if (lastButtonPressed == "RightButton")
            {
                playerLatestMove = "Right";
                if (Map.playerLocation.AdjacentTile.EntryCondition == playerLatestMove)
                {
                    //Game Text Update
                    GameTextPanel.SetActive(false);
                    GameText.text = Map.playerLocation.AdjacentTile.EntryConditionAchievedGameText;
                    GameTextPanel.SetActive(true);

                    //Change player position
                    Map.playerLocation = Map.playerLocation.AdjacentTile;

                    //Gamestate changes
                    GiveKeys();

                    // Hide menus after button press.
                    MoveSubMenu.SetActive(false);

                    //Re-enable buttons
                    LeftButton.interactable = true;
                    RightButton.interactable = true;
                    ForwardButton.interactable = true;


                }
                else
                {
                    //Game Text Update
                    GameTextPanel.SetActive(false);
                    GameText.text = Map.playerLocation.PressRightGameText;
                    GameTextPanel.SetActive(true);

                    // Hide menus after button press
                    MoveSubMenu.SetActive(false);

                    //disable buttons
                    RightButton.interactable = false;

                }
            } else if (lastButtonPressed == "ForwardButton")
            {
                playerLatestMove = "Forward";
                if (Map.playerLocation.AdjacentTile.EntryCondition == playerLatestMove)
                {
                    //Game Text Update
                    GameTextPanel.SetActive(false);
                    GameText.text = Map.playerLocation.AdjacentTile.EntryConditionAchievedGameText;
                    GameTextPanel.SetActive(true);

                    //Change player position
                    Map.playerLocation = Map.playerLocation.AdjacentTile;

                    //Gamestate changes
                    GiveKeys();

                    // Hide menus after button press.
                    MoveSubMenu.SetActive(false);

                    //Re-enable buttons
                    LeftButton.interactable = true;
                    RightButton.interactable = true;
                    ForwardButton.interactable = true;

                }
                else
                {
                    //Game Text Update
                    GameTextPanel.SetActive(false);
                    GameText.text = Map.playerLocation.PressForwardGameText;
                    GameTextPanel.SetActive(true);

                    // Hide menus after button press
                    MoveSubMenu.SetActive(false);

                    //disable buttons
                    ForwardButton.interactable = false;
                    
                }
            }

        } else
        {
            // Game text manipulation when movement is made when flashlight is not on
            GameTextPanel.SetActive(false);
            GameText.text = "You stumble in the darkness. Perhaps you should find a light source.";
            GameTextPanel.SetActive(true);

            // Hide menus after button press
            MoveSubMenu.SetActive(false);

            //some buttons are disabled when a course of action is currently useless.
            LeftButton.interactable = false;
            RightButton.interactable = false;
            ForwardButton.interactable = false;

        }
        
    }

    public void EquipFlashlight()
    {

        //Add if statement about whether or not batteries are already in or not.
        if (FlashlightState.flashlightHasBatteries == true)
        {
            // Game text manipulation when flashlight is used but batteries have finally been put in
            GameTextPanel.SetActive(false);
            GameText.text = "The flashlight emits a faint light. You are now able to see where you are going.";
            GameTextPanel.SetActive(true);

            // Gamestate changes
            FlashlightState.flashlightIsActivated = true;

            //hide menus after button press
            BackpackSubMenu.SetActive(false);
            FlashlightSubMenu.SetActive(false);

            //some buttons are re-enabled after some actions are made.
            LeftButton.interactable = true;
            RightButton.interactable = true;
            ForwardButton.interactable = true;
            FlashlightUnequipButton.gameObject.SetActive(true);

            //some buttons are disabled when a course of action is currently useless..
            KeysButton.interactable = false;
            FlashlightEquipButton.gameObject.SetActive(false);

        } else 
        {
            // Game text manipulation when flashlight is used but batteries have not been put in it yet
            GameTextPanel.SetActive(false);
            GameText.text = "The flashlight won't turn on. You see no point in holding it for now.";
            GameTextPanel.SetActive(true);

            //hide menus after button press
            BackpackSubMenu.SetActive(false);
            FlashlightSubMenu.SetActive(false);

            //some buttons are disabled when a course of action is currently useless.
            FlashlightButton.interactable = false;
        }

    }

    public void UnequipFlashlight()
    {
        // Game text manipulation when flashlight is put away
        GameTextPanel.SetActive(false);
        GameText.text = "You turn your flashlight off and put it away. Your hands are now free to hold something else.";
        GameTextPanel.SetActive(true);

        // Gamestate changes
        FlashlightState.flashlightIsActivated = false;

        //hide menus after button press
        BackpackSubMenu.SetActive(false);
        FlashlightSubMenu.SetActive(false);

        //some buttons are re-enabled after some actions are made.
        KeysButton.interactable = true;
        FlashlightEquipButton.gameObject.SetActive(true);

        //some buttons are disabled when a course of action is currently useless..
        FlashlightUnequipButton.gameObject.SetActive(false);
    }

    public void EquipKeys()
    {
        // Game text manipulation
        GameTextPanel.SetActive(false);
        GameText.text = "You hold the keys you found on the ground.";
        GameTextPanel.SetActive(true);

        //hide menus after button press
        BackpackSubMenu.SetActive(false);
        KeysSubMenu.SetActive(false);

        //some buttons are re-enabled after some actions are made.
        KeysUseButton.gameObject.SetActive(true);
        KeysUnequipButton.gameObject.SetActive(true);

        //some buttons are disabled when a course of action is currently useless..
        FlashlightButton.interactable = false;
        KeysEquipButton.gameObject.SetActive(false);
        
    }

    public void UnequipKeys()
    {
        // Game text manipulation
        GameTextPanel.SetActive(false);
        GameText.text = "You put away the keys.";
        GameTextPanel.SetActive(true);

        //hide menus after button press
        BackpackSubMenu.SetActive(false);
        KeysSubMenu.SetActive(false);

        //some buttons are re-enabled after some actions are made.
        FlashlightButton.interactable = true;
        KeysEquipButton.gameObject.SetActive(true);

        //some buttons are disabled when a course of action is currently useless..
        KeysUseButton.gameObject.SetActive(false);
        KeysUnequipButton.gameObject.SetActive(false);
    }

    public void UseBatteries()
    {
        // Game text manipulation when batteries are used
        GameTextPanel.SetActive(false);
        GameText.text = "You replace your flashlight's batteries.";
        GameTextPanel.SetActive(true);

        // Gamestate changes
        FlashlightState.flashlightHasBatteries = true;

        //hide menus after button press
        BackpackSubMenu.SetActive(false);
        BatteriesSubMenu.SetActive(false);

        //some buttons are re-enabled after some actions are made.
        FlashlightButton.interactable = true;
        FlashlightEquipButton.interactable = true;

        //some buttons are disabled/hidden when a course of action is currently useless.
        BatteriesButton.gameObject.SetActive(false);

    }

    public void UseKeys()
    {
        if (Map.playerLocation.TileName == "Tile3")
        {
            // Game text manipulation 
            GameTextPanel.SetActive(false);
            GameText.text = "You unlock and open the door in front of you. A hooded figure stands before you.";
            GameTextPanel.SetActive(true);

            //hide menus after button press
            BackpackSubMenu.SetActive(false);
            KeysSubMenu.SetActive(false);

            //disable buttons
            MoveButton.interactable = false;
            BackpackButton.interactable = false;

        } else
        {
            // Game text manipulation 
            GameTextPanel.SetActive(false);
            GameText.text = "You see no point in using this at the moment.";
            GameTextPanel.SetActive(true);

            //hide menus after button press
            BackpackSubMenu.SetActive(false);
            KeysSubMenu.SetActive(false);
        }
    }

    public void GiveKeys()
    {
        if (Map.playerLocation.TileName == "Tile2")
        {
            KeysButton.gameObject.SetActive(true);
        }
    }


}
