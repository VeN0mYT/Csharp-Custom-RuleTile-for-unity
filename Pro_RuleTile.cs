using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
[CreateAssetMenu(menuName = "custom/Pro_RuleTile")]
public class Pro_RuleTile : ScriptableObject
{

    /*
     * the idea is that we represent the 8 directions as a bit mask  8 digits 00000000
     * each direction control specific digit in the bit mask
     * the 8 directions are up, up_right, right, down_right, down, down_left, left, up_left
     * so we change it to 0 or 1 , true or false based on the condetion
     * if condition is true we add 1 to the specific digit that represents the direction in the bit mask
     * the first tile mach all the condetions and get a 11111111 which is 255 bit mask we return it
     * */

    // Important call (Init) function before use it !!!!!!

    public HashSet<Vector2Int> tilesSet;
    public List<Pro_TileData> tiles = new List<Pro_TileData>();
    public Sprite defuilt;
    public Tile.ColliderType typeCollider = Tile.ColliderType.None;   // the tileBase collider type in case you use tiles from tilemap
    private string SetName;

    [HideInInspector]
    public List<Vector2Int> Directions;

    public void init(string name)
    {
        SetName = name;

        if(!Pro_CommonSet.Instance.IsContains(SetName))    //  the HashSet that containes the tiles pos in case want many rules communicate with each other
            Pro_CommonSet.Instance.AddSet(SetName);
        
        tilesSet = Pro_CommonSet.Instance.GetSet(SetName);
        
      
        // make sure the directions synic with the tiles condetion directions in Pro_Directions
        Directions = new List<Vector2Int>()
        {
            new Vector2Int(0, 1),    // up
            new Vector2Int(1, 1),    // up_right
            new Vector2Int(1, 0),    // right
            new Vector2Int(1, -1),   // down_right
            new Vector2Int(0, -1),   // down
            new Vector2Int(-1, -1),  // down_left
            new Vector2Int(-1, 0),   // left
            new Vector2Int(-1, 1),   // up_left
        };
    }

    public Sprite GetTile(int x, int y)     // if you want to use Sprite directly
    {
        Vector2Int pos = new Vector2Int(x, y);
        Vector2Int temp = new Vector2Int(0,0);
        for(int i = 0;i<tiles.Count;i++)
        {
            
            int BitMask = 0;

            for(int j = 0;j<8;j++)
            {
                temp = pos + Directions[j];
                
                if (RuleMatch(tiles[i].directions[j], temp))
                {
                    BitMask |= 1 << j;
                }
               
            }

            if (BitMask == 255)
            {
                return tiles[i].GetTileAsset();
            }

        }
        return defuilt;
    }

    public Tile GetTileBase(int x, int y,Func<Sprite,Sprite> config = null)  // if you want to use TileMap with Tiles
    {                                                                           // function to pointer if you want to edit the sprite before it go to the tilemap
        Sprite sprite = GetTile(x, y);
        if (sprite == null)
            return null;
        
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = config != null ? config(sprite) : sprite;
        tile.colliderType = typeCollider; 
    
        return tile;
    }

    public bool RuleMatch(Pro_TileData.Condition currentCondition, Vector2Int pos)
    {
        switch(currentCondition)
        {
            case Pro_TileData.Condition.Any:
                return true;
            case Pro_TileData.Condition.ThisTile:
                return tilesSet.Contains(pos);
            case Pro_TileData.Condition.NotThisTile:
                return !tilesSet.Contains(pos);
            default:
                return false;
        }
    }

    public void AddTile(Vector2Int pos)
    {
        tilesSet.Add(pos);
    }

    public void RemoveTile(Vector2Int pos)
    {
        tilesSet.Remove(pos);
    }

    public bool ContainsTile(Vector2Int pos)
    {
        return tilesSet.Contains(pos);
    }

    public void ChangeSetName(string name)
    {
        SetName = name;

        if (!Pro_CommonSet.Instance.IsContains(SetName))    
            Pro_CommonSet.Instance.AddSet(SetName);

        tilesSet = Pro_CommonSet.Instance.GetSet(SetName);
    }

    public string GetSetName()
    {
        return SetName;
    }
}
