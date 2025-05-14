/*
 * 
using Data;
using Monster;
using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour
{
    [SerializeField] private int monsterSpawnCount = 0;


    [SerializeField] private TileMapLoader tileMapLoader;
    
    [SerializeField] private MonsterFactory monsterFactory;
    
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameClearPanel;
    [SerializeField] private GameObject door;
    private GameObject _door;
    
    
    [SerializeField] private Player playerPrefab;
    private Player _player;

    
    [SerializeField] private VoidEventChannelSO playerDeathEvent;
    [SerializeField] private MonsterEventChannelSO killedMonster;
    [SerializeField] private MonsterListVariableSO monsterList;
    [SerializeField] private IntegerVariableSO selectedStageLevel;
    [SerializeField] private IntegerVariableSO clearedStageLevel;
    [SerializeField] private IntegerVariableSO currentMapIndex;
    [SerializeField] private IntegerVariableSO totalGold;
    [SerializeField] private TransformEventChannelSO rooting;
    [SerializeField] private SpawnDatabaseSO spawnData;
    
    [SerializeField] private TextMeshProUGUI gameOverState;
    [SerializeField] private TextMeshProUGUI gameOverGoldText;
    [SerializeField] private TextMeshProUGUI gameClearState;
    [SerializeField] private TextMeshProUGUI gameClearGoldText;

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
    }



    private void Init()
    {
        _monsterSpawner = tileMapLoader.TileMap.SpawnArea;
        monsterSpawnCount = _monsterSpawner.Count;
        GetComponent<TileMapDataViewer>().tileMapData = tileMapLoader.TileMap;

        Debug.LogWarning(tileMapLoader.MaxY);
        _door = Instantiate(door);
        _door.transform.position = new Vector2(0f, tileMapLoader.MaxY + Camera.main.orthographicSize - 5);
        _door.SetActive(false);

        _player = Instantiate(playerPrefab);
        _player.name = "Player";
        _player.transform.position = tileMapLoader.TileMap.PlayerSpawnPosition;
        
        Camera.main.GetComponent<CameraController>().Init(_player.transform);

        SpawnRoom(selectedStageLevel.RuntimeValue, currentMapIndex.RuntimeValue);
    }


    void SpawnRoom(int stageLevel, int roomIndex)
    {
        StageRoomSpawnDataSO spawnDataSo = spawnData.GetSpawnData(stageLevel, roomIndex);

        if (spawnDataSo == null)
        {
            return;
        }
        monsterList.RuntimeValue = new();
        foreach (var info in spawnDataSo.spawns)
        {
            MonsterController monster = monsterFactory.SpawnMonster(info.monsterName, info.spawnPosition, _player.gameObject);
            monsterList.RuntimeValue.Add(monster);
            
        }
        _player.monsterList = monsterList;
    }

    private void KillMonster(MonsterController monster)
    {
        List<MonsterController> monsterList = this.monsterList.RuntimeValue;
        
        monsterList.Remove(monster);

        if (monsterList.Count != 0) return;
        
        AchievementManager.Instance.AddProgress(5,1);
        currentMapIndex.RuntimeValue++;
        StartCoroutine(DelayEvent());
        if (currentMapIndex.RuntimeValue >= tileMapLoader.numberOfMap)
        {
            GameClear();
        }
    }
    
    private IEnumerator DelayEvent()
    {
        yield return new WaitForSeconds(0.2f);

        rooting.Raise(_player.transform);

        yield return new WaitForSeconds(1.5f);

        OpenTheDoor();
    }

    private void OpenTheDoor()
    {
        _door.SetActive(true);
    }

    private void GameOver()
    {
        gameOverState.text = tileMapLoader.StageName;
        gameOverGoldText.text = totalGold.RuntimeValue.ToString();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        SaveManager.Instance.Save();
    }

    private void GameClear()
    {
        if (clearedStageLevel.RuntimeValue <= selectedStageLevel.RuntimeValue)
        {
            clearedStageLevel.RuntimeValue++;
        }

        gameClearState.text = tileMapLoader.StageName;
        gameClearGoldText.text = totalGold.RuntimeValue.ToString();
        
        currentMapIndex.RuntimeValue = 0;
        gameClearPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
        SaveManager.Instance.Save();
    }
}
 */
