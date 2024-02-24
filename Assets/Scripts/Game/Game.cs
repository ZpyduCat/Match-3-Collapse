using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Player player { private set; get; }
    [SerializeField] private List<TileObject> tileObjectPrefabs;
    [SerializeField] private Field field;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private float destroyDelay = 1f;
    [Space]
    private CutLineCalculator cutLineCalculator;
    private TileObjectsControl tileObjectsControl;



    public void Init()
    {
        cutLineCalculator = new CutLineCalculator(field);
        player = new Player();
        tileObjectsControl = new TileObjectsControl(tileObjectPrefabs);
    }

    public void StartGame()
    {
        GenerateField();
    }

    public void GameOver()
    {
        gameOverUI.Show(player);
    }

    public void GenerateField()
    {
        for (int x = 0; x < field.sizeX; x++)
        {
            for (int y = 0; y < field.sizeY; y++)
            {
                Tile tile = field.GetTile(x, y);
                if (tile.tileObj != null)
                    continue;

                TileObject tileObject = tileObjectsControl.SpawnTileObj(tileObjectPrefabs[Random.Range(0, tileObjectPrefabs.Count)].type, tile, field.transform);

                tileObject.OnTileObjClickEvent += OnTileObjClick;

                tileObject.ChangeTile(tile);
            }
        }
    }

    public void Recalculate()
    {
        for (int x = 0; x < field.sizeX; x++)
        {
            Queue<int> voidIndexs = new Queue<int>();

            for (int y = 0; y < field.sizeY; y++)
            {
                TileObject tileObject = field.GetTile(x, y).tileObj;
                if (tileObject == null)
                {
                    voidIndexs.Enqueue(y);
                }
                else if (tileObject != null && voidIndexs.Count > 0)
                {
                    tileObject.ChangeTile(field.GetTile(x, voidIndexs.Dequeue()));
                    voidIndexs.Enqueue(y);
                }

            }
        }
    }

    private void OnTileObjClick(TileObject tileObject)
    {
        CutLine(tileObject.tile.pos);
    }

    private void CutLine(Vector2Int startPos)
    {
        TileObject startTileObject = field.GetTile(startPos.x, startPos.y).tileObj;

        List<Vector2Int> linesToCut = cutLineCalculator.GetLinesToCut(startPos, startTileObject.type);
        List<TileObject> linesToDestroy = new List<TileObject>();

        foreach (var pos in linesToCut)
        {
            Tile tile = field.GetTile(pos.x, pos.y);
            TileObject tileObject = tile.tileObj;

            tileObject.active = false;
            tileObject.OnTileObjClickEvent -= OnTileObjClick;
            linesToDestroy.Add(tileObject);
        }

        StartCoroutine(CutLineDestroy(linesToDestroy));
       
        player.AddScore(linesToCut.Count);
        player.AddHealt(linesToCut.Count);
        player.RemoveHealt(player.GetTapPrice());
        player.AddTurn();

        if(player.GetHealt() <= 0)
        {
            GameOver();
        }
    }

    private IEnumerator CutLineDestroy(List<TileObject> tilesToDestroy)
    {
        float timeDelay = destroyDelay / tilesToDestroy.Count;
        while (tilesToDestroy.Count > 0)
        {
            TileObject tileObject = tilesToDestroy[tilesToDestroy.Count - 1];
            tileObject.Explode();
            tileObjectsControl.DespawnTileObj(tileObject);

            tilesToDestroy.RemoveAt(tilesToDestroy.Count - 1);
            yield return new WaitForSeconds(timeDelay);
        }

        Recalculate();
        GenerateField();
    }
}
