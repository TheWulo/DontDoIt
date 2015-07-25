using UnityEngine.Networking;
using System.Collections;


namespace Assets.Scripts.Player
{
    public class PlayerNetworkSetup : NetworkBehaviour
    {
        void Start()
        {
            if (!isLocalPlayer)
            {
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerWeapon>().enabled = false;
            }

        }
    }
}
