using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Player player { private set; get; }
    [SerializeField] private List<TileObject> tileObjectPrefabs;
    [SerializeField] private Field field;
    [SerializeField] private GameOverUI gameOverUI;
    [Space]
    [SerializeField] private AudioClip[] cutSounds;
    private Dictionary<int, List<TileObject>> poolTileObj;
    private CutLineCalculator cutLineCalculator;
    

    public void Init()
    {
        poolTileObj = new Dictionary<int, List<TileObject>>();
        cutLineCalculator = new CutLineCalculator(field);
        player = new Player();
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

                TileObject tileObject = SpawnTileObj(tileObjectPrefabs[Random.Range(0, tileObjectPrefabs.Count)].type, field.transform);
                tileObject.transform.position = tile.transform.position;

                tileObject.ChangeTile(tile);
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

        foreach (var pos in linesToCut)
        {
            Tile tile = field.GetTile(pos.x, pos.y);
            TileObject tileObject = tile.tileObj;
            tileObject.Explode();
            DespawnTileObj(tileObject);
        }

        player.AddScore(linesToCut.Count);
        player.AddHealt(linesToCut.Count);
        player.RemoveHealt(player.GetTapPrice());
        player.AddTurn();

        AudioManager.Singletone.gameSoundsAudioSource.PlayOneShot(cutSounds[Random.Range(0, cutSounds.Length-1)]);

        if(player.GetHealt() <= 0)
        {
            GameOver();
        }

        field.Recalculate();
        GenerateField();
    }

    private TileObject SpawnTileObj(int type, Transform parent)
    {
        TileObject tileObject = null;
        if (poolTileObj.TryGetValue(type, out List<TileObject> pool) && pool.Count > 0)
        {
            //Debug.Log("get object from pool: " + type);
            tileObject = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);

            tileObject.transform.SetParent(parent);
            tileObject.gameObject.SetActive(true);
        }
        else
        {
            //Debug.Log("spawn object: " + type);
            TileObject prefab = tileObjectPrefabs.Find(x => x.type == type);
            tileObject = Instantiate(prefab, parent);
        }

        tileObject.OnTileObjClickEvent += OnTileObjClick;
        return tileObject;
    }

    private void DespawnTileObj(TileObject tileObject)
    {
        tileObject.OnTileObjClickEvent -= OnTileObjClick;


        if (!poolTileObj.TryGetValue(tileObject.type, out List<TileObject> pool))
        {
            poolTileObj[tileObject.type] = new List<TileObject>();
        }

        tileObject.gameObject.SetActive(false);
        poolTileObj[tileObject.type].Add(tileObject);
    }
}
