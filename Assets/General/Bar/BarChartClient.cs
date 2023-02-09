using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

namespace BarChart
{
    public class BarChartClient : MonoBehaviour
    {
        [SerializeField]
        //private Material _changeMaterial;
        private JSONNode _dataRequest;
        private Material _tempMaterial;
        private string _nameObject = "bar";
        private GameObject _tempObject;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(WaitServer());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator RequestServer()
        {
            using (UnityWebRequest request = UnityWebRequest.Get(Constants.ENDPOINT_BARCHART_GET))
            {
                yield return request.SendWebRequest();
                if (request.isHttpError || request.isNetworkError)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    Debug.Log(request.downloadHandler.text);
                    _dataRequest = (JSONArray)JSON.Parse(request.downloadHandler.text);
                    if (!_dataRequest.IsNull)
                    {

                        //_tempObject = GameObject.Find(_dataRequest["state"]);
                        //_tempMaterial = _tempObject.GetComponent<MeshRenderer>().material;
                        //_tempObject.GetComponent<MeshRenderer>().material = _changeMaterial;
                        Debug.Log(_dataRequest);
                    }
                    Debug.Log(_dataRequest);
                }

            }
        }

        IEnumerator WaitServer()
        {
            while (true)
            {
                StartCoroutine(RequestServer());
                Debug.Log("Wait for next Update");
                yield return new WaitForSecondsRealtime(1f);
            }
        }
    }
}