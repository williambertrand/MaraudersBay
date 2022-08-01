using UnityEngine;

public class GoldPickup : MonoBehaviour
{

    public int amount;
    public bool shouldFloat;

    //Speed of pickup floating up/down
    [SerializeField] private float speed = 5f;
    //adjust this to change how high the pickup floats
    [SerializeField] private float height = 0.5f;
    [SerializeField] private float startHeight = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldFloat) return;

        Vector3 pos = transform.position;
        float newY = Mathf.Sin(Time.time * speed) + startHeight;
        transform.position = new Vector3(pos.x, newY, pos.z) * height;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInventory inventory = other.gameObject.GetComponent<PlayerInventory>();
            if (inventory != null) inventory.OnGoldCollect(amount);
            Destroy(gameObject);
        }
    }
}
