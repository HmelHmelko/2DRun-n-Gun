using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public Collector Collector;
    public Player player;
    public UIGameOverScreen gameOverScreen;

    private void Awake()
    {
        gameData = SaveManager.Load();
        Collector = FindObjectOfType<Collector>();
        player = FindObjectOfType<Player>();
    }


    public void GameOver()
    {
        gameData.totalGems += Collector.gems;
        gameData.totalKeys += Collector.keyCount;
        SaveManager.Save(gameData);

        if(player.Health <= 0)
        {
            gameOverScreen.EnableScreen();
        }
    }
}
