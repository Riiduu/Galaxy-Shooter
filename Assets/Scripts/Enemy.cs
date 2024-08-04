using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();    
    }

    // Update is called once per frame
    void Update()
    {
        // move down 4m/s
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if: bottom of screen - respawn at top
        // respawn at top with a new random x position

        if (transform.position.y <= -3.5)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f), 10, 0);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if collide with player

        if (other.tag == "Player")
        { 

            // null-check & damage the player
            if (_player != null)
            {
                _player.Damage();
            } else
            {
                Debug.Log("Player null");
            }

            // self-destruct
            Destroy(this.gameObject);
        }
        
        

        // if collide with laser
        if (other.tag == "Laser")
        {
            if (_player != null)
            {
                _player.AddScore(10);
            } else
            {
                Debug.Log("Player null");
            }
            // destroy laser
            Destroy(other.gameObject);
            // self-destruct
            // add 10 to score
            Destroy(this.gameObject);
        }
        
    }
}
