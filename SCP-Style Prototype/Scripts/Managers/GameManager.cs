using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Collider EnemySpawnTrig;
    public Collider FriendlyAIActivateTrig;
    public GameObject EnemyAIOBJ;
    public GameObject FriendlyAIOBJ; 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!EnemySpawnTrig.gameObject.activeInHierarchy)
        {
            EnemyAIOBJ.GetComponent<EnemyAiTutorial>().enabled = true; 
        }

        if (!FriendlyAIActivateTrig.gameObject.activeInHierarchy)
        {
            FriendlyAIOBJ.GetComponent<FriendlyAI>().enabled = true;
        }
    }
}
