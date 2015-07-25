using Assets.Scripts.Traps;
using UnityEngine;
using System.Collections;

public class TrapActivatorProximity : TrapActivator
{
    private int playersCountOnActivator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        playersCountOnActivator++;
        SetActivatorAsTriggered();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playersCountOnActivator--;
        if (playersCountOnActivator == 0)
        {
            SetActivatorAsNotTriggered();
        }
    }
}
