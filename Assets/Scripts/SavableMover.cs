using UnityEngine;

public class SavableMover : MonoBehaviour, ISavable
{
    [SerializeField] private string _key;
    public string GetKey() => _key;

    
    public void SaveState()
    {
        Vector3 position = transform.position;
        PlayerPrefs.SetString(GetKey(), $"{position.x};{position.y};{position.z}");
    }

    public void LoadState()
    {
        string positionData = PlayerPrefs.GetString(GetKey(), null);
        if (!string.IsNullOrEmpty(positionData))
        {
            string[] coords = positionData.Split(';');
            if (coords.Length == 3)
            {
                float x = float.Parse(coords[0]);
                float y = float.Parse(coords[1]);
                float z = float.Parse(coords[2]);
                transform.position = new Vector3(x, y, z);
            }
        }
    }
    
    public void HandleClick()
    {
        Vector2 offset = new Vector2(Random.Range(-2f, 2f), Random.Range(2f, 2f));
        transform.position =  (Vector2) transform.position + offset;
    }
}