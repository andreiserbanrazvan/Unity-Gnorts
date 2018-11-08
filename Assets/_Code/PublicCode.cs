﻿/*
    Written by JelleWho
 */

namespace PublicCode {
    public static class JelleWho {

        //https://www.desmos.com/calculator     y=\frac{-C}{99}\cdot x+B+\frac{C}{99}      X = height     B =SpeedAtHeigh1    and C = SpeedAtHeigh100     //Current added Speed = y=C\cdot x\ +D
        private static readonly float SpeedAtHeigh1 = 0.001f;                                   //A This times heigth = speed at that height
        private static readonly float SpeedAtHeigh100 = 4.5f;                                   //B  This times heigth = speed at that height
        public static float SpeedC = SpeedAtHeigh100 / 99f;                                     //C=\frac{-B}{99}       //99 = height 100 - height 1
        public static float SpeedD = SpeedAtHeigh1 + (SpeedAtHeigh100 / 99f);                   //D=A+\frac{B}{99}

        public static int MoveIfThisCloseToTheSides = 25;                                       //If the mouse is this close to the edge
        public static int RotateSpeedMouse = 300;                                               //The speed of rotating with the scroll wheel
        public static int RotateSpeedKeyboard = 60;                                             //The speed of the rotation when keyboard is used for rotation

        public static int MoveEdgeScrollSpeed = 254;                                            //The speed of the screen when moving with the screen edges
        public static int MoveSpeedMouse = 62;                                                  //The speed of dragging the map with the mouse
        public static int MoveSpeedKeyboard = 44;                                               //The speed of dragging the map with the keyboard
        
        public static int MinCameraHeight = 1;                                                  //The min height the camera needs to be
        public static int MaxCameraHeight = 128;                                                //The max heigh the camera can go
        public static int MaxMoveHorizontalOnMap = 128;                                         //The max distance the camera can be moved away from the center
        public static int ZoomScrollWheelSpeed = 4000;                                          //The speed of dthe ScrollWheel when zooming in and out
        public static int MinimapScrollSpeed = 1;                                               //The speed of the ScrollWheel when zooming in and out on the minimap
    
        public static int BoolSettingsDefault = 0x01;                                           //Set the default value of the Bool Settings
        public static int BoolSettingsLength = 4;                                               //How many settings we are storing


        public static float DeconstructUnused = 0.9f;
        public static float DeconstructUsed = 0.5f;
        /*
            Layout of playerPrefs 'BoolSettings' (1/0):
                Bit 1 = 0x01 = EdgeScroll (on/off)
                Bit 2 = 0x02 = 
                Bit 3 = 0x04 = 
                Bit 4 = 0x08 = 
                Bit 5 = 0x10 = 
                Bit 6 = 0x20 = 
                Bit 7 = 0x40 = 
                Bit 8 = 0x80 = 

            Read
                if ((PlayerPrefs.GetInt("BoolSettings",JelleWho.BoolSettingsDefault) & 0x02) == 0x02){}                         //Read the bit
            Set
                PlayerPrefs.SetInt("BoolSettings", (PlayerPrefs.GetInt("BoolSettings", JelleWho.BoolSettingsDefault) | 0x02));  //Set bit TRUE
            Clear
                 PlayerPrefs.SetInt("BoolSettings", (PlayerPrefs.GetInt("BoolSettings", JelleWho.BoolSettingsDefault) & ~0x02));//Set bit FALSE
            Toggle
                PlayerPrefs.SetInt("BoolSettings", (PlayerPrefs.GetInt("BoolSettings", JelleWho.BoolSettingsDefault) ^ 0x02));  //Toggle
        */
    }
    public class SettingsBool                                                           //Create a new Class data type to store the bools in
    {
        //Please see the next url if you don't get this. it was really helpfull for me
        //https://unity3d.com/learn/tutorials/topics/scripting/classes
        public string Name;                                                                     //(To GET data) 1th part is a string,   The name
        public bool Stat;                                                                       //(To GET data) 2nd part is a bool,     The default state
        public string Desc;                                                                     //(To GET data) 3rd part is a string,   The description (hoverover text)
        public SettingsBool(string name, bool default_status, string description)               //Create a way to add all data at once
        {
            this.Name = name;                                                                   //(To SET data) 1th part is a string,   The name
            this.Stat = default_status;                                                         //(To SET data) 2nd part is a bool,     The default state
            this.Desc = description;                                                            //(To SET data) 3rd part is a string,   The description (hoverover text)
        }
    }
    /// <summary>
    /// Describes a keybinding.
    /// </summary>
    public class KeyBinding                                                             //Create a new data type to store all keybindings in
    {
        public string Name;                                                                     //(To GET data) 1th part is a string,   The name
        public UnityEngine.KeyCode KeyCode;                                                     //(To GET data) 1th part is a string,   The set key
        public string Description;                                                              //(To GET data) 1th part is a string,   The description (hoverover text)
        public KeyBinding(string name, UnityEngine.KeyCode keyCode, string description)         //Create a way to add all data at once
        {
            Name = name;                                                                        //(To SET data) 1th part is a string,   The name
            KeyCode = keyCode;                                                                  //(To SET data) 2nd part is a bool,     The default state
            Description = description;                                                          //(To SET data) 3rd part is a string,   The description (hoverover text)
        }
    }
    
    /// <summary>
    /// Indicates the type of building.
    /// </summary>
    public enum BuildType
    {
        Normal,
        Wall,
        SpikedWall,
        Stair,
        FireBasket,
        ReplaceWall,
        Bridge,
    }
}

//public static bool EdgeScroll { get { if ((PlayerPrefs.GetInt("BoolSettings", JelleWho.BoolSettingsDefault) & 0x02) == 0x02) return true; else return false; } }
