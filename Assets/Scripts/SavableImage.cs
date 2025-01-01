using UnityEngine;
using UnityEngine.UI;

public class SavableImage : MonoBehaviour, ISavable
{
    [SerializeField] private string _key;
    [SerializeField] private Image _image;

    public string GetKey() => _key;
    
    public void SaveState()
    {
        if (_image.sprite != null)
        {
            PlayerPrefs.SetString(GetKey(), _image.sprite.name);
        }
    }

    public void LoadState()
    {
        string spriteName = PlayerPrefs.GetString(GetKey(), null);
        if (!string.IsNullOrEmpty(spriteName))
        {
            Sprite loadedSprite = Resources.Load<Sprite>(spriteName);
            if (loadedSprite != null)
            {
                _image.sprite = loadedSprite;
            }
        }
    }

    public void ExecuteAction()
    {
        _image.sprite = Resources.Load<Sprite>(Random.Range(1,4).ToString());
    }
    
}