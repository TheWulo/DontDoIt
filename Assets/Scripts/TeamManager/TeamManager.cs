using System;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;
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

public class TeamManager : MonoBehaviour
{
    public TeamData Suicidals { get; set; }
    public TeamData Rescuers { get; set; }

    public int MaxScore = 10;
    private bool isMaster;

    public static TeamManager instance;
    

    public void Awake()
    {
        instance = this;
    }

	void Start ()
	{
	    isMaster = true; //TODO
        Suicidals = new TeamData(Team.Suicidials);
        Rescuers = new TeamData(Team.Rescuers);
	}

    public void AddScore(Team team)
    {
        if (team == Team.Suicidials)
        {
            Suicidals.AddPoint();
        }
        else
        {
            Rescuers.AddPoint();
        }
    }

    public Team Register(PlayerBase playerBase)
    {
        TeamData teamData = GetAvailableTeam();
        teamData.PlayerCount++;
        playerBase.OnDeath += OnPlayerDeath;
        Debug.Log("Registered player at " + teamData.Team + " side.");
        return teamData.Team;
    }

    private void OnPlayerDeath(PlayerBase player, DeathReason type)
    {
        if (player.Team == Team.Suicidials && type == DeathReason.Trap)
        {
            Suicidals.AddPoint();
        }
    }

    private TeamData GetAvailableTeam()
    {
        return Rescuers.PlayerCount <= Suicidals.PlayerCount ? Rescuers : Suicidals;
    }
}
