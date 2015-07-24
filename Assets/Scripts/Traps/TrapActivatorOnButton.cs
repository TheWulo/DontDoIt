
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class TrapActivatorOnButton : TrapActivator
    {
        public void OnAction()
        {
            targetTrap.ActivateTrap();
        }
    }
}
