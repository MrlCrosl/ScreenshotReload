public interface ISavable
{
    string GetKey();
    void SaveState();
    void LoadState();
}