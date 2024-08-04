using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Debug.Log("Hit: " + other.transform.name);

        //if collide with player

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            // null-check & damage the player
            if (player != null)
            {
                player.Damage();
            }
            // self-destruct
            Destroy(this.gameObject);
        }
        
        

        // if collide with laser
        if (other.tag == "Laser")
        {
            // destroy laser
            Destroy(other.gameObject);
            // self-destruct
            Destroy(this.gameObject);
        }
        
    }
}
