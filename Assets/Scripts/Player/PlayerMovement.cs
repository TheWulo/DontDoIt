using System.Linq;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 10)] public float MaxMoveSpeed;
    [Range(0, 10)] public float JumpPower;
    [Range(0, 1)] public float JumpTimeInSeconds;
    public KeyCode JumpKey;
    public GameObject[] TerrainCheckers;

    private new Rigidbody2D rigidbody;
    private bool jumping;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(JumpKey) && !jumping)
        {
            var standOnGround = TerrainCheckers.Any(o => Physics2D.OverlapPoint(o.transform.position, LayerMask.GetMask("Ground")));
            if (standOnGround)
            {
                StartCoroutine(JumpCoroutine(JumpTimeInSeconds));
            }
        }
    }

    IEnumerator JumpCoroutine(float jumpTime)
    {
        jumping = true;
        var jumpStart = Time.time;
        while (jumpStart + jumpTime > Time.time)
        {
            rigidbody.velocity = new Vector2(0, JumpPower);
            yield return null;
        }
        jumping = false;
    }

    void FixedUpdate ()
	{
        transform.position += transform.right * Input.GetAxis("Horizontal") * MaxMoveSpeed * Time.fixedDeltaTime;
	}
}
