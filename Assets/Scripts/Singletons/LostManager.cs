using UnityEngine;
using UnityEngine.Serialization;

public class LostManager : MonoBehaviour {
    public static LostManager Instance { get; private set; }
    
    [SerializeField] GameObject lostText;

    void Awake()
    {
        // Singleton enforcement
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    void Start()
    {
        lostText.SetActive(false);
    }
    
    public void ShowLostText()
    {
        lostText.SetActive(true);
    }
}
