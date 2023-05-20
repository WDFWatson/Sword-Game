using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpawner : MonoBehaviour
{
    [SerializeField] private float swordSpeed = 2f;
    [SerializeField] private float minTimeBetweenSpawns = 0.2f;
    [SerializeField] private float maxTimeBetweenSpawns = 2f;
    [SerializeField] private float minTimeBetweenSpawnsMinimum = 0.1f;
    [SerializeField] private float maxTimeBetweenSpawnsMinimum = 0.1f;
    [SerializeField] private float timeBetweenRateIncreases = 5f;
    [SerializeField] private float timeChangeMaximum = 0.01f;
    [SerializeField] private float timeChangeMinimum = 0.01f;
    private float timeBetweenSpawns = 1f;
    public SwordController swordPrefab;
    
    private float timeSinceLastSpawn;
    private float timeSinceLastIncrease;
    private float currentMinTimeBetweenSpawns;
    private float currentMaxTimeBetweenSpawns;
    void Start()
    {
        timeSinceLastIncrease = 0;
        timeSinceLastSpawn = 0;
        currentMaxTimeBetweenSpawns = maxTimeBetweenSpawns;
        currentMinTimeBetweenSpawns = minTimeBetweenSpawns;
    }
    
    void FixedUpdate()
    {
        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            var newSword = Instantiate(swordPrefab);
            float angle = Random.Range(-90, 90);
            
            newSword.transform.position = transform.position;
            newSword.SetRotation(angle);
            newSword.rb.velocity = Vector3.back * swordSpeed;
            timeSinceLastSpawn = 0;
            timeBetweenSpawns = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        }

        if (timeSinceLastIncrease > timeBetweenRateIncreases)
        {
            timeSinceLastIncrease = 0;
            minTimeBetweenSpawns -= timeChangeMaximum;
            if (minTimeBetweenSpawns < minTimeBetweenSpawnsMinimum)
            {
                minTimeBetweenSpawns = minTimeBetweenSpawnsMinimum;
            }
            maxTimeBetweenSpawns -= timeChangeMaximum;
            if (maxTimeBetweenSpawns < maxTimeBetweenSpawnsMinimum)
            {
                maxTimeBetweenSpawns = maxTimeBetweenSpawnsMinimum;
            }
        }
        timeSinceLastSpawn += Time.fixedDeltaTime;
        timeSinceLastIncrease += Time.fixedDeltaTime;
    }
}
