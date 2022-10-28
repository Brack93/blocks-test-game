using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public sealed class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    public void LoadScene(Scenes scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals(Scenes.HomeScene.ToString()))
        {
            TryPopulateScore(ScoreTags.Highscore);
        }
        else if (scene.name.Equals(Scenes.GameoverScene.ToString()))
        {
            TryPopulateScore(ScoreTags.Highscore);
            TryPopulateScore(ScoreTags.Score);
        }
    }

    private void TryPopulateScore(ScoreTags scoreTag)
    {
        if (TryGetTextObjectWithTag(scoreTag.ToString(), out TextMeshProUGUI textObj))
        {
            textObj.text = PlayerPrefs.GetInt(scoreTag.ToString(), 0).ToString();
        }
    }

    private bool TryGetTextObjectWithTag(string tag, out TextMeshProUGUI textObj)
    {
        textObj = null;
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        
        if (objects.Length == 0) return false;
        
        GameObject obj = objects[0];
        
        return obj.TryGetComponent<TextMeshProUGUI>(out textObj);
    }

}
