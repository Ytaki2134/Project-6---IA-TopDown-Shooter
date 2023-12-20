using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRadius : MonoBehaviour
{
    private float _damage;
    private List<Collider2D> _colliders = new();

/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Hull>() != null)
            _colliders.Add(collision);
    }
*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(_colliders.Count);
        if (collision.gameObject.GetComponent<Hull>() != null)
            if (!_colliders.Contains(collision))
                _colliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Hull>() != null)
            if (_colliders.Contains(collision))
                _colliders.Remove(collision);
    }

    public void Explode()
    {
        if (_colliders != null)
            foreach (var collider in _colliders)
                collider.gameObject.GetComponent<Hull>().RemoveHealth(_damage);
        Destroy(gameObject);
    }
    public void SetDamage(float damage)
    {
        _damage = damage;
    }
}
