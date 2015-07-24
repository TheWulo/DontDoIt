

using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class TrapActivatorDelay : TrapActivator
    {
        [Header("Trigger with Delay")] 
        public float TimeOfDelay;

        private bool isActivating;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isActivating) return;
            //if (other.tag != player) return;

            StartCoroutine("StartAfterTimeCoroutine");
            isActivating = true;
        }

        private IEnumerator StartAfterTimeCoroutine()
        {
            yield return new WaitForSeconds(TimeOfDelay);
            targetTrap.ActivateTrap();
            isActivating = false;
            yield return null;
        }
    }
}
