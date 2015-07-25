using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        private PlayerBase targetPlayer;
        public float CameraMovementSpeed;

        public void SubscribePlayer(PlayerBase player)
        {
            if (targetPlayer == null)
                targetPlayer = player;
        }

        public void MoveCameraToLocation(Vector3 targetPosition)
        {
            gameObject.transform.position = new Vector3(Mathf.Lerp(gameObject.transform.position.x, targetPosition.x, CameraMovementSpeed * Time.deltaTime),
                                                            Mathf.Lerp(gameObject.transform.position.y, targetPosition.y, CameraMovementSpeed * Time.deltaTime),
                                                            gameObject.transform.position.z);
        }

        private void Update()
        {
            if (targetPlayer != null)
                MoveCameraToLocation(targetPlayer.gameObject.transform.position);
        }
    }
}
