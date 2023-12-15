using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class DestructionBlock : MonoBehaviour
{
    [SerializeField] private float marge = 0.01f;
    private Tilemap destructTilemap;

    private void Start()
    {
        destructTilemap = GetComponent<Tilemap>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogError(1);
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.LogError(2);
            Vector3 HitPosition = Vector3.zero;
/*            foreach (ContactPoint2D hit in collision.contacts)
            {*/
             HitPosition.x = collision.transform.position.x- marge * collision.transform.position.normalized.x;
             HitPosition.y = collision.transform.position.y - marge * collision.transform.position.normalized.y;
             destructTilemap.SetTile(destructTilemap.WorldToCell(HitPosition), null);
            /*
                        }*/
            Invoke(null,0.5f);
            GetComponent<ShadowCaster2DCreator>().DestroyOldShadowCasters();
            GetComponent<ShadowCaster2DCreator>().Create();
        }
    }
    private void CollisionEnter2D(Collision2D collision)
    {
        Debug.LogError(1);
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.LogError(2);
            Vector3 HitPosition = Vector3.zero;
            foreach(ContactPoint2D hit in collision.contacts)
            {
                HitPosition.x = hit.point.x - marge * hit.normal.x; 
                HitPosition.y = hit.point.y - marge * hit.normal.y;
                destructTilemap.SetTile(destructTilemap.WorldToCell(HitPosition), null);
                
            }
        }
    }
}
