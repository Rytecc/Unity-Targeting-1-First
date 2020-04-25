using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningSystem : MonoBehaviour
{
    public GlobalData GameData;
    public Transform StartPoint;
    public GameObject[] EnemyPrefabs;
    public float SpawnDelay = 0.5f;
    float Delay;

    // Start is called before the first frame update
    void Start()
    {
        Delay = SpawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(Delay > 0f)
        {
            Delay -= Time.deltaTime;
        }
        else if(Delay <= 0f)
        {
            int Rand = Random.Range(0, EnemyPrefabs.Length);

            Instantiate(EnemyPrefabs[Rand], StartPoint.position, Quaternion.identity);

            GameData.UpdateArrays();

            Delay = SpawnDelay;
        }
    }
}
