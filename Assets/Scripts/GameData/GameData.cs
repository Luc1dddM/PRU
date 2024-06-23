using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int sceneIndex;
    public SerializableItemDictionary<string, bool> itemCollected;

    //Define default value if no data exsit
    public GameData()
    {
        sceneIndex = 1;
        itemCollected = new SerializableItemDictionary<string, bool>();
        playerPosition = new Vector3(7, 11, 5);
    }
}
