using UnityEngine;

[RequireComponent(typeof(ShipLifeHandler))]
public class Enemy : MonoBehaviour
{

    public int id;
    public int health;
    public ShipLifeHandler enemyLife;

    // Amount of gold to drop on death
    public int goldReward;
    public int reputationAward;

    // Start is called before the first frame update
    void Start()
    {
        enemyLife = GetComponent<ShipLifeHandler>();
        enemyLife.maxLife = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDeath(GameObject actor)
    {
        EnemyManager.Instance.OnEnemyDeath(id, actor, transform.position);
        EffectsManager.Instance.ShipSinkEffectAt(new Vector3(transform.position.x, 15.0f, transform.position.z));
    }
}
