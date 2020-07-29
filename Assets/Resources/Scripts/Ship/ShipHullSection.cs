using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHullSection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.GetComponent<ShipCore>().AddPartHealth(1);
        transform.parent.GetComponent<ShipCore>().AddShipLenght(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
