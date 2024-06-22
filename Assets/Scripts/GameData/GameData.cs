using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int sceneIndex;

    //Define default value if no data exsit
    public GameData()
    {
        sceneIndex = 1;
    }
}
