
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamageEffects : MonoBehaviour
{

    [Serializable] public struct DamageEffect
    {
        public float cutoff;
        public GameObject effect;
    }

    [SerializeField] private List<DamageEffect> effects;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].effect.SetActive(false);
        }
    }

    public void UpdateEffects(float currentHealthPercent)
    {
        for(int  i = 0; i < effects.Count; i++)
        {
            DamageEffect e = effects[i];
            if(currentHealthPercent <= e.cutoff)
            {
                e.effect.SetActive(true);
            } else
            {
                e.effect.SetActive(false);
            }
        }
    }
}
