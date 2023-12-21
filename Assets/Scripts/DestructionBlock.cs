using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class DestructionBlock : MonoBehaviour
{
    [SerializeField] private float marge = 0.01f;
    private Tilemap destructTilemap;
    private int destroyedTiled = 0;

    private void Start()
    {
        destructTilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Vector3 HitPosition = Vector3.zero;
            HitPosition.x = collision.gameObject.transform.position.x - marge * collision.gameObject.transform.position.normalized.x;
            HitPosition.y = collision.gameObject.transform.position.y - marge * collision.gameObject.transform.position.normalized.y;
            Vector3Int Cell = destructTilemap.WorldToCell(new Vector3(HitPosition.x, HitPosition.y, HitPosition.z));
            for (int x = -1; x <1; x++)
                for (int y = -1; y < 1; y++)
                    destructTilemap.SetTile(new Vector3Int(Cell.x + x, Cell.y + y, Cell.z), null);            
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Vector3 HitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                HitPosition.x = hit.point.x - marge * hit.normal.x;
                HitPosition.y = hit.point.y - marge * hit.normal.y;
                for(int x = -1; x <=2; x++)
                    for(int y = -1; y <1; y++)
                        destructTilemap.SetTile(destructTilemap.WorldToCell(new Vector3(HitPosition.x+2.56f*x, HitPosition.y + 2.56f * y, HitPosition.z)), null);
            }
        }
    }
}
