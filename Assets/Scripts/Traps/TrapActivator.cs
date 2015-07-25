using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class TrapActivator : MonoBehaviour
    {
        [Header("Trigger Base")]
        [SerializeField] protected Collider2D triggerArea;
        [SerializeField] private TrapBase targetTrap;
        [SerializeField] private Animator animator;

        protected bool isActive;

        private void Start()
        {
            if (triggerArea == null)
            {
                triggerArea = GetComponent<Collider2D>();
            }
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }

        protected void SetActivatorAsTriggered()
        {
            if (!isActive)
            {
                animator.SetBool("active", true);
                OnActivatorTrigger();
                isActive = true;
            }
        }

        protected void SetActivatorAsNotTriggered()
        {
            if (isActive)
            {
                animator.SetBool("active", false);
                isActive = false;
            }
            
        }

        protected virtual void OnActivatorTrigger()
        {
            
        }
        
        protected void ActivateTrap()
        {
            targetTrap.ActivateTrap();
        }
    }
}
