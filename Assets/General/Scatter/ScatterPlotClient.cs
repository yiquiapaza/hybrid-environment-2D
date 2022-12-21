using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;


namespace ScatterPlot
{
    public class ScatterPlotClient : MonoBehaviour
    {
        #region Features
        [SerializeField] Material _selectMaterial;
        private JSONNode _dataRequest;
        private GameObject _tempObject;
        #endregion
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        IEnumerator RequestServer()
        {
            using (UnityWebRequest request = UnityWebRequest.Get(Constants.ENDPOINT_RAWDATA))
            {
                yield return request.SendWebRequest();
                if (request.isHttpError || request.isHttpError)
                {

                }
                else
                {
                    _dataRequest = (JSONNode)JSON.Parse(request.downloadHandler.text);
                    if (!_dataRequest["staste"].Equals("scatter001"))
                    {

                        _tempObject = GameObject.Find(_dataRequest["state"]);
                        _tempObject.GetComponent<MeshRenderer>().material = _selectMaterial;
                    }
                }
            }
        }

        IEnumerator WaitServer()
        {
            while (true)
            {
                StartCoroutine(RequestServer());
                yield return new WaitForSecondsRealtime(1f);
            }
        }
    }
}