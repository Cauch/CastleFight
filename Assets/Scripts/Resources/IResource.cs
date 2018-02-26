using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IResource {
    public uint Value;
    public string Name;

    public IResource(uint value, string name)
    {
        this.Value = value;
        this.Name = name;
    }

    public virtual bool CanPurchase(IResource cost)
    {
        if (SameType(cost))
        {
            return cost.Value <= Value;
        }
        Debug.Log("Wrong Resource");
        return false;
    }

    public abstract void Purchase(IResource cost);

    bool SameType(IResource other)
    {
        return (other.GetType() == this.GetType()) ;
    }

    public virtual void Add(IResource other)
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
