using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapDataViewer : MonoBehaviour
{
    public TileMapData tileMapData;

    private void OnDrawGizmos()
    {
        if (tileMapData == null || tileMapData.SpawnArea == null) return;

        Gizmos.color = Color.red;

        foreach (Vector2 pos in tileMapData.SpawnArea)
        {
            Vector3 worldPos = new Vector3(pos.x, pos.y, 0f);
            Gizmos.DrawWireSphere(worldPos, 0.3f);
        }

        Gizmos.color = Color.green;
        Vector2 playerPos = tileMapData.PlayerSpawnPosition;
        Gizmos.DrawWireCube(new Vector3(playerPos.x, playerPos.y, 0f), Vector3.one * 0.5f);
    }
}
