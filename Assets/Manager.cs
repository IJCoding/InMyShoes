using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private Generator sunScript = null;

    [SerializeField]
    private Plant plantScript = null;


    // Start is called before the first frame update
    void Awake()
    {
        sunScript.SetValues("Light", 10, 0, true);
        var x = Instantiate<Generator>(sunScript);
        x.name = "Sun";
        
        GameObject y = Instantiate<Plant>(plantScript).gameObject;
        y.GetComponent<Plant>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
