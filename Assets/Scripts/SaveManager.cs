using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private List<ISavable> _savableObjects;
    
    public void SaveAllStates()
    {
        FindSavableObjects();
        foreach (var savable in _savableObjects)
        {
            savable.SaveState();
        }
        PlayerPrefs.Save();
    }

    public void LoadAllStates()
    {
        FindSavableObjects();
        foreach (var savable in _savableObjects)
        {
            savable.LoadState();
        }
    }

    private void FindSavableObjects()
    {
        _savableObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<ISavable>()
            .ToList();
    }
}