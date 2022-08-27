using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PlayfabStatsController : MonoBehaviour
{
    public static PlayfabStatsController Instance;
    public bool isMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (PlayfabContext.isActive)
        {
            GetPlayerStatistics();
        }
    }

    public delegate void OnStatComplete();

    public void UpdatePlayerStatistic(string name, int value, OnStatComplete onComplete)
    {
        if (!PlayfabContext.isActive) return;

        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest()
            {
                Statistics = new List<StatisticUpdate>() {
                    new StatisticUpdate() {
                        StatisticName = name,
                        Value = value
                    }
                }
            },
            result => onComplete(),
            onError
        );
    }

    private void onError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        //SceneManager.LoadScene(GameScenes.Menu);
    }

    public void GetPlayerStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }


    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var stat in result.Statistics)
        {
            if (stat.StatisticName == "kills")
            {
                Debug.Log("Player ship kills:" + stat.Value);
            }
        }
    }

    public void AddCurrency(long amount, OnStatComplete onComplete)
    {
        AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest();
        request.Amount = (int)amount;
        request.VirtualCurrency = "CR";
        PlayFabClientAPI.AddUserVirtualCurrency(request,
            result => onComplete(),
            error => Debug.Log("Error adding currency: " + error.ErrorMessage)
        );
    }
}