using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Weapons
{
    public class BulletBase : NetworkBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.GetMask("Player"))
            {
                Debug.Log("Hit Player");
            }
        }
    }
}
