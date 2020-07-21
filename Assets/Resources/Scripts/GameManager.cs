using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GridController GridController { get; private set; }
    public Vector3 GridSize;

    // Start is called before the first frame update
    void Start()
    {
        GridController = new GridController();
        GridController.Init();
        GridController.CreateGrid(GridSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
