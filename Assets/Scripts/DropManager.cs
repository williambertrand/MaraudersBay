using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{

    [SerializeField] GameObject chestDrop;
    #region Singleton
    public static DropManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    public void SpawnChestAt(Vector3 pos)
    {
        Instantiate(chestDrop, pos, Quaternion.identity);
    }
}
