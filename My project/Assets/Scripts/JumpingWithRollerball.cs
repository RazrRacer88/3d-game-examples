using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingWithRollerball : MonoBehaviour
{
    public float JumpForce = 10f;
    public float MoveSpeed = 1f;
    public float GravityModifier = 1f;
    public bool IsOnGround = true;
    public float OutOfBounds = -10f;
    float horizontalInput;
    private Vector3 _startingPosition;
    float verticalInput;

    private Rigidbody _playerRb;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;
        _startingPosition = transform.position;
    }

   

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);
        _playerRb.AddForce(movement * MoveSpeed);

        if(transform.position.y < OutOfBounds)
    {
        transform.position = _startingPosition;
    }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Checkpoint"))
    {
        _startingPosition = other.gameObject.transform.position;
    }
    }
    
}