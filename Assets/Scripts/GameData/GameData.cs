using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool isReset;
    public int coinNumber;
    public Vector3 playerPosition;
    public int sceneIndex;
    public SerializableItemDictionary<string, bool> itemCollected;

    //Define default value if no data exsit
    public GameData()
    {
        isReset = true;
        sceneIndex = 1;
        coinNumber = 0;
        itemCollected = new SerializableItemDictionary<string, bool>();
    }
}
