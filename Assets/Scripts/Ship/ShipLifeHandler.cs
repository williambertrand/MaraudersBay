using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;


public class ShipLifeHandler : MonoBehaviour {
    
    [SerializeField]int _life = 100;
    int Life{
        get { return _life;}
        set {
            if (value > maxLife) value = maxLife; 
            _life = value;
            UpdateUI();
            UpdateDamageEffects();
        }
    }


    public int maxLife = 100;
    public bool isInvincible;
    public string[] collidableTags;


    [Tooltip("Set this for player health to show in UI")]
    [SerializeField] private ShipHealthBar healthBar;

    private ShipDamageEffects damageEffects;

    private ShipMovement movement;

    void Start() {
        Life = maxLife;
        if(healthBar != null)
        {
            healthBar.SetMaxHeath(maxLife);
        }
        UpdateUI();

        movement = GetComponent<ShipMovement>();
        damageEffects = GetComponent<ShipDamageEffects>();
    }

    public void ApplyDamage(int damage, GameObject actor) {
        if (isInvincible)
            return;

        Life -= damage;
        if (Life <= 0)
        {
            Die(actor);
        }
    }

    public void ApplyRepair(int amount)
    {
        Life += amount;
    }

    void Die(GameObject fromActor) {

        Enemy enemyComponent = GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.OnDeath(fromActor);
            Destroy(gameObject);
        }


        if (GetComponent<Player>() != null)
        {
            // Handle player death specific actions here
            // Players lose all on ship items when sunk
            inventorySystem.Instance.Clear();

            GameManager.Instance.OnPlayerDeath();
        }
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
                if (movement.GetSpeedSqr() < 5.0f) return;
                ApplyDamage((int)CollisionDamageCalculator(movement.GetSpeedSqr(), 20), null);
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

    private void UpdateDamageEffects()
    {
        if(damageEffects != null)
        {
            damageEffects.UpdateEffects((float) Life / maxLife);
        }
    }

    public void Reset()
    {
        Life = maxLife;
        if (healthBar != null)
        {
            healthBar.SetMaxHeath(maxLife);
        }
        UpdateUI();
    }
}
