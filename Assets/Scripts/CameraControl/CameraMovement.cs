using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        private PlayerBase targetPlayer;
        public float CameraMovementSpeed;

        public float MaxLeftPoint = -6.75f;
        public float MaxUpPoint = 5;
        public float MaxRightPoint = 6.5f;
        public float MaxBottomPoint = -5;

        public void SubscribePlayer(PlayerBase player)
        {
            if (targetPlayer == null)
                targetPlayer = player;
        }

        public void MoveCameraToLocation(Vector3 targetPosition)
        {
            var finalPosition = new Vector3(Mathf.Lerp(gameObject.transform.position.x, targetPosition.x, CameraMovementSpeed * Time.deltaTime),
                                                            Mathf.Lerp(gameObject.transform.position.y, targetPosition.y, CameraMovementSpeed * Time.deltaTime),
                                                            gameObject.transform.position.z);

            if (finalPosition.x < MaxLeftPoint)
            {
                finalPosition = new Vector3(MaxLeftPoint, finalPosition.y, finalPosition.z);
            }
            else if (finalPosition.x > MaxRightPoint)
            {
                finalPosition = new Vector3(MaxRightPoint, finalPosition.y, finalPosition.z);
            }

            if (finalPosition.y < MaxBottomPoint)
            {
                finalPosition = new Vector3(finalPosition.x, MaxBottomPoint, finalPosition.z);
            }
            else if (finalPosition.y > MaxUpPoint)
            {
                finalPosition = new Vector3(finalPosition.x, MaxUpPoint, finalPosition.z);
            }
            
            gameObject.transform.position = finalPosition;
        }

        private void Update()
        {
            if (targetPlayer != null)
                MoveCameraToLocation(targetPlayer.gameObject.transform.position);
        }
    }
}
