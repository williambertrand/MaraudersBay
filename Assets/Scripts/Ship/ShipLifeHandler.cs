using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class ShipLifeHandler : MonoBehaviour {
    
    [SerializeField]int _life = 100;
    int Life{
        get { return _life;}
        set { 
            _life = value;
            if (value <= 0) Die();
        }
    }
    public int defaultLife = 100;
    public bool isInvincible;
    public string[] collidableTags;
    public GameObject[] spawnLocations;

    void Start() {
        Life = defaultLife;
    }

    public bool ApplyDamage(int damage) {
        if (isInvincible)
            return false;
        
        Life -= damage;
        return Life <= 0;
    }

    void Die() {
        Life = defaultLife;

        GameObject nearest = null;
        var distance = float.MaxValue;
        
        foreach (GameObject spawn in spawnLocations) {
            var newDis = Vector2.Distance(spawn.transform.position, transform.position);
            
            if (nearest && !(newDis < distance))
                continue;
            
            nearest = spawn;
            distance = newDis;
        }

        if (nearest)
            transform.position = nearest.transform.position;
    }
    
    float CollisionDamageCalculator(float speed, float max) {
        return math.sin(math.clamp(speed, 1, max)/15.9f) * max;
    }
    
    void OnTriggerEnter(Collider collision) {
        if (collidableTags.Contains(collision.gameObject.tag))
            Life -= (int)CollisionDamageCalculator(2, 25);
    }
}
