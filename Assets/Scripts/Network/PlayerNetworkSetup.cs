using UnityEngine.Networking;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Player
{
    public class PlayerNetworkSetup : NetworkBehaviour
    {
        private PlayerBase targetPlayer;

        private bool SkinSetUp;

        void Start()
        {
            if (!isLocalPlayer)
            {
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerWeapon>().enabled = false;
            }
            targetPlayer = gameObject.GetComponent<PlayerBase>();
        }

        void Update()
        {
            if (targetPlayer.Team != Team.None && !SkinSetUp)
            {
                targetPlayer.SetSkin();
                SkinSetUp = true;
            }
        }
    }
}
