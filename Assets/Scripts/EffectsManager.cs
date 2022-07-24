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
        GameObject splash =Instantiate(SplashEffect, pos, Quaternion.identity);
        splash.GetComponent<ParticleSystem>().Play();
    }
}
