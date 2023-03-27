using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

namespace ScatterPlot 
{
    public class ScatterPlotManagement : MonoBehaviour
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

        #region Input Features
        [SerializeField] GameObject _scatterElement;

        [SerializeField] Material _materialCategory1;
        [SerializeField] Material _materialCategory2;
        [SerializeField] Material _materialCategory3;
        [SerializeField] Material _materialCategory4;
        [SerializeField] Material _materialCategory5;
        [SerializeField] Material _materialCategory6;
        [SerializeField] Material _materialCategory7;
        [SerializeField] Material _materialCategory8;

        [SerializeField] TextAsset _data;
        [SerializeField] string _firstParameter;
        [SerializeField] string _secondParameter;
        [SerializeField] int _currentlyPosition;
        #endregion

        #region Features
        private GameObject _tempObject;
        private JSONArray _tempData;
        #endregion

        void Start()
        {
            _tempData = (JSONArray)JSON.Parse(_data.text);
            for (int i = 0; _tempData.Count > i; i++)
            {
                if (!_tempData)
                {
                    _tempObject = Instantiate(_scatterElement);
                    _tempObject.transform.parent = transform;
                    AddName(_tempObject, i, _currentlyPosition, _firstParameter, _secondParameter );
                    AddTagObject(_tempObject, i);
                    SetMaterial(_tempObject, _tempData[i]["parameter1"]);
                    UpdatePosition(_tempObject, i, _firstParameter, _secondParameter, _currentlyPosition);
                }
            }
        }
        // Update is called once per frame
        void Update()
        {

        }

        private void AddTagObject(GameObject tempObject, int i)
        {
            tempObject.tag = string.Concat("scatter-", i);
        }

        private void UpdatePosition(GameObject gameObject, int element, string x, string y, int currentlyPosition)
        {
            Debug.Log(_tempObject.transform.localScale.ToString());
            Debug.Log(_tempObject.transform.position.ToString());
            Debug.Log(_tempData[element][x]);
            gameObject.transform.localPosition = new Vector3(_tempData[element][x][currentlyPosition]*8.5f, _tempData[element][y][currentlyPosition]*8.5f, 0);   
        }

        private void AddName(GameObject tempObject, int element, int currentlyPosition, string firstValue, string secondValue )
        {
            tempObject.name = string.Concat("scatter", "-", element, "-", currentlyPosition, "-", firstValue, "-", secondValue);
        }

        #region Select Material
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
    }

}
