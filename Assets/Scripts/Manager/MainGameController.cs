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
    [SerializeField] private GameObject door;
    
    [SerializeField] private Button exitButton;
    
    [SerializeField] private Player playerPrefab;
    private Player _player;

    
    [SerializeField] private VoidEventChannelSO playerDeathEvent;
    [SerializeField] private MonsterEventChannelSO killedMonster;
    [SerializeField] private MonsterListVariableSO monsterList;
    [SerializeField] private IntegerVariableSO selectedStageLevel;
    [SerializeField] private IntegerVariableSO clearedStageLevel;
    [SerializeField] private IntegerVariableSO currentMapIndex;
    [SerializeField] private TransformEventChannelSO rooting;


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

        Debug.LogWarning(tileMapLoader.MaxY);
        door.transform.position = new Vector2(0f, tileMapLoader.MaxY + Camera.main.orthographicSize - 1);

        _player = Instantiate(playerPrefab);
        
        _player.name = "Player";
        
        _player.transform.position = tileMapLoader.TileMap.PlayerSpawnPosition;
        
        Camera.main.GetComponent<CameraController>().Init(_player.transform);

        monsterList.RuntimeValue = new();
        
        for (int i = 0; i < monsterSpawnCount; i++)
        {
            var spawnPos = new Vector2(_monsterSpawner[i].x, _monsterSpawner[i].y);

            var monster = monsterFactory.SpawnMonster(MonsterName.Mob1, spawnPos, _player.gameObject);
            monsterList.RuntimeValue.Add(monster);
        }

        _player.monsterList = monsterList;
    }

    private void KillMonster(MonsterController monster)
    {
        List<MonsterController> monsterList = this.monsterList.RuntimeValue;
        
        monsterList.Remove(monster);

        if (monsterList.Count != 0) return;
        
        Debug.LogWarning("클리어!!!!");
        currentMapIndex.RuntimeValue++;
        rooting.Raise(_player.transform);
        StartCoroutine(WaitAndOpenDoor());
        if (currentMapIndex.RuntimeValue >= tileMapLoader.numberOfMap)
        {
            GameClear();
        }
    }
    
    private IEnumerator WaitAndOpenDoor()
    {
        yield return new WaitForSeconds(2f);
        OpenTheDoor();
    }

    private void OpenTheDoor()
    {
        Debug.LogWarning("오픈더도아");
        door.SetActive(true);
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
