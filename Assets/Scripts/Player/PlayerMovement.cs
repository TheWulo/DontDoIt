using System.Linq;
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
public class PlayerMovement : MonoBehaviour
{
    [Range(0, 1000)] public float RunningSpeed = 100f;
    [Range(0, 10000)] public float JumpPower = 100f;
    [Range(0, 1)] public float HorizontalDrag = 0f;
    [Range(0, 1)] public float VerticalDrag = 0f;
    [Range(0, 100)] public float Gravity = 40;
    public float[] JumpPowerFrames = new float[] { 1f, .9f, .75f, .6f, .4f, .2f };
    public KeyCode JumpKey;
    public KeyCode JumpKeyController;
    public GameObject[] TerrainCheckers;
	public GameObject[] CeilingCheckers;

    public bool touchingGround;

    private new Rigidbody2D rigidbody;
    private int jumpFrame = 0;
    private bool jumping = false;
	private bool shortJump = false;
	public bool alive = true;
	private int lastPressedJump = 1<<20;
	private int lastOnGround = 1<<20;
	private int seqPressedJump = 0;

    public void ResetMovement() 
    {
        rigidbody.velocity = new Vector2(0f, 0f);
        jumping = false;
		lastPressedJump = 1<<20;
		lastOnGround = 1<<20;
		seqPressedJump = 0;
    }

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate ()
	{
		//add custom drag
        rigidbody.velocity = new Vector2(rigidbody.velocity.x * HorizontalDrag, rigidbody.velocity.y * VerticalDrag);

		//add custom gravity
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y - Gravity * Time.fixedDeltaTime);

        var touchGround = TerrainCheckers.Any(o => Physics2D.OverlapPoint(o.transform.position, LayerMask.GetMask("Ground") | LayerMask.GetMask("Player")));
        var touchCeiling = CeilingCheckers.Any(o => Physics2D.OverlapPoint(o.transform.position, LayerMask.GetMask("Ground") | LayerMask.GetMask("Player")));
		bool pressedJump = alive && (Input.GetKey(JumpKey) || Input.GetKey(JumpKeyController));
		
        touchingGround = touchGround;
		
		lastOnGround = touchGround ? 0 : lastOnGround + 1;
		lastPressedJump = pressedJump ? 0 : lastPressedJump + 1;
		seqPressedJump = pressedJump ? seqPressedJump + 1 : 0;
		
		//process jumping
        if (lastPressedJump <= 2 && seqPressedJump <= 3 && !jumping && lastOnGround <= 2) {
			jumping = true;
			jumpFrame = 0;
        }
		
		if (jumping && !pressedJump && jumpFrame <= 6) {
			shortJump = true;
		}
		
		if (shortJump && jumpFrame == 6) {
			shortJump = false;
			jumping = false;
			jumpFrame = 0;
			if (rigidbody.velocity.y > 0) rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
		}
			// Debug.Log("PJ: " + pressedJump);
		// Debug.Log("LPJ: " + lastPressedJump);
		// Debug.Log("J: " + jumping);
		// Debug.Log("TG: " + touchGround);

        if (jumping) {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpPower * JumpPowerFrames[jumpFrame] * Time.fixedDeltaTime);
            jumpFrame++;
            if (jumpFrame >= JumpPowerFrames.Length) {
                jumping = false;
                jumpFrame = 0;
            }
        }

        rigidbody.velocity = new Vector2(rigidbody.velocity.x + (alive ? Input.GetAxis("Horizontal") : 0) * RunningSpeed * Time.fixedDeltaTime, rigidbody.velocity.y);

	}
}
