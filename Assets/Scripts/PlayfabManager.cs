using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager instance;

    private int currentScoreStored;
    private int currentMaxStreakStored;
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
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnModifyLeaderboard, OnError);
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
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnModifyLeaderboard, OnError);
    }

    void OnModifyLeaderboard(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard Modified Successfully");
    }
    public int GetScore()
    {
        var request = new GetPlayerStatisticsRequest
        {
            StatisticNames = new List<string>
            {
                "Score"
            }
        };
        PlayFabClientAPI.GetPlayerStatistics(request, OnGetScore, OnError);
        return currentScoreStored;
    }

    void OnGetScore(GetPlayerStatisticsResult result)
    {
        currentScoreStored = result.Statistics[0].Value;
    }
    
    public int GetMaxStreak()
    {
        var request = new GetPlayerStatisticsRequest
        {
            StatisticNames = new List<string>
            {
                "MaxStreak"
            }
        };
        PlayFabClientAPI.GetPlayerStatistics(request, OnGetMaxStreak, OnError);
        return currentMaxStreakStored;
    }

    void OnGetMaxStreak(GetPlayerStatisticsResult result)
    {
        currentMaxStreakStored = result.Statistics[0].Value;
    }
}
