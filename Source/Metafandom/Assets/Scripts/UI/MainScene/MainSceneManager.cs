using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vimeo.Recorder;

public class MainSceneManager : MonoBehaviour
{
    private GameObject[] _rootObject;

    private LoadSceneParameters _parameters = new LoadSceneParameters { loadSceneMode = LoadSceneMode.Additive };

    public VimeoRecorder _recoder;

    private MainSceneManager() { }

  
    private static MainSceneManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            // 씬 시작될때 인스턴스 초기화, 씬을 넘어갈때도 유지되기위한 처리
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // instance가, GameManager가 존재한다면 GameObject 제거 
        	Destroy(this.gameObject);
        }
    }
    
	// Public 프로퍼티로 선언해서 외부에서 private 멤버변수에 접근만 가능하게 구현
    public static MainSceneManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }


    void Start()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        _rootObject = new GameObject[sceneCount];
        _recoder = transform.Find("[VimeoRecorder]").GetComponent<VimeoRecorder>();
        Model.CategoryModel.InitCategoryData();
        changeScene("Main_Feed");
    }   
    /// <summary>
    /// 씬이 없으면 로드하고 Root오브젝트를 저장.
    /// 씬이 있으면 Root오브젝트 컨트롤.
    /// </summary>
    /// <param name="sceneName">불러올 씬 이름</param>
    public void changeScene(string sceneName)
    {

        Scene NextScene = SceneManager.GetSceneByName(sceneName);
        foreach (GameObject rootObnject in _rootObject)
        {
            if (rootObnject == null)
                continue;
            rootObnject.SetActive(false);
        }
        if (NextScene.isLoaded)
        {
           
            _rootObject[NextScene.buildIndex].SetActive(true);
        }
        else
        {
            NextScene = SceneManager.LoadScene(sceneName, _parameters);
            SceneManager.sceneLoaded += FindRootObject;
        }
    }


    void FindRootObject(Scene scene, LoadSceneMode mode)
    {
        GameObject[] list = scene.GetRootGameObjects();

        foreach (GameObject gameObject in list)
        {
            if (gameObject.name == "Root")
            {
                _rootObject[scene.buildIndex] = gameObject;
                SceneManager.sceneLoaded -= FindRootObject;
                return;
            }
        }
    }

}
