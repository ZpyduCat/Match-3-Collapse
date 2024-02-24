using UnityEngine;
using System.Collections.Generic;

public class Field : MonoBehaviour
{
    public Tile tilePrefab;

    public int sizeX = 10;
    public int sizeY = 10;
    public int gameFieldSizeX = 10;
    public int gameFieldSizeY = 10;
    public int startGameFieldX = 0;
    public int startGameFieldY = 0;

    public float spacingX;
    public float spacingY;

    private Tile[,] tiles;

    public void Init()
    {
        tiles = new Tile[sizeX, sizeY];
        GenerateField();
    }

    private void GenerateField()
    {
        float offSetX = transform.position.x;
        float offSetY = transform.position.y;
        Vector2 spacing = Vector2.zero;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                Vector3 pos = new Vector3(x + offSetX + spacing.x, y + offSetY + spacing.y, 0f);
                tiles[x, y] = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
                tiles[x, y].Init(new Vector2Int(x, y), !IsTileOnGameField(x,y));
                spacing.y += spacingY;
            }

            spacing.x += spacingX;
            spacing.y = 0;
        }
    }


    public Tile GetTile(int x, int y)
    {
        return tiles[x, y];
    }

    public bool TryGetTile(int x, int y, out Tile tile)
    {
        tile = null;

        if(!HasTile(x,y))
            return false;

        tile = GetTile(x, y);

        return true;
    }

    public bool HasTile(int x, int y)
    {
        if (x < 0 || y < 0 || x >= sizeX || y >= sizeY)
            return false;

        return true;
    }

    public bool IsTileOnGameField(int x, int y)
    {
        if (x < startGameFieldX || y < startGameFieldY || x >= gameFieldSizeX || y >= gameFieldSizeY)
            return false;

        return true;
    }
}
