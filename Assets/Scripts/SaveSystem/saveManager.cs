using UnityEngine;
using UnityEngine.SceneManagement;

public class saveManager : MonoBehaviour
{
    public static saveManager Instance;

    void Awake() {
    if (Instance == null) {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    } else {
        Destroy(gameObject);
    }
}


    public void SaveGame()
    {
        // Save current level
        PlayerPrefs.SetInt("savedScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
    }

   public void LoadGame()
{
    int sceneIndex = PlayerPrefs.GetInt("savedScene", -1); // default to -1 or an invalid scene index
    if (sceneIndex >= 0) {
        SceneManager.LoadScene(sceneIndex);
    } else {
        Debug.LogError("Invalid scene index");
    }
}


    public bool doesSaveDataExist()
    {
        return PlayerPrefs.HasKey("savedScene");
    }
}
