using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileMapLoader : MonoBehaviour
{
    [SerializeField] private List<StageData> stageDataList;
    [SerializeField] private IntegerVariableSO currentMapIndex;
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

    public int numberOfMap = 0;


    public void LoadRandomTileMap(int stageLevel)
    {
        Debug.LogWarning(stageLevel);
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
        
        CameraBoundSetting();
    }
    
    private void CameraBoundSetting()
    {
        Debug.Log("=======바운드 세팅=======");
        Tilemap tileMap = _tileMap.GridMap.transform.Find("Collider").GetComponent<Tilemap>();
        if (tileMap != null)
        {
            var bounds = tileMap.cellBounds;
            float minY = bounds.yMin + Camera.main.orthographicSize;
            float maxY = bounds.yMax - Camera.main.orthographicSize;
            
            Camera.main.GetComponent<CameraController>().
                SetBound(minY, maxY);
        }
    }
}
