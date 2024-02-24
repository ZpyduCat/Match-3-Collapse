using UnityEngine;
using UnityEngine.EventSystems;

public delegate void OnTileObjClickHandler (TileObject tileObject);

public class TileObject : MonoBehaviour, IPointerClickHandler
{
    public event OnTileObjClickHandler OnTileObjClickEvent;

    public int type => _type;
    public Tile tile;
    public bool active { set; get; }

    [SerializeField] private int _type;
    [SerializeField] private float speed = 5f;
    [SerializeField] private AudioClip[] cutSounds;
    [SerializeField] private Color objectColor;
    private bool isMoving;

    //Обработка перемещения объекта
    void Update()
    {
        if (tile == null || !isMoving)
            return;

        transform.position = Vector3.MoveTowards(transform.position, tile.transform.position, Time.deltaTime*speed);

        if (Vector3.Distance(transform.position, tile.transform.position) == 0)
            isMoving = false;
    }

    //Изменения объекта на котором находится Tile
    public void ChangeTile(Tile tile)
    {
        if(this.tile != null)
        {
            this.tile.ChangeTileObj(null);
        }

        this.tile = tile;
        if (this.tile != null)
        {
            this.tile.ChangeTileObj(this);
            isMoving = true;
        }

    }
    public void DisableObj()
    {
        ChangeTile(null);
    }

    //"Взрыв" объекта
    public void Explode()
    {
        AudioManager.Singletone.gameSoundsAudioSource.PlayOneShot(cutSounds[Random.Range(0, cutSounds.Length - 1)]);
        ParticleExplode.Singletone.EmitParcticle(transform.position, objectColor);
        DisableObj();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnTileObjClickEvent?.Invoke(this);
    }

}
