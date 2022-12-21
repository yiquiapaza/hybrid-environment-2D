using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using SimpleJSON;

namespace ScatterPlot
{
    //TODO: create mouse event
    public class ScatterInteraction : MonoBehaviour
    {
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator SendRequest()
        {
            WWWForm form = new WWWForm();
            form.AddField("id", gameObject.tag);
            form.AddField("state", 0);
            using (UnityWebRequest request = UnityWebRequest.Post(Constants.ENDPOINT_COUNTRY, form))
            {
                yield return request.SendWebRequest();
                if (!request.isNetworkError || !request.isHttpError)
                {
                    Debug.Log(request.downloadHandler.text);
                }
                else
                {
                    Debug.Log("request Error");
                }
            }
        }
        
    }
}