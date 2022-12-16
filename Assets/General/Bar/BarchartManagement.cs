using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Microsoft.MixedReality.Toolkit;
using SimpleJSON;

namespace BarChart
{
    #region Materials
    public static class MaterialSelector
    {
        public const string CATEGORY1 = "OC";
        public const string CATEGORY2 = "AS";
        public const string CATEGORY3 = "EU";
        public const string CATEGORY4 = "AF";
        public const string CATEGORY5 = "NA";
        public const string CATEGORY6 = "01";
        public const string CATEGORY7 = "02"; 
        public const string CATEGORY8 = "03";
    }
    #endregion

    public class BarchartManagement : MonoBehaviour
    {
        #region Features
        [SerializeField] GameObject _barElement;
        [SerializeField] GameObject _message;
        //Yellow
        [SerializeField] Material _materialCategory1;
        //red
        [SerializeField] Material _materialCategory2;
        //Purple
        [SerializeField] Material _materialCategory3;
        //Green
        [SerializeField] Material _materialCategory4;
        //Blue
        [SerializeField] Material _materialCategory5;
        //White
        [SerializeField] Material _materialCategory6;
        //ligth
        [SerializeField] Material _materialCategory7;
        //Emerald
        [SerializeField] Material _materialCategory8;

        [SerializeField] TextAsset _data;

        private GameObject TempObj;
        private Vector3 _relativeScale;
        private Vector3 _relativePosition;
        private float _valueZ = 1;
        private float _tmpHeight = 0.0f;
        private readonly string _nameObject = "bar";
        private JSONArray _tempData;
        private float _temp = 0;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            _tempData = (JSONArray)JSON.Parse(_data.text);
            for (int i = 0; _tempData.Count > i; i++)
            {

                for (int j = 0; _tempData[i]["parameter3"].Count > j; j++)
                {
                    TempObj = Instantiate(_barElement) as GameObject;
                    TempObj.transform.parent = transform;
                    UpdateBarSize(TempObj, i);
                    UpdateBarPosition(TempObj, i, j);
                    SetMaterial(TempObj, _tempData[i]["parameter1"]);
                    AddNameObject(TempObj, i+1, j);
                }
            }
            StartCoroutine(WaitResquest());
        }

        // Update is called once per frame
        void Update()
        {
            //ObjectInfo();
        }

        void UpdateBarPosition(GameObject gameObject, int indexX, int indexZ)
        {
            Debug.Log("Possition");
            _relativePosition = gameObject.transform.localPosition;
            Debug.Log("Position" + _relativePosition.ToString());
            _relativeScale = gameObject.transform.localScale;
            float length = 0;
            if (indexZ == 0 )
            {
                _temp = 0;
                _temp = _relativeScale.y;
                length = _temp / 2;
                gameObject.transform.localPosition = new Vector3(indexX + 1f, length, 0);
            }
            else
            {
                _temp = _temp + _relativeScale.y / 2 ;
                
                gameObject.transform.localPosition = new Vector3(indexX + 1f, _temp + 0.1f, 0);
                _temp = _temp + _relativeScale.y / 2 + 0.1f;
            }
        }

        void UpdateBarSize(GameObject gameObject, float size = 0)
        {
            _relativeScale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x / _relativeScale.x , 
                Random.Range(0f, gameObject.transform.localScale.y / _relativeScale.y * 9), 
                gameObject.transform.localScale.z / _relativeScale.z);
        }

        #region Material
        void SetMaterial(GameObject gameObject, string category)
        {
            switch (category)
            {
                case MaterialSelector.CATEGORY1:
                    gameObject.GetComponent<MeshRenderer>().material = _materialCategory1;
                    break;
                case MaterialSelector.CATEGORY2:
                    gameObject.GetComponent<MeshRenderer>().material = _materialCategory2;
                    break;
                case MaterialSelector.CATEGORY3:
                    gameObject.GetComponent<MeshRenderer>().material = _materialCategory3;
                    break;
                case MaterialSelector.CATEGORY4:
                    gameObject.GetComponent<MeshRenderer>().material = _materialCategory4;
                    break;
                case MaterialSelector.CATEGORY5:
                    gameObject.GetComponent<MeshRenderer>().material = _materialCategory5;
                    break;
                case MaterialSelector.CATEGORY6:
                    gameObject.GetComponent<MeshRenderer>().material = _materialCategory6;
                    break;
                case MaterialSelector.CATEGORY7:
                    gameObject.GetComponent<MeshRenderer>().material = _materialCategory7;
                    break;
                case MaterialSelector.CATEGORY8:
                    gameObject.GetComponent<MeshRenderer>().material = _materialCategory8;
                    break;
            }
        }
        #endregion

        IEnumerator RequestServer ()
        {
            using (UnityWebRequest request = UnityWebRequest.Get(Constants.ENDPOINT_RAWDATA))
            {
                yield return request.SendWebRequest();
                if (request.isHttpError || request.isNetworkError)
                {
                    Debug.Log(request.error);
                } 
                else
                {
                    Debug.Log(request.downloadHandler.text);
                }
            }
        }

        IEnumerator WaitResquest()
        {
            while (true)
            {
                StartCoroutine(RequestServer());
                Debug.Log("Wait for next update");
                yield return new WaitForSecondsRealtime(1f);
            }
        }

        void ObjectInfo ()
        {
            if (CoreServices.InputSystem.GazeProvider.GazeTarget)
            {
                var tempPosition = CoreServices.InputSystem.GazeProvider.GazeTarget.transform.position;
                _message.transform.SetPositionAndRotation(new Vector3(tempPosition.x, tempPosition.y + 0.08f, tempPosition.z), Quaternion.LookRotation(Camera.main.transform.forward));
            }
            else
            {
                _message.transform.position = new Vector3(0, 0, -10);
            }
        }

        void AddNameObject(GameObject gameObj, int indexX, int indexY)
        {
            gameObj.name = string.Concat(_nameObject, indexX, indexY);
        }
    }
}












