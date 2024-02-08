using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JumpingWithRollerball : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float JumpForce = 10f;
    public int score = 0;
    public float MoveSpeed = 1f;
    public float GravityModifier = 1f;
    public bool IsOnGround = true;
    public float OutOfBounds = -10f;
    float horizontalInput;
    private bool _isAtCheckpoint = false;
    private bool _checkpointPosition = false;
    private Vector3 _startingPosition;
    private Vector3 _checkpointposition;
    float verticalInput;

    private Rigidbody _playerRb;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;
        _startingPosition = transform.position;
        scoreText.text = "Score" + score.ToString();
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
        if(_isAtCheckpoint)
        {
        transform.position = _checkpointposition;
        }
        else
        {
         transform.position = _startingPosition;
        }
    }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }

        if(collision.gameObject.CompareTag("Dead zone"))
        {
            if(_isAtCheckpoint)
            {
                transform.position = _checkpointposition;
            }
            else
            {
                transform.position = _startingPosition;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Checkpoint"))
    {
        _isAtCheckpoint = true;
        _checkpointposition = other.gameObject.transform.position;
    }
    if(other.gameObject.CompareTag("Endpoint"))
    {
        transform.position = _startingPosition;
    }

    if(other.gameObject.CompareTag("Collectible"))
    {
        score++;
        scoreText.text = "Score" + score.ToString();
        Destroy(other.gameObject);
    }
    }
    
}