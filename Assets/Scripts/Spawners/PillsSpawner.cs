using UnityEngine;
using System.Collections;
using Assets.Scripts.Traps;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;
using Random = UnityEngine.Random;

public class PillsSpawner : NetworkBehaviour
{
    [SerializeField]
    protected List<Transform> PillsPositions;
    [SerializeField]
    protected GameObject Prefab;
    [SerializeField]
    protected float Cooldown;
    //[SyncVar(hook = "OnPillCollectedChange")]
    public bool PillCollected;
    //[SyncVar(hook = "OnActivePillChange")]
    public int ActivePill;

    void Start()
    {
        if (isServer)
        {
            SpawnPill();
        }
    }

    private void SpawnPill()
    {
        var count = PillsPositions.Count;
        var index = Random.Range(0, count);
        var newPill = (GameObject)Instantiate(Prefab, PillsPositions[index].position, Prefab.transform.rotation);
        NetworkServer.Spawn(newPill);
    }

    public void CollectPill(TrapPills pill)
    {
        pill.CmdOrderDestroy();
        if (isServer)
        {
            StartCoroutine(WaitCooldown());
        }
    }

    private IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        if (isServer)
        {
            SpawnPill();
        }

    }
}
