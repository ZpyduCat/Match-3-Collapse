using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileObject tileObj { private set; get; }
    public Vector2Int pos { private set; get; }
    [SerializeField] private GameObject visualOjb;
    private bool isHidden;

    public void Init(Vector2Int pos, bool isHidden = false)
    {
        this.pos = pos;
        this.isHidden = isHidden;

        if (isHidden)
            visualOjb.SetActive(false);
    }

    public void ChangeTileObj(TileObject tileObj)
    {
        this.tileObj = tileObj;
    }
}
