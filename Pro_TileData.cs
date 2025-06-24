using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Pro_TileData
{
    public string name;
    public enum Condition
    {
        Any,  // always true (no condition)  //blue
        ThisTile, // something must be here //green
        NotThisTile // something must not be here //red
    }

    

    public Pro_Directions directions;
    public List<Sprite> tiles = new List<Sprite>();


    public int Count => tiles.Count;
    public Sprite GetTileAsset()
    {
        if(tiles.Count == 0)
        {
            throw new Exception("TileData has no tiles");
        }
        int idx = UnityEngine.Random.Range(0, tiles.Count);
        return tiles[idx];
    }
    
}
