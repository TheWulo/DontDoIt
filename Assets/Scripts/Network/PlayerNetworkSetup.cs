using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkSetup : NetworkBehaviour
{
    void Start()
    {
        if(!isLocalPlayer)
        {
            GetComponent<PlayerMovement>().enabled = false;

        }

    }
}
