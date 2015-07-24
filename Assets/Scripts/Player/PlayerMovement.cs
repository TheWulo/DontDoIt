using System.Linq;
using UnityEngine;
using System.Collections;
<<<<<<< HEAD
=======
using JetBrains.Annotations;
>>>>>>> e232dfca8edd703666e329712a4b33163475329e

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 1000)] public float RunningSpeed = 100f;
<<<<<<< HEAD
    [Range(0, 10000)] public float JumpPower = 100f;
=======
    [Range(0, 1000)] public float JumpPower = 100f;
>>>>>>> e232dfca8edd703666e329712a4b33163475329e
    [Range(0, 1)] public float HorizontalDrag = 0f;
    [Range(0, 1)] public float VerticalDrag = 0f;
    public float[] JumpPowerFrames = new float[] { 1f, .9f, .75f, .6f, .4f, .2f };
    public KeyCode JumpKey;
<<<<<<< HEAD
=======
    public KeyCode JumpKeyController;
>>>>>>> e232dfca8edd703666e329712a4b33163475329e
    public GameObject[] TerrainCheckers;

    private new Rigidbody2D rigidbody;
    private int jumpFrame = 0;
    private bool jumping = false;

    public void ResetMovement() 
    {
        rigidbody.velocity = new Vector2(0f, 0f);
        jumping = false;
    }

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate ()
	{
        rigidbody.velocity = new Vector2(rigidbody.velocity.x * HorizontalDrag, rigidbody.velocity.y * VerticalDrag);

        if (Input.GetKey(JumpKey) || Input.GetKey(JumpKeyController) && !jumping) {
            var standOnGround = TerrainCheckers.Any(o => Physics2D.OverlapPoint(o.transform.position, LayerMask.GetMask("Ground")));
            if (standOnGround) {
                jumping = true;
                jumpFrame = 0;
            }
        }

        if (jumping) {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpPower * JumpPowerFrames[jumpFrame] * Time.fixedDeltaTime);
            jumpFrame++;
            if (jumpFrame >= JumpPowerFrames.Length) {
                jumping = false;
                jumpFrame = 0;
            }
        }

        rigidbody.velocity = new Vector2(rigidbody.velocity.x + Input.GetAxis("Horizontal") * RunningSpeed * Time.fixedDeltaTime, rigidbody.velocity.y);

	}
<<<<<<< HEAD
=======
    
>>>>>>> e232dfca8edd703666e329712a4b33163475329e
}
