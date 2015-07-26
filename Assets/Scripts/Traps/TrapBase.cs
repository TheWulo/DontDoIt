using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Traps
{
    public class TrapBase : NetworkBehaviour
    {
        [Header("Trap Base")]
        [SerializeField]
        protected Collider2D triggerArea;
        [SerializeField]
        protected Animator trapAnimation;
        protected List<PlayerBase> playersInsideTrap = new List<PlayerBase>();
        protected float lastActivationTime = -100;
        
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

        public void ActivateTrap()
        {
            trapAnimation.Play("Activate");
            lastActivationTime = Time.time;
        }
        
    }
}
