using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace BarChart
{
    public class BarInteraction : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator OnMouseDown()
        {
            WWWForm form = new WWWForm();
            form.AddField("state", gameObject.name);
            Debug.Log(gameObject.transform.ToString());
            using (UnityWebRequest request = UnityWebRequest.Post(Constants.ENDPOINT_BARCHART_GET, form))
            {
                yield return request.SendWebRequest();
                if (request.isNetworkError || request.isHttpError)
                    Debug.Log(request.error);
                else
                    Debug.Log(request.downloadHandler.text);
            }
        }
    }
}