using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class DemoTest : MonoBehaviour
{
    public Pro_RuleTile rule;
    public Dictionary<Vector2Int, GameObject> WorldTiles = new Dictionary<Vector2Int, GameObject>();
    public bool TileMap = false;
    public Tilemap Tilemap;
    private void Start()
    {
        rule.init("terrian");
    }

    private void Update()
    {
        if (!TileMap)
        {
            setBlock();
            removeBlock();
        }
        else
        {
            SetBlockTile();
            removeBlockTile();
        }
    }

    void setBlock()
    {
        if (Input.GetButton("Fire1"))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.FloorToInt(worldPos.x);
            int y = Mathf.FloorToInt(worldPos.y);
            Vector2Int pos = new Vector2Int(x, y);

            if (!rule.ContainsTile(pos))
            {
                PlaceTiles(pos, rule.GetTile(x, y));
                rule.AddTile(pos);
                RefreshTiles(pos);
            }
        }
    }

    void PlaceTiles(Vector2Int pos, Sprite sp)
    {
        var spiritGO = new GameObject("Spirit");
        WorldTiles.Add(pos, spiritGO);
        var sr = spiritGO.AddComponent<SpriteRenderer>();
        sr.sprite = sp;
        spiritGO.transform.position = new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);

    }

    void removeBlock()
    {
        if (Input.GetButton("Fire2"))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.FloorToInt(worldPos.x);
            int y = Mathf.FloorToInt(worldPos.y);
            Vector2Int pos = new Vector2Int(x, y);

            if (WorldTiles.ContainsKey(pos))
            {
                Destroy(WorldTiles[pos]);
                WorldTiles.Remove(pos);
                rule.RemoveTile(pos);
                RefreshTiles(pos);
            }
        }
    }

    void RefreshTiles(Vector2Int pos)
    {
        Vector2Int temp = new Vector2Int(0, 0);
        for (int i = 0; i < 8; i++)
        {
            temp = pos + rule.Directions[i];
            if (WorldTiles.ContainsKey(temp))
            {
                WorldTiles[temp].GetComponent<SpriteRenderer>().sprite = rule.GetTile(temp.x, temp.y);
            }
        }
    }

    //=================================TileMap============================
    void SetBlockTile()
    {
        if (Input.GetButton("Fire1"))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.FloorToInt(worldPos.x);
            int y = Mathf.FloorToInt(worldPos.y);
            Vector2Int pos = new Vector2Int(x, y);

            if (!Tilemap.HasTile(new Vector3Int(x, y, 0)))
            {
                Tilemap.SetTile(new Vector3Int(x, y, 0), rule.GetTileBase(x, y));
                rule.AddTile(pos);
                RefreshTilesBase(pos);
            }
        }
    }

    void removeBlockTile()
    {
        if (Input.GetButton("Fire2"))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.FloorToInt(worldPos.x);
            int y = Mathf.FloorToInt(worldPos.y);
            Vector2Int pos = new Vector2Int(x, y);

            if (Tilemap.HasTile(new Vector3Int(x, y, 0)))
            {
                Tilemap.SetTile(new Vector3Int(x, y, 0), null);
                rule.RemoveTile(pos);
                RefreshTilesBase(pos);
            }
        }
    }

    void RefreshTilesBase(Vector2Int pos)
    {
        Vector2Int temp = new Vector2Int(0, 0);
        for (int i = 0; i < 8; i++)
        {
            temp = pos + rule.Directions[i];
            if (Tilemap.HasTile(new Vector3Int(temp.x, temp.y, 0)))
            {
                Tilemap.SetTile(new Vector3Int(temp.x, temp.y, 0), rule.GetTileBase(temp.x, temp.y));
            }
        }
    }
}
