using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnLoc
{
    public string name;
    public Transform transform;
}

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    public GameObject DeathUI;
    public GameObject[] InGameUIs;
    public Player player;
    public List<SpawnLoc> spawnLocations;

    // Start is called before the first frame update
    void Start()
    {}

    public void OnPlayerDeath()
    {
        player.GetComponent<ShipMovement>().enabled = false;
        player.GetComponent<ShipFiring>().enabled = false;
        DeathUI.SetActive(true);

        foreach(GameObject g in InGameUIs)
        {
            g.SetActive(false);
        }
    }

    public void OnPlayerChooseSpawn(string locName)
    {
        Debug.Log("Spawning player at: " + locName);

        SpawnLoc loc = spawnLocations.Find(l => l.name.Equals(locName));
        player.transform.position = loc.transform.position;

        player.GetComponent<ShipLifeHandler>().Reset();
        player.GetComponent<ShipMovement>().enabled = true;
        player.GetComponent<ShipFiring>().enabled = true;

        DeathUI.SetActive(false);
        foreach (GameObject g in InGameUIs)
        {
            g.SetActive(true);
        }
    }

}
