using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class ShipLifeHandler : MonoBehaviour {
    
    [SerializeField]int _life = 100;
    int Life{
        get { return _life;}
        set { 
            _life = value;
        }
    }


    public int maxLife = 100;
    public bool isInvincible;
    public string[] collidableTags;
    public GameObject[] spawnLocations;


    [Tooltip("Set this for player health to show in UI")]
    [SerializeField] private ShipHealthBar healthBar;

    private ShipMovement movement;

    void Start() {
        Life = maxLife;
        if(healthBar != null)
        {
            healthBar.SetMaxHeath(maxLife);
        }
        UpdateUI();

        movement = GetComponent<ShipMovement>();
    }

    public void ApplyDamage(int damage, GameObject actor) {
        if (isInvincible)
            return;

        Debug.Log(gameObject.name + " taking " + damage + " damage from: " + actor.name);

        Life -= damage;
        UpdateUI();
        if (Life <= 0)
        {
            Die(actor);
        }
    }

    void Die(GameObject fromActor) {

        Enemy enemyComponent = GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.OnDeath(fromActor);
        }

        // TODO: Initiate spawn sequence / cut scene here
        Life = maxLife;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collidableTags.Contains(collision.gameObject.tag))
        {
            if (movement != null)
            {
                ApplyDamage((int)CollisionDamageCalculator(movement.GetSpeedSqr(), 10), null);
            }
        }
    }


    private void UpdateUI()
    {
        if (healthBar != null)
        {
            healthBar.SetValue(Life);
        }
    }
}
