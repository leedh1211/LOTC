using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileMapLoader : MonoBehaviour
{
    private Grid _currentGrid;
    public Grid CurrentGrid
    {
        get { return _currentGrid; }
    }
    [SerializeField] private StageData _stageData;
    [SerializeField] private int _currentMainStage;

    private void Start()
    {
        LoadRandomTileMap(_currentMainStage);
    }

    public void LoadRandomTileMap(int mainStage)
    {
        var stageInfo = _stageData.Stages.Find(s => s.MainStage == mainStage);

        if (stageInfo == null || stageInfo.Maps.Count == 0)
        {
            Debug.LogError("맵이 없음");
            return;
        }
        
        int randomIndex = Random.Range(0, stageInfo.Maps.Count);
        var selectedTileMapData = stageInfo.Maps[randomIndex];
        LoadTileMap(selectedTileMapData);
    }

    private void LoadTileMap(TileMapData data)
    {
        if (_currentGrid != null)
            Destroy(_currentGrid.gameObject);

        Vector3 worldPos = new Vector3(data.Position.x, data.Position.y, 0);
        _currentGrid = Instantiate(data.GridMap, worldPos, Quaternion.identity);
    }
}
