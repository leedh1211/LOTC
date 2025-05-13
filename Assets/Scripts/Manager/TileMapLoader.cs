using System;
using System.Collections;
using System.Collections.Generic;
using Monster.AI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileMapLoader : MonoBehaviour
{
    [SerializeField] private List<StageDataSO> stageDataList;
    [SerializeField] private IntegerVariableSO currentMapIndex;
    [SerializeField] private GridManager gridManager;
    private Grid _currentGrid;
    public Grid CurrentGrid
    {
        get { return _currentGrid; }
    }
    private TileMapData _tileMap;

    public TileMapData TileMap
    {
        get { return _tileMap; }
    }

    private float _minY;

    public float MinY
    {
        get { return _minY; }
    }

    private float _maxY;

    public float MaxY
    {
        get { return _maxY; }
    }

    public int numberOfMap = 0;


    public void LoadRandomTileMap(int stageLevel)
    {
        var stageInfo = stageDataList[stageLevel].Stages;
        numberOfMap = stageInfo.Count;
        var currentStage = stageInfo[currentMapIndex.RuntimeValue];

        if (stageInfo == null || currentStage.Maps.Count == 0)
        {
            Debug.LogError("맵이 없음");
            return;
        }
        
        int randomIndex = Random.Range(0, currentStage.Maps.Count);
        var selectedTileMapData = currentStage.Maps[randomIndex];
        LoadTileMap(selectedTileMapData);
    }

    private void LoadTileMap(TileMapData data)
    {
        if (_currentGrid != null)
            Destroy(_currentGrid.gameObject);

        _tileMap = data;
        Vector3 worldPos = new Vector3(data.Position.x, data.Position.y, 0);
        _currentGrid = Instantiate(data.GridMap, worldPos, Quaternion.identity);
        
        var tilemap = _currentGrid.transform.Find("Collider").GetComponent<Tilemap>();
        gridManager.SetTilemap(tilemap); // <- 여기를 반드시 넣어줘야 함
        gridManager.InitGridFromTilemap();
        
        CameraBoundSetting();
    }
    
    private void CameraBoundSetting()
    {
        Debug.Log("=======바운드 세팅=======");
        Tilemap tileMap = _tileMap.GridMap.transform.Find("Collider").GetComponent<Tilemap>();
        if (tileMap != null)
        {
            var bounds = tileMap.cellBounds;
            _minY = bounds.yMin + Camera.main.orthographicSize;
            _maxY = bounds.yMax - Camera.main.orthographicSize;
            
            Camera.main.GetComponent<CameraController>().
                SetBound(_minY, _maxY);
        }
    }
}
