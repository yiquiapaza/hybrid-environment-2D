using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace ScatterPlot
{
    public class ScatterData : MonoBehaviour
    {

        #region Features
        [SerializeField] TextAsset _data;
        [SerializeField] GameObject _message;

        private string[] data;
        private JSONArray _tempData;
        private GameObject message;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            message = Instantiate(_message);
            message.SetActive(false);
            //message.transform.parent = transform;
            _tempData = (JSONArray)JSON.Parse(_data.text);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseOver()
        {
            message.SetActive(true);
            message.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
            Debug.Log(gameObject.name);
            Debug.Log(gameObject.tag);
        }

        void OnMouseExit()
        {
            message.SetActive(false);   
        }
    }
}
