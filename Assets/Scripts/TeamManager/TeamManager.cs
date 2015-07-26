using System;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[Serializable]
public class TeamData
{
    public Team Team { get; private set; }
    public int PlayerCount { get; set; }
    public int Score { get; private set; }

    public TeamData(Team team)
    {
        Team = team;
    }

    public void AddPoint()
    {
        Score++;
    }

}

public class TeamManager : NetworkBehaviour
{
    #region old
    public NetworkClientWrapper NetworkClientWrapper;
    public TeamData Suicidals { get; set; }
    public TeamData Rescuers { get; set; }

    public int MaxScore = 10;
    public int SecondsToRescuersWin;
    #endregion

    [SyncVar]
    public int SuicidersScore;
    [SyncVar]
    public int RescuersScore;
    [SyncVar]
    public int SuicidersCount;
    [SyncVar]
    public int RescuersCount;
    [SyncVar]
    public float SecondsToGameEnd;

    public static TeamManager instance;

    public List<GameObject> RescuersSkins;
    public List<GameObject> SuecidersSkins;


    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Suicidals = new TeamData(Team.Suicidials);
        Rescuers = new TeamData(Team.Rescuers);
        if (isServer)
            SecondsToGameEnd = SecondsToRescuersWin;
    }

    void Update()
    {
        if (isServer)
        {
            var newTime = SecondsToGameEnd - Time.deltaTime;
            SecondsToGameEnd = newTime < 0 ? 0 : newTime;
            if (SecondsToGameEnd <= 0)
            {
                NetworkClientWrapper.LoadRescuersWon();
                return;
            }
            if (SuicidersScore >= MaxScore)
            {
                NetworkClientWrapper.LoadSuicidersWon();
                return;
            }
        }
    }

    #region Server Side Server Logic
    [Command]
    public void CmdAddPlayerForSuicidas()
    {
        SuicidersCount++;
    }

    [Command]
    public void CmdAddPlayerForRescuers()
    {
        RescuersCount++;
    }


    [Command]
    public void CmdAddScoreForSuicidas(int points)
    {
        SuicidersScore += points;
    }

    [Command]
    public void CmdAddScoreForRescuers(int points)
    {
        RescuersScore += points;
    }
    #endregion

    //private void OnPlayerDeath(PlayerBase player, DeathReason type)
    //{
    //    if (player.Team == Team.Suicidials && type == DeathReason.Trap)
    //    {
    //        Suicidals.AddPoint();
    //    }
    //}

    public Team GetAvailableTeam()
    {
        return RescuersCount <= SuicidersCount ? Team.Rescuers : Team.Suicidials;
    }

    public GameObject GetRandomRescuerSkin()
    {
        int i = UnityEngine.Random.Range(0, RescuersSkins.Count);
        return RescuersSkins[i];
    }

    public GameObject GetRandomSueciderSkin()
    {
        int i = UnityEngine.Random.Range(0, SuecidersSkins.Count);
        return SuecidersSkins[i];
    }


}
