using UnityEngine;
using UnityEngine.UI;

public class ShipHealthBar : MonoBehaviour
{

    [SerializeField] private Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetMaxHeath(int h)
    {
        healthSlider.maxValue = h;
    }

    public void SetValue(int v)
    {
        healthSlider.value = v;
    }
}
