using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CCB.Utility
{
    public class SceneController : Singleton<SceneController>
    {
        public string CORE_SCENE { get {return "Core_Scene";} }
        public string MAIN_SCENE { get {return "Main_Scene";} }

        float loadingProgress;
        Scene loadedSceneBefore;

        protected override void InitAfterAwake()
        {
            // LoadCoreScene();
        }

#if !UNITY_EDITOR
        void Start() 
        {
            OnLoadSceneAsync(MAIN_SCENE);
        }
#endif
        

        void LoadCoreScene()
        {
            SceneManager.LoadScene(CORE_SCENE,LoadSceneMode.Additive);
        }

        public void OnLoadSceneAsync(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        IEnumerator LoadSceneAsync(string sceneName)
        {
            yield return null;

            var asyncOparation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            asyncOparation.allowSceneActivation = false;

            while(!asyncOparation.isDone)
            {

                loadingProgress = Mathf.Clamp01(asyncOparation.progress / 0.9f);
                if (loadingProgress >= 0.9f)
                {
                    asyncOparation.allowSceneActivation = true;
                }

                yield return null;
            }
            
            yield return null;

            var loadedScene = SceneManager.GetSceneByName(sceneName);
            
            if(loadedScene.isLoaded)
            {
                SceneManager.SetActiveScene(loadedScene);
            }


            if(loadedSceneBefore.IsValid())
            SceneManager.UnloadSceneAsync(loadedSceneBefore);

            loadedSceneBefore = loadedScene;
        }
    }
}
