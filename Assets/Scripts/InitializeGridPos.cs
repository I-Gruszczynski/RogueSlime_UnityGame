using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGridPos : MonoBehaviour
{
    public bool shouldInitialize = true;
    [SerializeField] public static float tileWidth = 1f;
    void Start()
    {
        if(shouldInitialize)
        {
            GridController grid = FindObjectOfType<GridController>();
            if (grid != null)
            {
                Vector2Int gridPos = new Vector2Int(Mathf.RoundToInt(transform.position.x / tileWidth), Mathf.RoundToInt(transform.position.z / tileWidth));
                grid.PlaceObject(gameObject, gridPos);
            }
        }
    }
    void Update()
    {
        
    }
}
