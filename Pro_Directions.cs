using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pro_Directions 
{
    public Pro_TileData.Condition up;
    public Pro_TileData.Condition up_Right;
    public Pro_TileData.Condition right;
    public Pro_TileData.Condition down_right;
    public Pro_TileData.Condition down;
    public Pro_TileData.Condition down_left;
    public Pro_TileData.Condition left;
    public Pro_TileData.Condition up_left;

    //over ride the indexer
    public Pro_TileData.Condition this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return up;
                case 1: return up_Right;
                case 2: return right;
                case 3: return down_right;
                case 4: return down;
                case 5: return down_left;
                case 6: return left;
                case 7: return up_left;
                default: throw new System.IndexOutOfRangeException();
            }
            
        }
    }
}
