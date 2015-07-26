using UnityEngine;
using System.Collections;
using Assets.Scripts.Traps;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;
using Random = System.Random;

public class PillsSpawner : NetworkBehaviour
{
    [SerializeField]
    protected List<TrapPills> PillsOnScene;
    [SerializeField]
    protected float Cooldown;
    [SyncVar(hook = "OnPillCollectedChange")]
    public bool PillCollected;
    [SyncVar(hook = "OnActivePillChange")]
    public int ActivePill;

    private Random random = new System.Random();


    void Start()
    {
        ActivePill = 0;
        if (isServer)
        {
            PillsOnScene[0].gameObject.SetActive(false);
        }
    }

    public void OnPillCollectedChange(bool newVal)
    {
        PillsOnScene.ForEach(x => x.gameObject.SetActive(false));
        StartCoroutine(WaitCooldown());
    }

    private IEnumerator WaitCooldown()
    {
        ActivePill = -1;
        yield return new WaitForSeconds(Cooldown);
        if (isServer)
        {
            ActivePill = random.Next(0, 2);
            PillCollected = false;
        }

    }
    public void OnActivePillChange(int activePill)
    {
        if (activePill != -1)
            PillsOnScene[activePill].gameObject.SetActive(true);
    }

}
