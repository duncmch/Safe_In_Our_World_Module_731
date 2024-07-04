using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string transitionedFromScene;

    public Vector2 platformingRespawnPoint;
    public Vector2 respawnPoint;
    [SerializeField] Bench bench;
    public static GameManager Instance { get; private set; }

    [System.Obsolete]
    private void Awake()
    {
        SaveData.Instance.Initialize();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this; 
        }
        
        SaveScene();

        DontDestroyOnLoad(gameObject);
        bench = FindObjectOfType<Bench>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SaveData.Instance.SavePlayerData();
        }
    }
    public void SaveScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SaveData.Instance.sceneNames.Add(currentSceneName);
    }

    public void RespawnPlayer()
    {
        SaveData.Instance.LoadBench();

        if(SaveData.Instance.benchSceneName != null) // load the bench's scene if it exists
        {
            SceneManager.LoadScene(SaveData.Instance.benchSceneName);
        }

        if(SaveData.Instance.benchPos != null) // set the respawn point to the bench's position
        {
            respawnPoint = SaveData.Instance.benchPos;
        }
        else
        {
            respawnPoint = platformingRespawnPoint;
        }

        PlayerController.Instance.transform.position = respawnPoint;

        StartCoroutine(UIManager.Instance.DeactivateDeathScreen());
        PlayerController.Instance.Respawned();
    }
}
