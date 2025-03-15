using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    [SerializeField]
    private Transform pointPerent;
    private List<Transform> spawnPoint = new List<Transform>();
    public List<Transform> SpawnPoint { get { return spawnPoint; } }

    private void Awake()
    {
        var point = pointPerent.GetComponentsInChildren<Transform>();
        for(int i = 0; i < point.Length; i++)
        {
            if (point[i] == pointPerent)
            {
                continue;
            }

            spawnPoint.Add(point[i]);
        }
    }
}
