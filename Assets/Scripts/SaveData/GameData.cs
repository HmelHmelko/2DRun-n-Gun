using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData : MonoBehaviour
{
    public int totalKeys;
    public float totalGems;

    public bool levelUnlocked;

    public GameData()
    {
        totalGems = 0;
        totalKeys = 0;
    }
}
