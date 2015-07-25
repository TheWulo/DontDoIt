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
        public float TrapKillingTime = 1;
        [SerializeField]
        protected Animator trapAnimation;
        protected List<PlayerBase> playersInsideTrap = new List<PlayerBase>();
        private float lastActivationTime = -100;
        
        private void Start()
        {
            if (triggerArea == null)
            {
                triggerArea = GetComponent<Collider2D>();
            }
            if (trapAnimation == null)
            {
                trapAnimation = GetComponentInChildren<Animator>();
            }
        }

        void Update()
        {
            if (lastActivationTime + TrapKillingTime > Time.time)
            {
                for (int i = 0; i < playersInsideTrap.Count; i++)
                {
                    playersInsideTrap[0].Die(DeathReason.TrapSpike);
                }
                playersInsideTrap.Clear();
            }
        }

        public void ActivateTrap()
        {
            trapAnimation.Play("Activate");
            lastActivationTime = Time.time;
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
