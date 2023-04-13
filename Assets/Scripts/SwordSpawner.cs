using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpawner : MonoBehaviour
{
    [SerializeField] private float swordSpeed;
    [SerializeField] private float minTimeBetweenSpawns = 0.2f;
    [SerializeField] private float maxTimeBetweenSpawns = 2f;
    private float timeBetweenSpawns = 1f;
    public SwordController swordPrefab;
    private float timeSinceLastSpawn;
    void Start()
    {
        timeSinceLastSpawn = 0;
    }
    
    void FixedUpdate()
    {
        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            var newSword = Instantiate(swordPrefab) as SwordController;
            float angle = Random.Range(-90, 90);
            
            newSword.transform.position = transform.position;
            newSword.SetRotation(angle);
            newSword.rb.velocity = Vector3.back * swordSpeed;
            timeSinceLastSpawn = 0;
            timeBetweenSpawns = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        }
        timeSinceLastSpawn += Time.fixedDeltaTime;
    }
}
