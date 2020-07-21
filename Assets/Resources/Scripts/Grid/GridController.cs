using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public bool IsInitialized { get; private set; } = false;
    public bool GridExists { get; private set; }
    public Vector3 GridSize { get; private set; }
    public GameObject TileObject { get; private set; }
    public GameObject GridObject { get; private set; }

    public bool Init()
    {
        try
        {
            this.GridExists = false;
            this.GridSize = new Vector3();

            this.TileObject = Resources.Load<GameObject>("Prefabs/Grid/Tile");

            this.IsInitialized = true;
        }
        catch
        {
            Debug.LogError("ERROR - GridController could not be initialized!");
        }
        return true;
    }
    
    // GridController - Grid creation function
    public bool CreateGrid(Vector3 GridSize)
    {
        if(!this.IsInitialized)
        {
            Debug.LogError("ERROR - GridController not initialized!");
            return false;
        }

        // If true, Flush all values, then run rest of init.
        if (this.GridExists)
        {
            Debug.LogWarning("WARNING - Grid already exists! - Removing old grid and creating new grid");

            this.GridExists = false;
            this.GridSize = new Vector3();
        }

        this.GridSize = GridSize;

        try
        {
            this.GridObject = new GameObject();
            this.GridObject.transform.position = new Vector3(0, 0, 0);
            this.GridObject.transform.rotation = Quaternion.identity;
            this.GridObject.name = "Grid";

            // Add new layer
            for (int i = 0; i < GridSize.y; i++)
            {
                GameObject tempLayer = new GameObject();
                tempLayer.transform.position = new Vector3(0, i, 0);
                tempLayer.transform.rotation = Quaternion.identity;
                tempLayer.name = "L" + i + "#";
                tempLayer.transform.parent = this.GridObject.transform;
                // Add new row
                for (int j = 0; j < GridSize.x; j++)
                {
                    GameObject tempRow = new GameObject();
                    tempRow.transform.position = new Vector3(0, i, j);
                    tempRow.transform.rotation = Quaternion.identity;
                    tempRow.name = tempLayer.name + "R" + j + "#";
                    tempRow.transform.parent = tempLayer.transform;
                    // Add new tile
                    for (int k = 0; k < GridSize.z; k++)
                    {
                        GameObject tempTile = Instantiate<GameObject>(this.TileObject, new Vector3(k, i, j), Quaternion.identity);
                        tempTile.name = tempRow.name + "T" + k + "#";
                        tempTile.transform.parent = tempRow.transform;
                        Debug.Log(tempTile.name + " created");
                    }
                }
            }
        }
        catch
        {
            Debug.LogError("ERROR - Could not create Grid Object!");
        }

        return true;
    }
}
