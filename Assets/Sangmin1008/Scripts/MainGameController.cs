using System.Collections;
using System.Collections.Generic;
using Monster;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    [SerializeField] private TileMapLoader tileMapLoader;
    [SerializeField] private Player player;
    [SerializeField] private MonsterFactory monsterFactory;

    private List<Rect> _monsterSpawner;
    private Vector2 _playerSpawner;

    private void Init()
    {
        _monsterSpawner = tileMapLoader.TileMap.SpawnArea;
        _playerSpawner = tileMapLoader.TileMap.PlayerSpawnPosition;
    }
    
}
