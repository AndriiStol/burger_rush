using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName; // Имя сцены для загрузки

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName); // Загружаем сцену по имени
    }
}
