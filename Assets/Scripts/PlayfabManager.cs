using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager instance;

    public int CurrentScoreStored
    {
        get;
        private set;
    }

    public Action scoreEvent;
    public int CurrentMaxStreakStored
    {
        get;
        private set;
    }

    public Action maxStreakEvent;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        Login();
    }

    void Login()
    {
        var loginRequest = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(loginRequest, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Playfab Error Occured");
        Debug.Log(error.GenerateErrorReport());
    }

    public void ModifyScore(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Score",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnModifyScore, OnError);
    }
    public Action ModifyScoreEvent;
    void OnModifyScore(UpdatePlayerStatisticsResult result)
    {
        ModifyScoreEvent.Invoke();
    }
    
    public void ModifyMaxStreak(int maxStreak)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "MaxStreak",
                    Value = maxStreak
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnModifyMaxStreak, OnError);
    }

    public Action ModifyMaxStreakEvent;
    void OnModifyMaxStreak(UpdatePlayerStatisticsResult result)
    {
        ModifyMaxStreakEvent.Invoke();
    }
    public void GetScore()
    {
        var request = new GetPlayerStatisticsRequest
        {
            StatisticNames = new List<string>
            {
                "Score"
            }
        };
        PlayFabClientAPI.GetPlayerStatistics(request, OnGetScore, OnError);
    }

    void OnGetScore(GetPlayerStatisticsResult result)
    {
        CurrentScoreStored = result.Statistics[0].Value;
        scoreEvent.Invoke();
    }
    
    public void GetMaxStreak()
    {
        var request = new GetPlayerStatisticsRequest
        {
            StatisticNames = new List<string>
            {
                "MaxStreak"
            }
        };
        PlayFabClientAPI.GetPlayerStatistics(request, OnGetMaxStreak, OnError);
    }

    void OnGetMaxStreak(GetPlayerStatisticsResult result)
    {
        CurrentMaxStreakStored = result.Statistics[0].Value;
        maxStreakEvent.Invoke();
    }
}
