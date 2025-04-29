using System;
using UnityEngine;

public class LivesDisplayManager : MonoBehaviour {
    [SerializeField] GameObject[] lives;
    
    public static LivesDisplayManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }
    
    public void RemoveLife()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            GameObject life = lives[i];

            if (life.activeSelf)
            {
                life.SetActive(false);
                return;
            }
        }
    }
}
