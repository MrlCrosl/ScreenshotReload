using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SavableColor : MonoBehaviour, ISavable
{
    [SerializeField] private string _key;
    [SerializeField] private Image _image;

    public string GetKey() => _key;
    
    public void SaveState()
    {
        Color color = _image.color;
        PlayerPrefs.SetString(GetKey(), $"{color.r};{color.g};{color.b};{color.a}");
    }

    public void LoadState()
    {
        string colorData = PlayerPrefs.GetString(GetKey(), null);
        if (!string.IsNullOrEmpty(colorData))
        {
            string[] values = colorData.Split(';');
            if (values.Length == 4)
            {
                float r = float.Parse(values[0]);
                float g = float.Parse(values[1]);
                float b = float.Parse(values[2]);
                float a = float.Parse(values[3]);
                _image.color = new Color(r, g, b, a);
            }
        }
    }
    
    public void HandleClick()
    {
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        _image.color = color;
    }
    
    

}