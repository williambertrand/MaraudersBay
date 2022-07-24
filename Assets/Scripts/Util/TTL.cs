using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTL : MonoBehaviour
{

    private float createdAt;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        createdAt = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - createdAt >= duration)
        {
            Destroy(gameObject);
        }
    }
}
