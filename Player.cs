using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float _gravity = 1.0f;

    [SerializeField]
    private float _jumpHeight = 15.0f;

    [SerializeField]
    private float _yVelocity;

    [SerializeField]
    private int _coins;

    [SerializeField]
    private UImanager _uiManager;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private GameObject _camera;

    private bool _doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UImanager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is null");
        }

        _uiManager.UpdateLives(_lives);

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        PlayerLives();
    }

    //Player movement method
    void PlayerMovement()
    {
        //Gets input axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //Sets direction based on input axis
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        //Sets velocity of character
        Vector3 velocity = direction * _speed;
        
        //Applies gravity to player and jumping mechanism
        if (_controller.isGrounded == true)
        {            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _doubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _doubleJump == true)
            {
                _yVelocity += _jumpHeight;
                _doubleJump = false;
            }
            _yVelocity -= _gravity;
        }

        //Moves player based on direction input
        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    //Updates coins when picked up by player
    public void CoinsUpdate()
    {
        _coins++;
        Debug.LogError(_coins.ToString());
        _uiManager.UpdateCoins(_coins);
    }

    //life system
    private void PlayerLives()
    {
        if (transform.position.y <= -25.0f)
        {
            _lives--;
            transform.position = new Vector3(-8.73f, 2.96f, 0);
            _uiManager.UpdateLives(_lives);

            if (_lives < 1)
            {
                _camera.transform.SetParent(GameObject.Find("Platform (2)").GetComponent<Transform>());
                _gameManager.IsGameOver();
                _uiManager.GameOverText();
                Destroy(this.gameObject);
            }
        }
    }
}
