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
    private Random random = new System.Random();


    void Start()
    {
        if (isServer)
            PillsOnScene[0].gameObject.SetActive(false);
    }

    public void Collected()
    {
        if (!isServer) return;
        PillsOnScene.ForEach(x => x.gameObject.SetActive(false));
        StartCoroutine(WaitCooldown());
    }

    private IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        PillsOnScene[random.Next(0, 2)].gameObject.SetActive(true);
    }
}
