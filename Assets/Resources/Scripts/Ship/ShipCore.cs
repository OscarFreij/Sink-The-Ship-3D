using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCore : MonoBehaviour
{
    public string ShipID { get; private set; }
    public int ShipLenght { get; private set; }
    public int ShipLenghtOffset { get; private set; }
    public Vector3 ShipPos { get; private set; }
    public int Health { get; private set; }
    public List<ShipClass.ShipEffect> ShipEffects { get; private set; }

    private void Start()
    {
        ShipID = "REF-36K";
        Health = 0;
        this.transform.name = ShipID;
        ShipLenght = 0;
        ShipLenghtOffset = 0;
    }
    public void Init(string ShipID, bool IsCustomShip)
    {
        this.ShipID = ShipID;
    }

    public bool AddPartHealth(int Amount)
    {
        Health += Amount;
        return true;
    }

    public bool AddShipLenght(int Amount)
    {
        ShipLenght += Amount;
        return true;
    }

    public bool MoveTo(Vector3 NewPos, Quaternion NewRotation)
    {
        this.transform.position = NewPos;
        this.transform.rotation = NewRotation;
        this.ShipPos = NewPos;
        return true;
    }
}
