using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private string neededResource = "Light";

    [SerializeField]
    private Generator resourceGenerator = null;

    [SerializeField]
    private uint hunger = uint.MaxValue;

    [SerializeField]
    private uint hungerMax = uint.MaxValue;

    [SerializeField]
    private uint size = uint.MinValue + 1;

    [SerializeField]
    private uint gather = uint.MinValue + 1;

    [SerializeField]
    private uint sizeChance = uint.MinValue +1;

    [SerializeField]
    private uint gatherChance = uint.MinValue +1;

    [SerializeField]
    private uint cloneChance = uint.MinValue + 1;


    private void Awake()
    {
        GameObject gen = GameObject.Find("Sun");
        if (gen != null) { Console.Write("No Sun Found"); }
        else { resourceGenerator = gen.GetComponent<Generator>(); }
    }

    private void Update()
    {
        hunger -= size;

        Eat();

        if (hunger >= (size * 10)) Grow();

    }

    private void Eat()
    {
        if (resourceGenerator != null)
        {
            if (resourceGenerator.SafeGiveResource() == "Light")
                hunger += gather;
        }
    }


    private void Grow()
    {
        uint choice = (uint)UnityEngine.Random.Range(0, sizeChance + gatherChance + cloneChance);

        if (choice < sizeChance)
        {
            size++;
            sizeChance += 1;
        }
        else if (choice < sizeChance + gatherChance)
        {
            gather++;
            gatherChance += 1;
        }
        else
        {
            GameObject clone = this.gameObject;
            Instantiate(clone);
        }
    }
}
