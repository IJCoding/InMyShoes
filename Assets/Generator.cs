using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private string type = string.Empty;

    [SerializeField] 
    private uint current = uint.MaxValue;

    [SerializeField]
    private uint breakChance = uint.MinValue;

    [SerializeField]
    private bool working = true;

    public string UnsafeGiveResource()
    {
        if (working)
        {
            if (current == 0)
            {
                if (Random.value > breakChance)
                {
                    working = false;
                }
            }
            else
            {
                current -= 1;
            }
            return type;
        }
        return "Empty";
    }

    public string SafeGiveResource()
    {
        return type;
    }
}
