using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public UIManager uiManager;
    public SpawnManager spawnManager;

    public bool isStarted = true; // TODO : main menu yapılınca bura değişecek

    private void Start()
    {
        instance = this;
        spawnManager.StartSpawners();
    }

    private void Update()
    {
        
    }
}