using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public enum AnimationDeathType { DieSpike }

    public class AnimationController : MonoBehaviour
    {

        public GameObject GFXObject;
        public Animator GFXAnimator;

        public bool isDying;

        public void Update()
        {
            if (Input.GetAxis("Horizontal") >= 0.1)
            {
                if (GFXObject.gameObject.transform.localScale.x > 0)
                    GFXObject.gameObject.transform.localScale = new Vector3(GFXObject.gameObject.transform.localScale.x * -1, GFXObject.gameObject.transform.localScale.y, GFXObject.gameObject.transform.localScale.z);
            }
            if (Input.GetAxis("Horizontal") <= -0.1)
            {
                if (GFXObject.gameObject.transform.localScale.x < 0)
                    GFXObject.gameObject.transform.localScale = new Vector3(GFXObject.gameObject.transform.localScale.x * -1, GFXObject.gameObject.transform.localScale.y, GFXObject.gameObject.transform.localScale.z);
            }

            if (Input.GetAxis("Horizontal") >= 0.1 || Input.GetAxis("Horizontal") <= -0.1)
            {
                if (!isDying)
                    GFXAnimator.Play("Run");
            }
            else
            {
                if (!isDying)
                    GFXAnimator.Play("Idle");
            }
        }

        public void PlayDieAnimation(AnimationDeathType type)
        {
            isDying = true;
            GFXAnimator.Play(type.ToString());
            StartCoroutine("Respawn");
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(3);
            isDying = false;
        }
    }
}
