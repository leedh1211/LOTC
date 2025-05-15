using System;
using Data;
using Monster;
using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour
{
    [SerializeField] private TileMapLoader tileMapLoader;
    
    [SerializeField] private MonsterFactory monsterFactory;
    
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameClearPanel;
    
    
    [SerializeField] private Player playerPrefab;

    
    [SerializeField] private VoidEventChannelSO playerDeathEvent;
    [SerializeField] private MonsterEventChannelSO killedMonster;
    [SerializeField] private MonsterListVariableSO monsterList;
    [SerializeField] private IntegerVariableSO selectedStageLevel;
    [SerializeField] private IntegerVariableSO clearedStageLevel;
    [SerializeField] private IntegerVariableSO currentMapIndex;
    [SerializeField] private IntegerVariableSO totalGold;
    [SerializeField] private TransformEventChannelSO rooting;
    [SerializeField] private SpawnDatabaseSO spawnData;
    
    
    
    [Space(10f)]
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private GameClearUI gameClearUI;
    [SerializeField] private GameLevelUI levelUI;

    [Space(10f)]
    [SerializeField] private FloatEventChannelSO onGainExp;
    [SerializeField] private VoidEventChannelSO onLevelUp;
    

    //프리팹은 이름에 프리팹 달기
    [SerializeField] private GameObject doorPrefab;

    [SerializeField] private TileMapDataViewer mapDataViewer;
    
    private Player _player;

    private GameObject _door;

    private int level = 1;
    
    private float _currentExp = 0;

    private float _maxExp = 30;


    private void Start()
    {
        playerDeathEvent.OnEventRaised += GameOver;
        killedMonster.OnEventRaised += KillMonster;
        onGainExp.OnEventRaised += GainExp;

        levelUI.InitLevelText(level);
        
        //매서드 이름 명확히!
        //Init();
        InitGameState();
    }

    private void OnDestroy()
    {
        playerDeathEvent.OnEventRaised -= GameOver;
        killedMonster.OnEventRaised -= KillMonster;
        onGainExp.OnEventRaised -= GainExp;
    }


    void GainExp(float exp)
    {
        _currentExp += exp;
        
        if (_currentExp >= _maxExp)
        {
            var remain = _currentExp - _maxExp;

            _currentExp = remain;

            level++;
            
            levelUI.InitLevelText(level);
            
            onLevelUp.Raise();
        }
        
        levelUI.FillExpImage(_currentExp, _maxExp);
    }


    public void InitGameState()
    {
        tileMapLoader.LoadRandomTileMap(selectedStageLevel.RuntimeValue);

        //가급적 직접 참조 하기!
        //GetComponent<TileMapDataViewer>().tileMapData = tileMapLoader.TileMap;
        mapDataViewer.tileMapData = tileMapLoader.TileMap;
        
        _door = Instantiate(doorPrefab);
        
        _door.transform.position = new Vector2(0f, tileMapLoader.MaxY + Camera.main.orthographicSize - 5);
        
        _door.SetActive(false);
        

        _player = FindAnyObjectByType<Player>();
        
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
        yield return new WaitForSeconds(0.5f);

        rooting.Raise(_player.transform);

        yield return new WaitForSeconds(1.5f);

        _door.SetActive(true);
    }


    private void GameOver()
    {
        gameOverUI.OnGameOver(tileMapLoader.StageName, totalGold.RuntimeValue);
        Time.timeScale = 0;
        SaveManager.Instance.Save();
    }

    private void GameClear()
    {
        if (clearedStageLevel.RuntimeValue <= selectedStageLevel.RuntimeValue)
        {
            clearedStageLevel.RuntimeValue++;
        }

        gameClearUI.OnGameClear(totalGold.RuntimeValue, tileMapLoader.StageName);
        
        currentMapIndex.RuntimeValue = 0;
        Time.timeScale = 0;
        SaveManager.Instance.Save();
    }
}
