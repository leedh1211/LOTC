using System;
using System.Collections;
using System.Collections.Generic;
using Monster;
using Monster.ScriptableObject;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainGameController : MonoBehaviour
{
    [SerializeField] private int monsterSpawnCount = 5;


    [SerializeField] private TileMapLoader tileMapLoader;
    
    [SerializeField] private MonsterFactory monsterFactory;
    
    [SerializeField] private GameObject gameOverPanel;
    
    [SerializeField] private Button gameClearButton;
    
    [SerializeField] private Player playerPrefab;

    
    [SerializeField] private VoidEventChannelSO playerDeathEvent;
    [SerializeField] private MonsterEventChannelSO killedMonster;
    [SerializeField] private MonsterListVariableSO monsterList;
    [SerializeField] private IntegerVariableSO selectedStageLevel;
    [SerializeField] private IntegerVariableSO clearedStageLevel;

    private List<Rect> _monsterSpawner;
    private void OnEnable()
    {
        playerDeathEvent.OnEventRaised += GameOver;
        killedMonster.OnEventRaised += KillMonster;
    }

    private void OnDisable()
    {
        playerDeathEvent.OnEventRaised -= GameOver;
        killedMonster.OnEventRaised -= KillMonster;
    }


    private void Start()
    {
        tileMapLoader.LoadRandomTileMap(selectedStageLevel.RuntimeValue);

        Init();
        
        gameClearButton.onClick.AddListener(()=>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("LobbyScene");
        });
        
        gameClearButton.gameObject.SetActive(false);
    }

    private void Init()
    {
        _monsterSpawner = tileMapLoader.TileMap.SpawnArea;

        Player player = Instantiate(playerPrefab);
        
        player.name = "Player";
        
        player.transform.position = tileMapLoader.TileMap.PlayerSpawnPosition;

        monsterList.RuntimeValue = new();
        
        for (int i = 0; i < monsterSpawnCount; i++)
        {
            var spawnArea = _monsterSpawner[Random.Range(0, _monsterSpawner.Count)];
            var randomX = Random.Range(spawnArea.x - spawnArea.width / 2, spawnArea.x + spawnArea.width / 2);
            var randomY = Random.Range(spawnArea.y - spawnArea.height / 2, spawnArea.y + spawnArea.height / 2);
            var spawnPos = new Vector2(randomX, randomY);

            var monster = monsterFactory.SpawnMonster(MonsterName.Mob1, spawnPos, player.gameObject);
            
            Debug.Log($"{randomX}, {randomY}");

            monsterList.RuntimeValue.Add(monster);
        }

        player.monsterList = monsterList;
    }

    private void KillMonster(MonsterController monster)
    {
        List<MonsterController> monsterList = this.monsterList.RuntimeValue;
        
        monsterList.Remove(monster);

        if (monsterList.Count == 0)
        {
            GameClear();
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void GameClear()
    {
        if (clearedStageLevel.RuntimeValue <= selectedStageLevel.RuntimeValue)
        {
            clearedStageLevel.RuntimeValue++;
        }
        
        gameClearButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
