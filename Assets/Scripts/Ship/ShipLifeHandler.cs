using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class ShipLifeHandler : MonoBehaviour 
{
    [SerializeField] private int life = 100;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] string[] collidableTags; 
    public bool ApplyDamage(int damage) {
        if (isInvincible)
            return false;
        
        life -= damage;
        return life <= 0;
    }

    float CollisionDamageCalculator(float speed, float max) {
        return math.sin(math.clamp(speed, 1, max)/15.9f) * max;
    }
    
    void OnTriggerEnter(Collider collision) {
        if (collidableTags.Contains(collision.gameObject.tag))
            life -= (int)CollisionDamageCalculator(2, 25);
    }
}
