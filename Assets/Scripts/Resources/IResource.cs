using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource {
    public uint Value;
    public string Name;

    public Resource(uint value, string name)
    {
        this.Value = value;
        this.Name = name;
    }

    public virtual bool CanPurchase(Resource cost)
    {
        if (SameType(cost))
        {
            return cost.Value <= Value;
        }
        Debug.Log("Wrong Resource");
        return false;
    }

    public abstract void Purchase(Resource cost);

    bool SameType(Resource other)
    {
        return (other.GetType() == this.GetType()) ;
    }

    public virtual void Add(Resource other)
    {
        if(SameType(other))
        {
            Value += other.Value;
        }
    }

    public override string ToString()
    {
        return Name + ": " + Value;
    }

}
