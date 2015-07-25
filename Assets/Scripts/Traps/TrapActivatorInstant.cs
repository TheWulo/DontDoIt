using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class TrapActivatorInstant : TrapActivatorProximity
    {
        protected override void OnActivatorTrigger()
        {
            ActivateTrap();
        }
    }
}
