using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField]
    private Text _coins, _lives;

    [SerializeField]
    private GameObject _gameOverText;

    [SerializeField]
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Game manager is null");
        }

        _gameOverText.SetActive(false);
    }
    //updatecoins()
    public void UpdateCoins(int coins)
    {
        _coins.text = "Coins: " + coins.ToString();
    }

    public void UpdateLives(int lives)
    {
        _lives.text = "Lives: " + lives.ToString();
    }

    public void GameOverText()
    {
        if (_gameManager._isGameOver == true)
        {
            _gameOverText.SetActive(true);
        }
    }
}
