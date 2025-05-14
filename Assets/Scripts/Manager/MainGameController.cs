using System;
using Data;
using Monster;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
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
    
    private int displayLevel = 1;
    
    private float _currentExp = 0;

    private float _maxExp = 30;


    [SerializeField] private IntegerVariableSO gameLevel;
    [SerializeField] private FloatEventChannelSO onGainExp;
    [SerializeField] private VoidEventChannelSO onLevelUp;
    [SerializeField] private FloatVariableSO gameExp;
    
    [SerializeField] private GameLevelUI levelUI;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private UnityEngine.UI.Image expImage;


    private void Start()
    {
        playerDeathEvent.OnEventRaised += GameOver;
        killedMonster.OnEventRaised += KillMonster;
        onGainExp.OnEventRaised += GainExp;

        NewGame();
    }

    private void OnDestroy()
    {
        playerDeathEvent.OnEventRaised -= GameOver;
        killedMonster.OnEventRaised -= KillMonster;
        onGainExp.OnEventRaised -= GainExp;
    }


    public void NewGame()
    {
        tileMapLoader.LoadRandomTileMap(selectedStageLevel.RuntimeValue);
        
        Init();

        _currentExp = gameExp.RuntimeValue;
        
        expImage.fillAmount = _currentExp / _maxExp;
        
        levelText.text = "Lv " + (gameLevel.RuntimeValue + 1).ToString();
    }


    private void Update()
    {
        if (_currentExp >= _maxExp)
        {
            var remain = _currentExp - _maxExp;

            _currentExp = remain;

            gameLevel.RuntimeValue ++;

            levelText.text = "Lv " + (gameLevel.RuntimeValue + 1).ToString();
            
            onLevelUp.Raise();
            
            expImage.fillAmount = _currentExp / _maxExp;
        }

        gameExp.RuntimeValue = _currentExp;
    }

    void GainExp(float exp)
    {
        _currentExp += exp;
        
        expImage.fillAmount = _currentExp / _maxExp;
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
