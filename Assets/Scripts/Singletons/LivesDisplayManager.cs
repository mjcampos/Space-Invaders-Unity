using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LivesDisplayManager : MonoBehaviour {
    public static LivesDisplayManager Instance { get; private set; }
    
    [SerializeField] GameObject livesSpritePrefab;
    [SerializeField] Transform livesSpriteContainer;

    List<GameObject> _lives = new List<GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    public void SetLivesDisplay(int livesAmount)
    {
        for (int i = 0; i < livesAmount; i++)
        {
            GameObject livesSpriteInstance = Instantiate(livesSpritePrefab, livesSpriteContainer);
            
            _lives.Add(livesSpriteInstance);
        }
    }
    
    public void RemoveLife(int livesAmount)
    {
        for (int i = 0; i < _lives.Count; i++)
        {
            GameObject life = _lives[i];

            if (life.activeSelf)
            {
                life.SetActive(false);
                break;
            }
        }

        if (livesAmount < 1)
        {
            NoMoreLives();
        }
    }

    public void NoMoreLives()
    {
        StartCoroutine(DelayBeforeNotification());
    }

    IEnumerator DelayBeforeNotification()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.PlayerLost();
    }
}
