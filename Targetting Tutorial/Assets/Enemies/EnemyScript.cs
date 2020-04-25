using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    int IndexToTravelTo = 0;
    GlobalData GameData;

    public float Health = 10f;
    public float DistanceToNode;
    public float TrueDistance = 0f;

    void Start()
    {
        GameData = FindObjectOfType<GlobalData>();

        if(transform.position != GameData.PathNodes[0].position)
        {
            IndexToTravelTo = 0;
        }
    }

    void Update()
    {

        if (transform.position != GameData.PathNodes[IndexToTravelTo].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameData.PathNodes[IndexToTravelTo].position, 0.1f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(GameData.PathNodes[IndexToTravelTo].position - transform.position), 10f);
            DistanceToNode = Vector3.Distance(transform.position, GameData.PathNodes[IndexToTravelTo].position);
        }
        else
        {
            IndexToTravelTo++;

            if(IndexToTravelTo > GameData.PathNodes.Length - 1)
            {
                Destroy(gameObject);
            }

        }

        TrueDistance = 0f;
        TrueDistance += DistanceToNode;
        for (int i = IndexToTravelTo; i < GameData.PathNodes.Length; i++)
        {
            if(i < 4)
            {
                TrueDistance += GameData.PathNodes[i].GetComponent<DistanceToNode>().Distance;
            }
            else
            {
                break;
            }

        }

    }

    public void TakeDamage(float Damage)
    {
        Health -= Damage;

        if(Health <= 0f)
        {
            Destroy(gameObject);
        }
    }

}
