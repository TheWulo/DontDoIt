using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class TrapActivator : MonoBehaviour
    {
        [Header("Trigger Base")]
        [SerializeField] 
        protected Collider2D triggerArea;
        [SerializeField] 
        protected TrapBase targetTrap;

        private void Start()
        {
            if (triggerArea == null)
                triggerArea = GetComponent<Collider2D>();
        }

        protected void ActivateTrap()
        {
            targetTrap.ActivateTrap();
        }
    }
}
