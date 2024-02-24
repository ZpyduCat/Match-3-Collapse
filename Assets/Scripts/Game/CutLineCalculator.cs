using System.Collections.Generic;
using UnityEngine;

public class CutLineCalculator
{
    private Field field;
    private Vector2Int[] directions = new Vector2Int[4]
    {
        Vector2Int.left,
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down
    };

    public CutLineCalculator(Field field)
    {
        this.field = field;
    }

    //Поиск рядостоящих объектов по типу
    public List<Vector2Int> GetLinesToCut(Vector2Int startPos, int type)
    {
        int loop = field.gameFieldSizeX * field.gameFieldSizeY;

        List<Vector2Int> cutLineToCut = new List<Vector2Int>();
        List<Vector2Int> openPosition = new List<Vector2Int>();
        List<Vector2Int> closedPosition = new List<Vector2Int>();
        openPosition.Add(startPos);

        while(openPosition.Count > 0 && loop > 0)
        {
            Vector2Int pos = openPosition[openPosition.Count - 1];
            openPosition.RemoveAt(openPosition.Count - 1);
            closedPosition.Add(pos);

            if (field.TryGetTile(pos.x, pos.y, out Tile tile) && tile.tileObj != null && tile.tileObj.type == type && tile.tileObj.active)
                cutLineToCut.Add(pos);
            else
                continue;

            foreach (var dir in directions)
            {
                Vector2Int navPos = pos + dir;
                if (closedPosition.Contains(navPos))
                    continue;

                if(!openPosition.Contains(navPos) && field.IsTileOnGameField(navPos.x, navPos.y))
                    openPosition.Add(navPos);
            }

            loop--;
        }

        cutLineToCut.Reverse();

        return cutLineToCut;
    }

}
