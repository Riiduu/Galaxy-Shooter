using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;

    // Id's for powerups
    // O - triple shot
    // 1 - speed
    // 2 - shields

    [SerializeField]
    private int _powerID;

    // Powerup pick-up audio component
    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        // move down at a speed of 3
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // destroy when out of screen

        if (transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }

    // on trigger collision (by player only) change triple laser mode for the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.transform.GetComponent<Player>();

        AudioSource.PlayClipAtPoint(_clip, transform.position);

        if (collision.tag == "Player")
        {
            Debug.Log("Powerup Collected");
            // start the tripleshotpowerup coroutine

            if (player != null)
            {
                switch (_powerID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        break;
                }

            }

            
            Destroy(this.gameObject);
        }
    }
}
