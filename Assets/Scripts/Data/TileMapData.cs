using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New TileMap Data", menuName = "ScriptableObjects/TileMapData")]
public class TileMapData : ScriptableObject
{
    [SerializeField] private Vector2Int position;
    public Vector2Int Position
    {
        get { return position; }
    }

    [SerializeField] private Grid gridMap;
    public Grid GridMap
    {
        get { return gridMap; }
    }

    [SerializeField] private List<Vector2> spawnArea;
    public List<Vector2> SpawnArea
    {
        get { return spawnArea; }
    }

    [SerializeField] private Vector2 playerSpawnPosition;
    public Vector2 PlayerSpawnPosition
    {
        get { return playerSpawnPosition; }
    }
}