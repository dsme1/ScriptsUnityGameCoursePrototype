using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private UImanager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is null.");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UImanager>();
        if (_uiManager == null)
        {
            Debug.LogError("UImanager is null.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            _player.CoinsUpdate();
        }
    }
}
