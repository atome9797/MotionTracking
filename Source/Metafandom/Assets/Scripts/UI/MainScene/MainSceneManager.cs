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
            // �� ���۵ɶ� �ν��Ͻ� �ʱ�ȭ, ���� �Ѿ���� �����Ǳ����� ó��
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // instance��, GameManager�� �����Ѵٸ� GameObject ���� 
        	Destroy(this.gameObject);
        }
    }
    
	// Public ������Ƽ�� �����ؼ� �ܺο��� private ��������� ���ٸ� �����ϰ� ����
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
    /// ���� ������ �ε��ϰ� Root������Ʈ�� ����.
    /// ���� ������ Root������Ʈ ��Ʈ��.
    /// </summary>
    /// <param name="sceneName">�ҷ��� �� �̸�</param>
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
