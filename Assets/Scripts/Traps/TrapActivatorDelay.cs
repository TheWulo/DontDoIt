

using System.Collections;
using Assets.Scripts.Audio;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class TrapActivatorDelay : TrapActivatorProximity
    {
        [Header("Trigger with Delay")] 
        public float TimeOfDelay;

        private bool isActivating;

        protected override void OnActivatorTrigger()
        {
            if (isActivating) return;

            isActivating = true;
            AudioManager.instance.PlayRandomDoItSond();
            StartCoroutine(StartAfterTimeCoroutine());
        }

        private IEnumerator StartAfterTimeCoroutine()
        {
            yield return new WaitForSeconds(TimeOfDelay);
            ActivateTrap();
            isActivating = false;
        }
    }
}
