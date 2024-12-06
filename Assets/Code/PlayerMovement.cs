using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveAcceleration;
    public float JumpAcceleration;
    public float MaxLinearVelocity;

    private bool _onGround;

    private Rigidbody _rb;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.maxLinearVelocity = MaxLinearVelocity;
    }

    // Update is called once per frame
    private void Update()
    {
        var moveDelta = MoveAcceleration * Time.deltaTime;
        moveDelta *= _onGround ? 1f : 0.1f;
        MoveAlongAxis(KeyCode.W, KeyCode.S, transform.forward, moveDelta);
        MoveAlongAxis(KeyCode.D, KeyCode.A, transform.right, moveDelta);

        var jumpDelta = JumpAcceleration * Time.deltaTime;
        if (_onGround && MoveAlongAxis(KeyCode.Space, null, transform.up, jumpDelta))
            _onGround = false;
    }

    private void OnCollisionStay(Collision other)
    {
        if (CollidedWithGround(other))
            _onGround = true;
    }

    private bool MoveAlongAxis(KeyCode forwardKey, KeyCode? backwardKey, Vector3 moveDirection, float moveDelta)
    {
        var forwardKeyDown = Input.GetKey(forwardKey);
        var backwardKeyDown = backwardKey is not null && Input.GetKey(backwardKey.Value);
        if (forwardKeyDown is false && backwardKeyDown is false)
            return false;

        var moveAmount = forwardKeyDown ? moveDelta : 0;
        moveAmount -= backwardKeyDown ? moveDelta : 0;
        _rb.AddForce(moveAmount * moveDirection, ForceMode.Impulse);
        return true;
    }

    private bool CollidedWithGround(Collision other)
    {
        return other.gameObject.TryGetComponent<Ground>(out var groundComponent);
    }
}