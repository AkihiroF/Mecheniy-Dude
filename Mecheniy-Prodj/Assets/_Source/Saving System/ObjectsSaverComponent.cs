using System;
using System.Collections.Generic;
using _Source.Services;
using _Source.SignalsEvents.SavingEvents;
using UnityEngine;

namespace _Source.Saving_System
{
    public class ObjectsSaverComponent : MonoBehaviour
    {
        private const string NameData = "ObjectsData";
        private ObjectsData _data;
        private void Start()
        {
            Signals.Get<OnSaving>().AddListener(SaveData);
            Signals.Get<OnSaveStateObject>().AddListener(AddObject);
            if (PlayerPrefs.HasKey(NameData))
            {
                var nameSave = NameData;
                var data = PlayerPrefs.GetString(nameSave);
                if (data.Length != 0)
                {
                    var currentdata = JsonUtility.FromJson<ObjectsData>(data);
                    foreach (var obj in currentdata.deletedObjects)
                    {
                        Signals.Get<OnLoadStateObject>().Dispatch(obj);
                    }
                }
            }
            else
            {
                _data = new ObjectsData();
                _data.deletedObjects = new List<int>();
            }
        }

        private void AddObject(GameObject obj) => _data.deletedObjects.Add(obj.GetHashCode());

        private void SaveData()
        {
            string dataJson = JsonUtility.ToJson(_data);
            PlayerPrefs.SetString(NameData, dataJson);
            PlayerPrefs.Save();
        }

        private void OnDestroy()
        {
            Signals.Get<OnSaving>().RemoveListener(SaveData);
            Signals.Get<OnSaveStateObject>().RemoveListener(AddObject);
        }
    }

    [Serializable]
    public struct ObjectsData
    {
        public List<int> deletedObjects;
    }
}