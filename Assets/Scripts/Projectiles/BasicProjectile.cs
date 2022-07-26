using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{

    // Destroy after ball has lived this long
    [SerializeField] private float? TTL;

    // Destroy after ball has sunk below this line
    [SerializeField] private float MIN_DEPTH = -5;
    
    [SerializeField] private int Damage = 25;
    [SerializeField] string[] validTargets;
    
    private float createdAt;

    // Start is called before the first frame update
    void Start()
    {
        createdAt = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (TTL != null)
        {
            if (Time.time - createdAt >= TTL)
            {
                Destroy(gameObject);
            }
        }

        if (transform.position.y <= MIN_DEPTH)
        {
            Destroy(gameObject);
        }
    }

    // TODO: On colliding with enemy ship, deal damage
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

        } else if (collision.gameObject.CompareTag("Water"))
        {
            EffectsManager.Instance.SplashAt(collision.transform.position);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision) {
        if (!validTargets.Contains(collision.gameObject.tag))
            return;
        
        ShipLifeHandler lifeHandler = collision.gameObject.GetComponent<ShipLifeHandler>();
        
        if (lifeHandler == null)
            return;
        
        lifeHandler.ApplyDamage(Damage);
    }
}
