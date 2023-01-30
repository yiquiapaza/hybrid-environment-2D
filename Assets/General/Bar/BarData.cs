using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using TMPro;
using System;

namespace BarChart
{
    public class BarData : MonoBehaviour
    {
        #region Features
        [SerializeField] TextAsset _data;
        [SerializeField] GameObject _message;

        private string[] data;
        private JSONArray _tempData;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            _tempData = (JSONArray)JSON.Parse(_data.text);
        }

        void Update()
        {

        }
        void OnMouseDown()
        {
            GameObject tempGameOject;

            data = gameObject.name.Split('-');
            Debug.Log(data[0]);
            tempGameOject = _message.transform.GetChild(1).gameObject;
            Debug.Log(tempGameOject.name);
            tempGameOject.GetComponent<TextMeshPro>().text = _tempData[Int16.Parse(data[1])]["parameter"] + "\n" + _tempData[Int16.Parse(data[1])]["parameter3"][Int16.Parse(data[2])];

        }
    }
}
