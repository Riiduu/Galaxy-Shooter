using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // Enemy
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    // Powerup Array
    [SerializeField]
    private GameObject[] powerups;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemyRoutine();
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);

        // while loop (inifite)
        while (_stopSpawning == false)
        {
            // Instantiate enemy prefab
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9f, 9f), 10, 0), Quaternion.identity);

            newEnemy.transform.parent = _enemyContainer.transform;
            // yield wait for 5 seconds
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(2.0f);


        while (true)
        {
            Instantiate(powerups[Random.Range(0,3)], new Vector3(Random.Range(-9f, 9f), 10, 0), Quaternion.identity);

            // wait 3-7 seconds
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
