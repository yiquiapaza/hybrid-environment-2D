using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;


namespace ScatterPlot
{
    public class ScatterPlotElementClient : MonoBehaviour
    {
        private WWWForm elementInfo = null;
        private string[] dataElement;

        // Start is called before the first frame update
        void Start()
        {
            dataElement = gameObject.name.Split('-');
        }

        // Update is called once per frame
        void Update()
        {

        }
        IEnumerator SendElementInfo()
        {
            elementInfo = new WWWForm();
            elementInfo.AddField("element", dataElement[1]);
            elementInfo.AddField("value", dataElement[2]);
            Debug.Log("XXXXXXXXXXXXXXXX");
            using (UnityWebRequest client = UnityWebRequest.Post(Constants.ENDPOINT_SCATTERPLOT_DESKTOP_POST, elementInfo))
            {
                yield return client.SendWebRequest();
                if (client.isHttpError || client.isHttpError)
                    Debug.LogError(client);
            }
        }

        private void OnMouseDown()
        {
            StartCoroutine(SendElementInfo());
            Debug.Log("send data scatterplot");
        }
    }
}