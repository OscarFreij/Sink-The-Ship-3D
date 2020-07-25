using System;
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
            return false;
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
                tempLayer.name = "L#" + i + "#";
                tempLayer.transform.parent = this.GridObject.transform;
                Debug.Log(tempLayer.name + " created");
                // Add new row
                for (int j = 0; j < GridSize.x; j++)
                {
                    GameObject tempRow = new GameObject();
                    tempRow.transform.position = new Vector3(0, i, j);
                    tempRow.transform.rotation = Quaternion.identity;
                    tempRow.name = "R#" + j + "#";
                    tempRow.transform.parent = tempLayer.transform;
                    Debug.Log(tempRow.name + " created");
                    // Add new tile
                    for (int k = 0; k < GridSize.z; k++)
                    {
                        GameObject tempTile = Instantiate<GameObject>(this.TileObject, new Vector3(k, i, j), Quaternion.identity);
                        tempTile.name = "T#" + k + "#";
                        tempTile.transform.parent = tempRow.transform;
                        Debug.Log(tempTile.name + " created");
                    }
                }
            }
        }
        catch
        {
            Debug.LogError("ERROR - Could not create Grid Object!");
            return false;
        }

        try
        {
            GameObject WallGroopObject = new GameObject();
            WallGroopObject.name = "WallGroopObject";

            GameObject tempObject;


            // Floor
            tempObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Grid/MapWalls/WallObject_#Orientation#"), new Vector3((GridSize.x / 2)-0.5f, 0, (GridSize.z / 2) - 0.5f), Quaternion.identity);
            tempObject.transform.eulerAngles = new Vector3(-90, 0, 0);
            tempObject.transform.Find("Wall").GetComponent<SpriteRenderer>().size = new Vector2(GridSize.x, GridSize.z);
            tempObject.transform.Find("Wall").GetComponent<SpriteRenderer>().sortingOrder = -1;

            tempObject.transform.Find("WallMask").transform.localScale = new Vector3(GridSize.x, GridSize.z, 1);
            tempObject.transform.Find("WallMask").GetComponent<SpriteMask>().frontSortingOrder = -1;
            tempObject.transform.Find("WallMask").GetComponent<SpriteMask>().backSortingOrder = -2;

            tempObject.name = "WallObject_#Floor#";
            tempObject.transform.parent = WallGroopObject.transform;


            // Front
            tempObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Grid/MapWalls/WallObject_#Orientation#"), new Vector3(GridSize.x - 1, (GridSize.y / 2) - 0.5f, (GridSize.z / 2) - 0.5f), Quaternion.identity);
            tempObject.transform.eulerAngles = new Vector3(0, -90, 0);
            tempObject.transform.Find("Wall").GetComponent<SpriteRenderer>().size = new Vector2(GridSize.x, GridSize.y);
            tempObject.transform.Find("Wall").GetComponent<SpriteRenderer>().sortingOrder = -2;

            tempObject.transform.Find("WallMask").transform.localScale = new Vector3(GridSize.z, GridSize.y, 1);
            tempObject.transform.Find("WallMask").GetComponent<SpriteMask>().frontSortingOrder = -2;
            tempObject.transform.Find("WallMask").GetComponent<SpriteMask>().backSortingOrder = -3;

            tempObject.name = "WallObject_#Front#";
            tempObject.transform.parent = WallGroopObject.transform;


            // Rear
            tempObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Grid/MapWalls/WallObject_#Orientation#"), new Vector3(0, (GridSize.y / 2) - 0.5f, (GridSize.z / 2) - 0.5f), Quaternion.identity);
            tempObject.transform.eulerAngles = new Vector3(0, 90, 0);
            tempObject.transform.Find("Wall").GetComponent<SpriteRenderer>().size = new Vector2(GridSize.z, GridSize.y);
            tempObject.transform.Find("Wall").GetComponent<SpriteRenderer>().sortingOrder = -3;

            tempObject.transform.Find("WallMask").transform.localScale = new Vector3(GridSize.x, GridSize.y, 1);
            tempObject.transform.Find("WallMask").GetComponent<SpriteMask>().frontSortingOrder = -3;
            tempObject.transform.Find("WallMask").GetComponent<SpriteMask>().backSortingOrder = -4;

            tempObject.name = "WallObject_#Rear#";
            tempObject.transform.parent = WallGroopObject.transform;


            // Right
            tempObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Grid/MapWalls/WallObject_#Orientation#"), new Vector3((GridSize.x / 2) - 0.5f, (GridSize.y / 2) - 0.5f, 0), Quaternion.identity);
            tempObject.transform.eulerAngles = new Vector3(0, 0, 0);
            tempObject.transform.Find("Wall").GetComponent<SpriteRenderer>().size = new Vector2(GridSize.x, GridSize.y);
            tempObject.transform.Find("Wall").GetComponent<SpriteRenderer>().sortingOrder = -4;

            tempObject.transform.Find("WallMask").transform.localScale = new Vector3(GridSize.x, GridSize.y, 1);
            tempObject.transform.Find("WallMask").GetComponent<SpriteMask>().frontSortingOrder = -4;
            tempObject.transform.Find("WallMask").GetComponent<SpriteMask>().backSortingOrder = -5;

            tempObject.name = "WallObject_#Right#";
            tempObject.transform.parent = WallGroopObject.transform;


            // Left
            tempObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Grid/MapWalls/WallObject_#Orientation#"), new Vector3((GridSize.x / 2) - 0.5f, (GridSize.y / 2) - 0.5f, GridSize.z - 1), Quaternion.identity);
            tempObject.transform.eulerAngles = new Vector3(0, -180, 0);
            tempObject.transform.Find("Wall").GetComponent<SpriteRenderer>().size = new Vector2(GridSize.x, GridSize.y);
            tempObject.transform.Find("Wall").GetComponent<SpriteRenderer>().sortingOrder = -5;

            tempObject.transform.Find("WallMask").transform.localScale = new Vector3(GridSize.x, GridSize.y, 1);
            tempObject.transform.Find("WallMask").GetComponent<SpriteMask>().frontSortingOrder = -5;
            tempObject.transform.Find("WallMask").GetComponent<SpriteMask>().backSortingOrder = -6;

            tempObject.name = "WallObject_#Left#";
            tempObject.transform.parent = WallGroopObject.transform;


        }
        catch (Exception e)
        {
            Debug.LogError("ERROR - Could not create Grid Walls! | " + e);
            return false;
        }

        return true;
    }
}
