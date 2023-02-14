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

        void Update()
        {

        }

        void OnMouseOver()
        {
            message.SetActive(true);
            message.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z - 0.5f);
            GameObject tempGameOject;
            data = gameObject.name.Split('-');
            Debug.Log(gameObject.name);
            Debug.Log(gameObject.tag);
            tempGameOject = message.transform.GetChild(1).gameObject;
            Debug.Log(tempGameOject.name);
            tempGameOject.GetComponent<TextMeshPro>().text = _tempData[Int16.Parse(data[1])]["parameter"] + "\n" + _tempData[Int16.Parse(data[1])]["parameter3"][Int16.Parse(data[2])];
        }

        private void OnMouseExit()
        {
            message.SetActive(false);
        }
    }
}
