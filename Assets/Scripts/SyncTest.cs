//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Networking;
//using System.Collections;

//public class SyncTest : NetworkBehaviour
//{
//    [SyncVar]
//    public Text ScoreText;
//    public void AddToScore(int val)
//    {
//        ChangeScore();
//    }


//    [ClientCallback]
//    private void ChangeScore()
//    {
//        if (isLocalPlayer)
//            CmdChangeScore(1);
//    }

//    #region Server Side
//    [Command]
//    private void CmdChangeScore(int val)
//    {
//        ScoreText.text += val;
//    }
//    #endregion
//}
