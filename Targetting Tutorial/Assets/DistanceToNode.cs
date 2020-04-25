using UnityEngine;

public class DistanceToNode : MonoBehaviour
{
    GlobalData WorldAccessData;
    public float Distance = 0f;
    public int Index = 0; // Assign me

    void Start()
    {
        WorldAccessData = FindObjectOfType<GlobalData>();

        Distance = Vector3.Distance(transform.position, WorldAccessData.PathNodes[Index + 1].position);

    }
}
