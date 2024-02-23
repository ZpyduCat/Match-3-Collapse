using UnityEngine;
using UnityEngine.EventSystems;

public delegate void OnTileObjClickHandler (TileObject tileObject);

public class TileObject : MonoBehaviour, IPointerClickHandler
{
    public event OnTileObjClickHandler OnTileObjClickEvent;
    public int type => _type;
    public Tile tile;
    [SerializeField] private int _type;
    [SerializeField] private float speed = 5f;

    void Update()
    {
        if (tile == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, tile.transform.position, Time.deltaTime*speed);
    }

    public void ChangeTile(Tile tile)
    {
        if(this.tile != null)
        {
            this.tile.tileObj = null;
        }

        this.tile = tile;
        if (this.tile != null)
        {
            this.tile.tileObj = this;
            //transform.position = tile.transform.position;
        }
    }

    public void EnableObj()
    {

    }

    public void DisableObj()
    {
        ChangeTile(null);
    }

    //
    public void Explode()
    {
        DisableObj();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnTileObjClickEvent?.Invoke(this);
    }

}
