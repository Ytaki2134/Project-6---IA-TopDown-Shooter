using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float timeBeforeExplosion;
    private float _time, _passTime = 0;
    [SerializeField]private GameObject RadiusExplosion;
    [SerializeField] float _damage;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RadiusExplosion.GetComponent<ExplosionRadius>().Explode();
        Destroy(gameObject);
    }
    private void Start()
    {
        _time = Time.deltaTime;
        RadiusExplosion.GetComponent<ExplosionRadius>().SetDamage(_damage);
    }
    private void Update()
    {

        if (_passTime - _time > timeBeforeExplosion)
        {
            Debug.LogWarning("Boum");
            RadiusExplosion.GetComponent<ExplosionRadius>().Explode();
            Destroy( gameObject );
        }
        else
            _passTime += Time.deltaTime;
    }
    public void SetRadius(float newRadius)
    {
        RadiusExplosion.GetComponent<CircleCollider2D>().radius = newRadius;
    }
}
