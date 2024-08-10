using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class Asteroid : MonoBehaviour
{
    // Rotation Speed
    [SerializeField]
    private float _rotationSpeed = -80f;

    [SerializeField]
    private GameObject _explosion;

    // Check if the asteroid has collided with the laser
    // Remove asteroid
    // Instantiate the explosion at the position of the asteroid

    // SpawnManager component

    SpawnManager _spawnManager;


    // Start is called before the first frame update
    void Start()
    {   
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotationSpeed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(other.gameObject);

        _spawnManager.StartSpawning();
        Destroy(this.gameObject, 0.1f);
        
    }
    
}
