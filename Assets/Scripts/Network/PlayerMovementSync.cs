using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerMovementSync : NetworkBehaviour
{
    [SyncVar]
    private Vector2 syncPos;
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
            MyTransform.position = Vector2.Lerp(MyTransform.position, syncPos, Time.deltaTime * LerpRate);
        }
    }

    [Command]
    private void CmdProvidePositionToServer(Vector2 pos)
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
