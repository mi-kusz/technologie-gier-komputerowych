using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public List<Vector3> possiblePositions;
    
    void Start()
    {
        if (possiblePositions != null && possiblePositions.Count > 0)
        {
            int randomIndex = Random.Range(0, possiblePositions.Count);
            transform.localPosition = possiblePositions[randomIndex];
            
            Debug.Log("Wylosowano pozycję " + transform.localPosition);
        }
        else
        {
            Debug.Log("Brak dostępnych pozycji dla " + name);
        }
    }
}
