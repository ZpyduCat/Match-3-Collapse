using System.Collections.Generic;
using UnityEngine;

public class TileObjectsControl
{
    public List<TileObject> activeTileObjects { private set; get; }
    private List<TileObject> tileObjectPrefabs;
    private Dictionary<int, List<TileObject>> poolTileObj;


    public TileObjectsControl(List<TileObject> tileObjectPrefabs)
    {
        this.tileObjectPrefabs = tileObjectPrefabs;
        poolTileObj = new Dictionary<int, List<TileObject>>();
        activeTileObjects = new List<TileObject>();
    }

    public TileObject SpawnTileObj(int type, Tile tile, Transform parent = null)
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
            tileObject = Object.Instantiate(prefab, parent);
        }

        tileObject.transform.position = tile.transform.position;
        tileObject.active = true;

        activeTileObjects.Add(tileObject);
        return tileObject;
    }

    public void DespawnTileObj(TileObject tileObject)
    {
        activeTileObjects.Remove(tileObject);

        if (!poolTileObj.TryGetValue(tileObject.type, out List<TileObject> pool))
        {
            poolTileObj[tileObject.type] = new List<TileObject>();
        }

        tileObject.gameObject.SetActive(false);
        poolTileObj[tileObject.type].Add(tileObject);
    }
}
