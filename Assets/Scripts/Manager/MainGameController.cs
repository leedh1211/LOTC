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
    [SerializeField] private int monsterSpawnCount = 0;


    [SerializeField] private TileMapLoader tileMapLoader;
    
    [SerializeField] private MonsterFactory monsterFactory;
    
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameClearPanel;
    //[SerializeField] private GameObject door;
    
    [SerializeField] private Button exitButton;
    
    [SerializeField] private Player playerPrefab;

    
    [SerializeField] private VoidEventChannelSO playerDeathEvent;
    [SerializeField] private MonsterEventChannelSO killedMonster;
    [SerializeField] private MonsterListVariableSO monsterList;
    [SerializeField] private IntegerVariableSO selectedStageLevel;
    [SerializeField] private IntegerVariableSO clearedStageLevel;
    [SerializeField] private IntegerVariableSO currentMapIndex;


    private List<Vector2> _monsterSpawner;
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
        
        exitButton.onClick.AddListener(()=>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("LobbyScene");
        });
    }



private void Init()
    {
        _monsterSpawner = tileMapLoader.TileMap.SpawnArea;
        monsterSpawnCount = _monsterSpawner.Count;
        GetComponent<TileMapDataViewer>().tileMapData = tileMapLoader.TileMap;

        Player player = Instantiate(playerPrefab);
        
        player.name = "Player";
        
        player.transform.position = tileMapLoader.TileMap.PlayerSpawnPosition;
        
        Camera.main.GetComponent<CameraController>().Init(player.transform);

        monsterList.RuntimeValue = new();
        
        for (int i = 0; i < monsterSpawnCount; i++)
        {
            var spawnPos = new Vector2(_monsterSpawner[i].x, _monsterSpawner[i].y);

            var monster = monsterFactory.SpawnMonster(MonsterName.Mob1, spawnPos, player.gameObject);
            monsterList.RuntimeValue.Add(monster);
        }

        player.monsterList = monsterList;
    }

    private void KillMonster(MonsterController monster)
    {
        List<MonsterController> monsterList = this.monsterList.RuntimeValue;
        
        monsterList.Remove(monster);

        if (monsterList.Count != 0) return;
        
        currentMapIndex.RuntimeValue++;
        if (currentMapIndex.RuntimeValue >= tileMapLoader.numberOfMap)
        {
            GameClear();
        }
        else
        {
            OpenTheDoor();
        }
    }

    // 맵에 있는 몬스터 전부 죽이면 실행
    private void OpenTheDoor()
    {
        //door.SetActive(true);
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

        currentMapIndex.RuntimeValue = 0;
        gameClearPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
