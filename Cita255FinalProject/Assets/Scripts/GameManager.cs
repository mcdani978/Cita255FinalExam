using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    // Singleton pattern (1: Singleton)
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    // Delegate and event (2: Delegate & Event)
    public delegate void CollectibleCollectedHandler();
    public event CollectibleCollectedHandler OnAllCollectiblesCollected;

    // List to track collectibles (3: List)
    private List<GameObject> collectibles;

    // Property to get collectible count (4: Properties)
    public int CollectibleCount
    {
        get { return collectibles.Count; }
    }

  
    [SerializeField]
    private TextMeshProUGUI collectibleMessage;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    private void Start()
    {
        
        collectibles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Collectible"));

        
        if (collectibleMessage != null)
        {
            collectibleMessage.text = "";
        }
    }

    public void CollectibleCollected(GameObject collectible)
    {
        // Decision statement (5: Decision Statement)
        if (collectibles.Contains(collectible))
        {
            collectibles.Remove(collectible);
            Debug.Log($"Collectible removed! Remaining: {CollectibleCount}");

            if (CollectibleCount == 0)
            {
                Debug.Log("All collectibles collected!");
                OnAllCollectiblesCollected?.Invoke();

                
                if (collectibleMessage != null)
                {
                    collectibleMessage.text = "Congratulations! You collected all the items!";
                }
            }
        }

        //6 and 7 Object Oriented Programming And Scriptable Objects
    }
}
