using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BarElementClient : MonoBehaviour
{
    // Start is called before the first frame update
    private WWWForm elementInfo = null;
    private string[] dataElment;
    void Start()
    {
        dataElment = gameObject.name.Split('-');
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SendElemntInfo()
    {
        elementInfo = new WWWForm();
        elementInfo.AddField("element", dataElment[1]);
        elementInfo.AddField("value", dataElment[2]);
        using (UnityWebRequest client = UnityWebRequest.Post(Constants.ENDPOINT_BARCHART_DESKTOP_POST, elementInfo ))
        {
            yield return client.SendWebRequest();
            if (client.isHttpError)
                Debug.LogError(client.error);
        }
    }

    private void OnMouseDown()
    {
        StartCoroutine(SendElemntInfo());
        Debug.Log("send data");
    }
}
