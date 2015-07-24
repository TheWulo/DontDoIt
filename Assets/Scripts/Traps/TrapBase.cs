using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class TrapBase : MonoBehaviour
    {
        [Header("Trap Base")]
        [SerializeField]
        protected Collider2D triggerArea;

        protected bool isPlayerIn;

        private void Start()
        {
            if (triggerArea == null)
                triggerArea = GetComponent<Collider2D>();
        }
        
        //protected PlayerBase playerInsideTrap;

        public void ActivateTrap()
        {
            if (isPlayerIn)
            {
                //KILL PLAYER
            }
            Debug.Log("Trap Activated!");
        }

        public void SetUpTrap(/*PlayerBase playerInTrap*/)
        {
            if (isPlayerIn) return;

            isPlayerIn = true;
            //playerInsideTrap = playerInTrap;
            Debug.Log("Trap SetUp!");
        }

        public void UnsetUpTrap()
        {
            isPlayerIn = false;
            //playerInsideTrap = null;
            Debug.Log("Trap UnsetUp!");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //if (other.tag != player) return;

            SetUpTrap(/*other as PlayerBase*/);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag != "Player") return;
            //if (other.transform != playerInsideTrap.transform) return;

            UnsetUpTrap();
        }
        
    }
}
