using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

#region Materials
public static class MaterialSelector
{
    public const string CATEGORY1 = "OC";
    public const string CATEGORY2 = "AS";
    public const string CATEGORY3 = "EU";
    public const string CATEGORY4 = "AF";
    public const string CATEGORY5 = "NA";
    public const string CATEGORY6 = "01";
    public const string CATEGORY7 = "02";
    public const string CATEGORY8 = "03";
}
#endregion

[AddComponentMenu("General/AplicationManagement")]
public class AplicationManagement : MonoBehaviour
{
    private GameObject _temObjBar = null;
    private GameObject _temObjScatter = null;

    private JSONArray dataRequest;
    #region Input Features
    private JSONArray _tempData;
    [SerializeField] TextAsset _data;

    [SerializeField] Material _materialCategory1;
    [SerializeField] Material _materialCategory2;
    [SerializeField] Material _materialCategory3;
    [SerializeField] Material _materialCategory4;
    [SerializeField] Material _materialCategory5;
    [SerializeField] Material _materialCategory6;
    [SerializeField] Material _materialCategory7;
    [SerializeField] Material _materialCategory8;
    #endregion

    [SerializeField] GameObject ScatterPlot;
    [SerializeField] GameObject BarChart;

    private bool activateScatterPlot;
    private bool activateBarChar;


    private Vector3 positionBarchart;
    private Vector3 positionScatterPlot;
    // Start is called before the first frame update
    void Start()
    {
        _tempData = (JSONArray)JSON.Parse(_data.text);
        positionBarchart = BarChart.transform.position;
        positionScatterPlot = ScatterPlot.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveScatterPlot ()
    {
        if (activateScatterPlot)
        {
            ScatterPlot.transform.localPosition = new Vector3(0, 0, 0);
            activateScatterPlot = !activateScatterPlot;
        }
        else
        {
            ScatterPlot.transform.position = positionScatterPlot;
            activateScatterPlot = !activateScatterPlot;

        }
    }
    
    public void ActiveBarChar ()
    {
        if (activateBarChar)
        {
            BarChart.transform.position = new Vector3(50f, 0, 0);
            activateBarChar = !activateBarChar;
        }
        else
        {
            BarChart.transform.position = positionBarchart;
            activateBarChar = !activateBarChar;

        }
    }

    IEnumerator ResetBarChartCoroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(Constants.ENDPOINT_BARCHART_RESET))
        {
            yield return request.SendWebRequest();
            if (request.isHttpError || request.isNetworkError)
                Debug.Log("Error Request");
            else
            {
                dataRequest = (JSONArray)JSON.Parse(request.downloadHandler.text);
                for (int i = 0; _tempData.Count > i; i++)
                {
                    Debug.Log("|||" + _tempData.Count);
                    for (int j = 0; _tempData[i]["parameter3"].Count > j; j++)
                    {
                        Debug.Log("|||" + _tempData[i]["parameter3"].Count);
                        _temObjBar = GameObject.Find("bar-"+ i+ "-"+ j);
                        if (_temObjBar != null)
                            SetMaterial(_temObjBar, _tempData[i]["parameter1"]);
                    }
                }

            }
        }
    }

    IEnumerator ResetScatterPlotCoroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(Constants.ENDPOINT_SCATTERPLOT_RESET))
        {
            yield return request.SendWebRequest();
            if (request.isHttpError || request.isNetworkError)
                Debug.Log("Error Request");
            else
            {
                dataRequest = (JSONArray)JSON.Parse(request.downloadHandler.text);
                for (int i = 0; _tempData.Count > i; i++)
                {
                    for (int j = 0; _tempData[i]["parameter3"].Count > j; j++)
                    {
                        _temObjScatter = GameObject.Find(string.Concat("scatter-", i, "-", j));
                        if (_temObjScatter != null)
                            SetMaterial(_temObjScatter, _tempData[i]["parameter1"]);

                    }
                }

            }
        }
    }


    public void ResetBarChart()
    {
        StartCoroutine(ResetBarChartCoroutine());
    }

    public void ResetScatterPlot()
    {
        StartCoroutine(ResetScatterPlotCoroutine());
    }

    #region Select Material
    void SetMaterial(GameObject gameObject, string category)
    {
        Debug.Log(gameObject.name);
        switch (category)
        {
            case MaterialSelector.CATEGORY1:
                gameObject.GetComponent<MeshRenderer>().material = _materialCategory1;
                break;
            case MaterialSelector.CATEGORY2:
                gameObject.GetComponent<MeshRenderer>().material = _materialCategory2;
                break;
            case MaterialSelector.CATEGORY3:
                gameObject.GetComponent<MeshRenderer>().material = _materialCategory3;
                break;
            case MaterialSelector.CATEGORY4:
                gameObject.GetComponent<MeshRenderer>().material = _materialCategory4;
                break;
            case MaterialSelector.CATEGORY5:
                gameObject.GetComponent<MeshRenderer>().material = _materialCategory5;
                break;
            case MaterialSelector.CATEGORY6:
                gameObject.GetComponent<MeshRenderer>().material = _materialCategory6;
                break;
            case MaterialSelector.CATEGORY7:
                gameObject.GetComponent<MeshRenderer>().material = _materialCategory7;
                break;
            case MaterialSelector.CATEGORY8:
                gameObject.GetComponent<MeshRenderer>().material = _materialCategory8;
                break;
        }
    }
    #endregion
}
