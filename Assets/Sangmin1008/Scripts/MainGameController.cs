using System;
using System.Collections;
using System.Collections.Generic;
using Monster;
using Monster.ScriptableObject;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainGameController : MonoBehaviour
{
    [SerializeField] private TileMapLoader tileMapLoader;
    [SerializeField] private Player player;
    [SerializeField] private MonsterFactory monsterFactory;
    [SerializeField] private GameObject playerHpCanvasPrefab;
    [SerializeField] private int monsterSpawnCount = 5;
    
    [SerializeField] private VoidEventChannelSO playerDeathEvent;

    private List<Rect> _monsterSpawner;
    private Vector2 _playerSpawner;

    private void OnEnable()
    {
        playerDeathEvent.OnEventRaised += GameOver;
    }

    private void OnDisable()
    {
        playerDeathEvent.OnEventRaised -= GameOver;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _monsterSpawner = tileMapLoader.TileMap.SpawnArea;
        _playerSpawner = tileMapLoader.TileMap.PlayerSpawnPosition;

        var playerInstance = Instantiate(player, _playerSpawner, Quaternion.identity);
        playerInstance.name = "Player";
        
        var hpCanvas = Instantiate(playerHpCanvasPrefab);
        hpCanvas.transform.SetParent(playerInstance.transform);
        hpCanvas.transform.localPosition = new Vector3(0, -1f, 0);
        
        for (int i = 0; i < monsterSpawnCount; i++)
        {
            var spawnArea = _monsterSpawner[Random.Range(0, _monsterSpawner.Count)];
            var randomX = Random.Range(spawnArea.x - spawnArea.width / 2, spawnArea.x + spawnArea.width / 2);
            var randomY = Random.Range(spawnArea.y - spawnArea.height / 2, spawnArea.y + spawnArea.height / 2);
            var spawnPos = new Vector2(randomX, randomY);

            monsterFactory.SpawnMonster(MonsterName.Mob1, spawnPos, playerInstance.gameObject);
            Debug.Log($"{randomX}, {randomY}");
        }
    }

    private void GameOver()
    {
        
    }
    
}
