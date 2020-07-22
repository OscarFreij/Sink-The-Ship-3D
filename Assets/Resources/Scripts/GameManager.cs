using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GridController GridController { get; private set; }
    public Vector3 GridSize;

    public bool ChangeX;
    public bool ChangeY;
    public bool ChangeZ;
    public Vector3 SelectedGridPos;
    public GameObject Selected_Tile;
    // Start is called before the first frame update
    void Start()
    {
        GridController = new GridController();
        GridController.Init();
        GridController.CreateGrid(GridSize);
        SelectedGridPos = new Vector3(0,0,0);
        Selected_Tile = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Grid/Selected_Tile"), SelectedGridPos, Quaternion.identity);
        Selected_Tile.name = "Selected_Tile";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            ChangeX = true;
            ChangeY = false;
            ChangeZ = false;
            GameObject.Find("Canvas").transform.Find("Image").transform.Find("Text").GetComponent<Text>().text = "X";
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeX = false;
            ChangeY = true;
            ChangeZ = false;
            GameObject.Find("Canvas").transform.Find("Image").transform.Find("Text").GetComponent<Text>().text = "Y";
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeX = false;
            ChangeY = false;
            ChangeZ = true;
            GameObject.Find("Canvas").transform.Find("Image").transform.Find("Text").GetComponent<Text>().text = "Z";
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            ChangeX = false;
            ChangeY = false;
            ChangeZ = false;
            GameObject.Find("Canvas").transform.Find("Image").transform.Find("Text").GetComponent<Text>().text = "U";
        }


        if(Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Plus))
        {
            if(ChangeX && SelectedGridPos.x < GridSize.x)
            {
                SelectedGridPos += new Vector3(1, 0, 0);
            }
            else if (ChangeY && SelectedGridPos.y < GridSize.y)
            {
                SelectedGridPos += new Vector3(0, 1, 0);
            }
            else if (ChangeZ && SelectedGridPos.z < GridSize.z)
            {
                SelectedGridPos += new Vector3(0, 0, 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus))
        {
            if (ChangeX && SelectedGridPos.x > 0)
            {
                SelectedGridPos -= new Vector3(1, 0, 0);
            }
            else if (ChangeY && SelectedGridPos.y > 0)
            {
                SelectedGridPos -= new Vector3(0, 1, 0);
            }
            else if (ChangeZ && SelectedGridPos.z > 0)
            {
                SelectedGridPos -= new Vector3(0, 0, 1);
            }
        }


        GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = SelectedGridPos.x + ":" + SelectedGridPos.y + ":" + SelectedGridPos.z;

        try
        {
            Selected_Tile.transform.position = GameObject.Find("Grid").transform.Find("L#" + SelectedGridPos.y + "#").transform.Find("R#" + SelectedGridPos.z + "#").transform.Find("T#" + SelectedGridPos.x + "#").transform.position;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        
    }
}
