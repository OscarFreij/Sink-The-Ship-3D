using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipClass
{
    public class ShipEffect
    {
        public string Name { get; private set; }
        public int RemainingTurns { get; private set; }
        public int Severity { get; private set; }

        public ShipEffect(string Name, int Severity, int AmountOfTurns)
        {
            this.Name = Name;
            this.RemainingTurns = AmountOfTurns;
            this.Severity = Severity;
        }

        public void Tick()
        {
            if (RemainingTurns > 0)
            {
                RemainingTurns--;
            }
        }
    }

    public class ShipModule
    {

    }

    public class Action
    {
        public string ActionName { get; private set; }
        public string ShipID { get; private set; }
        
        public Vector3 TargetPos { get; private set; }

        public Action(string ActionName, string ShipID, Vector3 TargetPos)
        {
            this.ActionName = ActionName;
            this.ShipID = ShipID;
            this.TargetPos = TargetPos;
        }
    }
}
