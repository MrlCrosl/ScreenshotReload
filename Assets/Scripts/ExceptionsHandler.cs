using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExceptionsHandler : MonoBehaviour
{
    [SerializeField] private Canvas _exceptionCanvas;
    [SerializeField] private Image _exceptionImage; 

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string condition, string stacktrace, LogType type)
    {
        if (type == LogType.Exception || type == LogType.Error)
        {
            CaptureScreenshot();
        }
    }

    public void ThrowException()
    {
        throw new System.NotImplementedException();
    }

    private void CaptureScreenshot()
    {
        StartCoroutine(CaptureAndDisplay());
    }

    private System.Collections.IEnumerator CaptureAndDisplay()
    {
        Debug.LogWarning($"Soft  reload started...");
        yield return new WaitForEndOfFrame();
        
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenTexture.Apply();

        Sprite screenSprite = Sprite.Create(screenTexture, new Rect(0, 0, screenTexture.width, screenTexture.height),
            new Vector2(0.5f, 0.5f));
        
        _exceptionImage.sprite = screenSprite;

        SaveManager saveManager = FindFirstObjectByType<SaveManager>();
        saveManager.SaveAllStates();
        _exceptionCanvas.enabled = true;
        Debug.LogWarning($"Fade...");
        Color fadedColor = new Color(_exceptionImage.color.r * 0.5f, _exceptionImage.color.g * 0.5f, _exceptionImage.color.b * 0.5f, 1f);
        _exceptionImage.DOColor(fadedColor, 1f);
        yield return new WaitForSeconds(1f);
        Debug.LogWarning($"Scene reload...");
        SceneManager.LoadScene("Bootstrap", LoadSceneMode.Additive);
        yield return new WaitForSeconds(5f);
         saveManager = FindFirstObjectByType<SaveManager>();
         Debug.LogWarning("LoadAllStates");
        saveManager.LoadAllStates();
        Debug.LogWarning($"Loading old states...");
        _exceptionImage.DOColor(Color.white, 1f);
        yield return new WaitForSeconds(2f);
        _exceptionCanvas.enabled = false;
        Debug.LogWarning($"Ready...");

    }
}