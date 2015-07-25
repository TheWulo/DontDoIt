using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerMovementSync : NetworkBehaviour
{
    [SyncVar]
    private Vector3 syncPos;
    [SerializeField]
    private Transform MyTransform;
    [SerializeField]
    private float LerpRate;

    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();
    }

    private void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            MyTransform.position = Vector3.Lerp(MyTransform.position, syncPos, Time.deltaTime * LerpRate);
        }
    }

    [Command]
    private void CmdProvidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [ClientCallback]
    private void TransmitPosition()
    {
        if (isLocalPlayer)
            CmdProvidePositionToServer(MyTransform.position);
    }
}
