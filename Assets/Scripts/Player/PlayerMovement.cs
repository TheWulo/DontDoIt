using System.Linq;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 10)] public float MaxMoveSpeed;
    [Range(0, 10)] public float JumpPower;
    [Range(0, 1)] public float JumpCooldown;
    public KeyCode JumpKey;
    public GameObject[] TerrainCheckers;

    private new Rigidbody2D rigidbody;
    private float lastJumpTime;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lastJumpTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKey(JumpKey) && lastJumpTime + JumpCooldown < Time.time)
        {
            var standOnGround = TerrainCheckers.Any(o => Physics2D.OverlapPoint(o.transform.position, LayerMask.GetMask("Ground")));
            if (standOnGround)
            {
                lastJumpTime = Time.time;
                rigidbody.velocity = new Vector2(0, JumpPower);
            }
        }
    }

    void FixedUpdate ()
	{
        transform.position += transform.right * Input.GetAxis("Horizontal") * MaxMoveSpeed * Time.fixedDeltaTime;
	}
}
