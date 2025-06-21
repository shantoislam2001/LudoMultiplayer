using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Ludo online", menuName = "ScriptableObjects/Ludo online", order = 1)]
public class LudoOnline : ScriptableObject
{
    public bool multiplayerMode;
    public bool test;
    public bool testClick;
    public int selfColour;
    public RoomData bluePlayer;
    public RoomData redPlayer;
    public RoomData greenPlayer;
    public RoomData yellowPlayer;

    [System.Serializable]
    public class RoomData
    {
        public string currentRoom;
        public int colour;
        public string pice;
        public int dice;
        public bool clicked;

    }


   

}
