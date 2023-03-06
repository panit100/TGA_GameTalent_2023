using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class EditorAutoLoadScene
{
    static EditorAutoLoadScene()
    {
        EditorSceneManager.sceneOpened += OnSceneOpened;
    }

    private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
    {
        if (EditorApplication.isPlaying)
        {
            return;
        }

        if (scene.name != "Core_Scene")
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Core_Scene.unity", OpenSceneMode.Additive);
        }
    }
}