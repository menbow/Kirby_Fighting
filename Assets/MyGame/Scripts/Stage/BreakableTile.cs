using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTile : MonoBehaviour
{
    public Tilemap tilemap;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void TileDestroy(Vector3 destroyPos)
    {
        Vector3Int pos = Vector3Int.CeilToInt(destroyPos);

        TileBase destroyTile = tilemap.GetTile(pos);

        //Debug.Log(destroyTile);

        if (tilemap.HasTile(pos))
        {
            Debug.Log("adwefewfgelj");
            tilemap.SetTile(pos, null);
        }

    }

     //if (tile != null && tilemap.GetTileFlags(cellPosition) == TileFlags.None)
     //       {
     //           GameObject tileObject = tilemap.GetInstantiatedObject(cellPosition);
     //           if (tileObject != null && tileObject.CompareTag("CanBroke"))
     //           {
     //               tilemap.SetTile(cellPosition, null);
     //           }
     //       }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Atack"))
        {
            Debug.Log("aaaa");
            TileDestroy(collision.transform.position);
        }

    }

}
