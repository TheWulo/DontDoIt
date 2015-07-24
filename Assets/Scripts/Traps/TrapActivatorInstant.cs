using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class TrapActivatorInstant : TrapActivator
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            //if (other.tag != player) return;

            ActivateTrap();
        }
    }
}
