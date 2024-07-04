using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{

    [SerializeField] GameObject[] maps;

    Bench bench;

    [System.Obsolete]
    private void OnEnable()
    {
        bench = FindObjectOfType<Bench>();
        if(bench != null )
        {
            if(bench.interacted)
            {
                UpdateMap();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateMap()
    {
        var savedScenes = SaveData.Instance.sceneNames;

        for(int i = 0; i < maps.Length; i++)
        {
            if(savedScenes.Contains("Cave_" + (i + 1)))
            {
                maps[i].SetActive(true);
            }
            else
            {
                maps[i].SetActive(false);
            }
        }
    }
}
