using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("General/AplicationManagement")]
public class AplicationManagement : MonoBehaviour
{
    [SerializeField] GameObject ScatterPlot;
    [SerializeField] GameObject BarChart;

    private bool activateScatterPlot;
    private bool activateBarChar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveScatterPlot ()
    {
        ScatterPlot.SetActive(activateScatterPlot);
        activateScatterPlot = !activateScatterPlot;
    }
    
    public void ActiveBarChar ()
    {
        BarChart.SetActive(activateBarChar);
        activateBarChar = !activateBarChar;
    }

}
