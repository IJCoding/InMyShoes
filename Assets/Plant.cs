using JetBrains.Annotations;
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
    private uint hunger = 10;

    [SerializeField]
    private uint hungerMax = uint.MaxValue;

    [SerializeField]
    private uint size = 1;

    [SerializeField]
    private uint gather = 2;

    [SerializeField]
    private uint population = 1;

    [SerializeField]
    private uint sizeChance = 1;

    [SerializeField]
    private uint gatherChance = 1;

    [SerializeField]
    private uint cloneChance = 1;

    [SerializeField]
    private float time = 2;

    private void Awake()
    {
        GameObject gen = GameObject.Find("Sun");
        if (gen != null) { Console.Write("No Sun Found"); }
        else { resourceGenerator = gen.GetComponent<Generator>(); }
    }

    public string GetName()
    {
        return "G: " + gather.ToString() + ", S: " + size.ToString() + ", P: " + population.ToString();
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            this.name = GetName();

            for (uint i = population; i > 0; i--)
            {
                if (hunger > size)
                {
                    hunger -= size;

                    Eat();
                    if (hunger >= (size * 10)) Grow();

                    uint reproChance = (uint)UnityEngine.Random.Range(0, sizeChance + gatherChance + cloneChance);

                    if (reproChance < sizeChance)
                    {
                        Eat();
                    }
                    else if (reproChance < sizeChance + gatherChance)
                    {
                        Eat();
                    }
                    else
                    {
                        population += 1;
                    }
                }
                else 
                {
                    if(population >2) population--;
                    if (size >2) size--; 
                    if (gather >2) gather--;
                    if (sizeChance >2) sizeChance--;
                    if (gatherChance >2) gatherChance--;
                    if (cloneChance >2) cloneChance--;
                }
            }

            if(hunger > size *10)
            {
                population++;
                hunger = size * 10;
            }

            time = 2;
        }

    }

    private void Eat()
    {
        if (resourceGenerator != null)
        {
            if (resourceGenerator.SafeGiveResource() == "Light")
                hunger += gather;
        }
    }

    public void AdjustPopulation(uint amount)
    {
        population += amount;
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

            if(GameObject.Find(clone.gameObject.GetComponent<Plant>().GetName()))
            {
                var other = GameObject.Find(GetName());

                other.gameObject.GetComponent<Plant>().AdjustPopulation(1);

                Destroy(clone.gameObject);
            }

        }
    }
}
