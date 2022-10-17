using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
        form.AddField("state", 1);
        Debug.Log(gameObject.transform.ToString());
        using (UnityWebRequest request = UnityWebRequest.Post(Constants.ENDPOINT_RAWDATA, form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
                Debug.Log(request.downloadHandler.text);
        }
    }
}
