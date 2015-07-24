using System.Linq;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 10)] public float MaxMoveSpeed;
    [Range(0, 10)] public float JumpPower;
    [Range(0, 1)] public float JumpCooldown;
    public GameObject TerrainCheckerLeft;
    public GameObject TerrainCheckerRight;

    private new Rigidbody2D rigidbody;
    private float lastJumpTime;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lastJumpTime = Time.time;
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") > 0 && lastJumpTime + JumpCooldown < Time.time)
        {
            var standOnGround = Physics2D.OverlapArea(TerrainCheckerLeft.transform.position, TerrainCheckerRight.transform.position, LayerMask.GetMask("Ground"));
            if (standOnGround != null)
            {
                rigidbody.AddForce(new Vector2(0, JumpPower));
            }
        }
    }

    void FixedUpdate ()
	{
        transform.position += transform.right * Input.GetAxis("Horizontal") * MaxMoveSpeed * Time.fixedDeltaTime;
	}
}
