using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Player
{
    public class PlayerWeapon : NetworkBehaviour
    {
        [SerializeField]
        private float bulletSpawningDistanceOffset;        
        [SerializeField] 
        private GameObject bulletPrefab;
        [SerializeField] 
        private float firePower;
        [SerializeField]
        private float lifeTimeInSeconds;

        private Vector2 bulletSpawningDirection;

        public void Shoot()
        {
            if (!isLocalPlayer) return;
            Vector3 finalSpawnPosition = new Vector3(gameObject.transform.position.x + bulletSpawningDirection.x * bulletSpawningDistanceOffset,
                                                        gameObject.transform.position.y + bulletSpawningDirection.y * bulletSpawningDistanceOffset,
                                                        gameObject.transform.position.z);
            OrderBulletSpawn(finalSpawnPosition, bulletSpawningDirection);
        }

        public void Update()
        { 
            //QQ
            if (Input.GetAxis("Horizontal") >= 0.25f || Input.GetAxis("Horizontal") <= -0.25f || Input.GetAxis("Vertical") >= 0.25f || Input.GetAxis("Vertical") <= -0.25f)
            {
                bulletSpawningDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
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

            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                Shoot();
            }
        }

        [Command]
        private void CmdSpawnBullet(Vector3 finalSpawnPosition, Vector2 dir)
        {
            GameObject go = Instantiate(bulletPrefab, finalSpawnPosition, bulletPrefab.transform.rotation) as GameObject;
            var bul = go.GetComponent<Net>();
            bul.bulletSpawningDirection = dir;
            bul.firePower = firePower;
            Destroy(go, lifeTimeInSeconds);
            NetworkServer.Spawn(go);
        }

        [ClientCallback]
        private void OrderBulletSpawn(Vector3 pos, Vector2 dir)
        {
            if (isLocalPlayer)
                CmdSpawnBullet(pos, dir);
        }
    }
}
