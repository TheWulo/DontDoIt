using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class TrapBase : MonoBehaviour
    {
        [Header("Trap Base")]
        [SerializeField]
        protected Collider2D triggerArea;
        [SerializeField]
        protected Animator animation;
        protected List<PlayerBase> playersInsideTrap = new List<PlayerBase>();
        
        private void Start()
        {
            if (triggerArea == null)
            {
                triggerArea = GetComponent<Collider2D>();
            }
            if (animation == null)
            {
                animation = GetComponent<Animator>();
            }
        }

        public void ActivateTrap()
        {
            
            for (int i = 0; i < playersInsideTrap.Count; i++)
            {
                playersInsideTrap[0].Die(DeathReason.Trap);
            }
        }

        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<PlayerBase>();
            if (player)
            {
                playersInsideTrap.Add(player);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var player = other.GetComponent<PlayerBase>();
            if (player && playersInsideTrap.Contains(player))
            {
                playersInsideTrap.Remove(player);
            }
        }
        
    }
}
