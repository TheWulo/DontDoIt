using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField]
        private float bulletSpawningDistanceOffset;
        [SerializeField]
        private Vector2 bulletSpawningDirection;
        [SerializeField] 
        private GameObject bulletPrefab;
        [SerializeField] 
        private float firePower;

        public void Shoot()
        {
            Vector3 finalSpawnPosition = new Vector3(gameObject.transform.position.x + bulletSpawningDirection.x * bulletSpawningDistanceOffset,
                                                        gameObject.transform.position.y + bulletSpawningDirection.y * bulletSpawningDistanceOffset,
                                                        gameObject.transform.position.z);
            GameObject go = Instantiate(bulletPrefab, finalSpawnPosition, bulletPrefab.transform.rotation) as GameObject;
            go.GetComponent<Rigidbody2D>().AddForce(bulletSpawningDirection*firePower);
        }

        public void Update()
        {
            //QQ
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                bulletSpawningDirection = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                bulletSpawningDirection = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                bulletSpawningDirection = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                bulletSpawningDirection = Vector2.left;
            }
            //QQ

            if (Input.GetKeyDown(KeyCode.C))
            {
                Shoot();
            }
        }
    }
}
