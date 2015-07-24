using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class BulletBase : MonoBehaviour
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
