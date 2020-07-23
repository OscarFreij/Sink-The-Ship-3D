using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 Sensitivity = new Vector2(1, 1);
    public GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GM.CameraMovement)
        {
            this.transform.Rotate(0, Input.GetAxis("Mouse X") * Sensitivity.x, 0);
            this.transform.Find("Main Camera").transform.Rotate(-Input.GetAxis("Mouse Y") * Sensitivity.y, 0, 0);

            Vector3 MovementVector = new Vector3();

            MovementVector.z = Mathf.Cos(this.transform.eulerAngles.y * Mathf.PI / 180) * Input.GetAxis("Vertical");
            MovementVector.x = Mathf.Sin(this.transform.eulerAngles.y * Mathf.PI / 180) * Input.GetAxis("Vertical");

            MovementVector.z += Mathf.Sin(this.transform.eulerAngles.y * Mathf.PI / -180) * Input.GetAxis("Horizontal");
            MovementVector.x += Mathf.Cos(this.transform.eulerAngles.y * Mathf.PI / -180) * Input.GetAxis("Horizontal");

            MovementVector.y += Input.mouseScrollDelta.y;

            this.transform.position += MovementVector;
        }


    }
}
