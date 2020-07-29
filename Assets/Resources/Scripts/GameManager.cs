using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GridController GridController { get; private set; }
    public Vector3 GridSize;


    // Grid Selection Variables
    public Vector3 SelectedGridPos;
    public GameObject Selected_Tile;

    public Vector3 SelectedGridPos_1;
    public GameObject SelectedObject;
    public Vector3 SelectedGridPos_2;

    // Camera Variabels
    public bool CameraMovement = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GridController = new GridController();
        GridController.Init();
        GridController.CreateGrid(GridSize);
        SelectedGridPos = new Vector3(0,0,0);
        SelectedGridPos_1 = new Vector3(0,0,0);
        SelectedGridPos_2 = new Vector3(0,0,0);
        Selected_Tile = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Grid/Selected_Tile"), SelectedGridPos, Quaternion.identity);
        Selected_Tile.name = "Selected_Tile";
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement = Input.GetMouseButton(1);

        if(!CameraMovement)
        {
            // X - Axis
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (SelectedGridPos.x < GridSize.x)
                {
                    SelectedGridPos.x++;
                }

            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (SelectedGridPos.x > 0)
                {
                    SelectedGridPos.x--;
                }
            }

            // Y - Axis
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (SelectedGridPos.y < GridSize.y)
                {
                    SelectedGridPos.y++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                if (SelectedGridPos.y > 0)
                {
                    SelectedGridPos.y--;
                }
            }

            // Z - Axis
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (SelectedGridPos.z < GridSize.z)
                {
                    SelectedGridPos.z++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (SelectedGridPos.z > 0)
                {
                    SelectedGridPos.z--;
                }
            }


            // Rotation
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (SelectedGridPos.z < GridSize.z)
                {
                    Selected_Tile.transform.Rotate(0, -90, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Selected_Tile.transform.Rotate(0, 90, 0);
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
        else
        {
            // Camera movement is allowed
        }


        GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = SelectedGridPos.x + ":" + SelectedGridPos.y + ":" + SelectedGridPos.z;


        //BASIC Movement SYSTEM - HACKYWAKY
        try
        {
            Selected_Tile.transform.position = GameObject.Find("Grid").transform.Find("L#" + SelectedGridPos.y + "#").transform.Find("R#" + SelectedGridPos.z + "#").transform.Find("T#" + SelectedGridPos.x + "#").transform.position;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectedGridPos_1 = Selected_Tile.transform.position;
                foreach (Transform Ship in GameObject.Find("Ships").transform)
                {
                    if (Ship.GetComponent<ShipCore>().ShipPos == SelectedGridPos_1)
                    {
                        SelectedObject = Ship.gameObject;
                        GameObject.Find("Canvas").transform.Find("Selection_1").GetComponent<Text>().text = "Selection_1: " + Ship.GetComponent<ShipCore>().ShipID;
                        break;
                    }
                    else
                    {

                    }
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectedGridPos_2 = Selected_Tile.transform.position;
                GameObject.Find("Canvas").transform.Find("Selection_2").GetComponent<Text>().text = "Selection_2: " + SelectedGridPos.x + ":" + SelectedGridPos.y + ":" + SelectedGridPos.z;
            }

            if (SelectedObject != null && SelectedGridPos_2 != null && Input.GetKeyDown(KeyCode.M))
            {
                GameObject.Find(SelectedObject.GetComponent<ShipCore>().ShipID).GetComponent<ShipCore>().MoveTo(SelectedGridPos_2, Selected_Tile.transform.rotation);

            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        
    }
}
