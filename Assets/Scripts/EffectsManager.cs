using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{

    #region Singleton
    public static EffectsManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    public GameObject SplashEffect;

    public List<GameObject> explosions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SplashAt(Vector3 pos)
    {
        GameObject splash = Instantiate(SplashEffect, pos, Quaternion.identity);
        splash.transform.localScale = Vector3.one * Random.Range(1.5f, 3.0f);
    }

    public void ExplosionAt(Vector3 pos)
    {
        GameObject explosionEffect = explosions[Random.Range(0, explosions.Count)];
        GameObject exp = Instantiate(explosionEffect, pos, Quaternion.identity);
        exp.transform.localScale = Vector3.one * Random.Range(1.5f, 3.0f);
    }
}
