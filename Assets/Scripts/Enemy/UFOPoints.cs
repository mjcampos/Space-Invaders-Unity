using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class UFOPoints : MonoBehaviour {
    [SerializeField] int points;
    
    void Start()
    {
        StartCoroutine(CycleUFOScore());
    }

    IEnumerator CycleUFOScore()
    {
        while (true)
        {
            points = GetRandomPoints();
            yield return new WaitForSeconds(1f);
        }
    }

    int GetRandomPoints()
    {
        int[] pointsArray = { 50, 100, 150, 200, 250, 300 };
        float[] weights = {0.25f, 0.25f, 0.20f, 0.15f, 0.10f, 0.05f};

        float totalWeight = 0f;
        
        foreach (float weight in weights)
        {
            totalWeight += weight;
        }
        
        float rand = Random.value * totalWeight;
        float cumulative = 0f;

        for (int i = 0; i < pointsArray.Length; i++)
        {
            cumulative += weights[i];

            if (rand <= cumulative)
            {
                return pointsArray[i];
            }
        }
        

        // Return 50 by default
        return 50;
    }
}
