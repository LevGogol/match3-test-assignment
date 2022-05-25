using UnityEngine.SceneManagement;

public class LoadSceneButtonModel
{
    private string _nextSceneName;
    
    public LoadSceneButtonModel(string nextSceneName)
    {
        _nextSceneName = nextSceneName;
    }
    
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(_nextSceneName);
    }
}
