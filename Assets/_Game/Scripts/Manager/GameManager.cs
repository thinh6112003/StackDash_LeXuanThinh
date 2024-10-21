using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    private static GameManager instance;
    [SerializeField] private LevelSO levelSO;
    private Map currentMap;
    private Player player;
    private Vector3 beginPos;
    private List<AsyncOperation> sceneToLoad = new List<AsyncOperation>();
    [SerializeField] private LoadSceneUI loadSceneUI;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        InitGame();
    }
    private void InitGame()
    {
        Observer.AddListener(conststring.NEXTLEVEL, LoadNextLevel);
        sceneToLoad.Add(SceneManager.LoadSceneAsync(conststring.HOMESCENE, LoadSceneMode.Additive));
        sceneToLoad.Add(SceneManager.LoadSceneAsync
        (
            DataManager.Instance.dynamicData.CurrentEnviromentSceneName(), LoadSceneMode.Additive)
        );
        StartCoroutine(LoadingProgress());
    }
    private IEnumerator LoadingProgress()
    {
        while (!sceneToLoad[0].isDone || !sceneToLoad[1].isDone) yield return null;
        int idLevel = DataManager.Instance.dynamicData.currentIDLevel;
        Scene targetScene = SceneManager.GetSceneByName("Level"+ idLevel);
        SceneManager.SetActiveScene(targetScene);
        Observer.Noti(conststring.DONELOADSCENEASYNC);
        currentMap = Instantiate(levelSO.GetMapByLevelID(idLevel));
        currentMap.getDataMap(ref player.planeEndGame, ref beginPos);
        player.SetPos(beginPos);
        Invoke(nameof(UnloadScene), 4f);
    } 
    private void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(conststring.LOADSCENE));
    }
    private void LoadNextLevel()
    {
        UIManager.Instance.SetUIScene(UIManager.SceneUIType.GamePlayScene);
        StartCoroutine(LoadingLevel(DataManager.Instance.dynamicData.NextCurrentIDLevel()));
    }
    private IEnumerator LoadingLevel(int idLevel)
    {
        sceneToLoad = new List<AsyncOperation>();
        sceneToLoad.Add(SceneManager.LoadSceneAsync
        (
            DataManager.Instance.dynamicData.CurrentEnviromentSceneName(), LoadSceneMode.Additive)
        );
        Debug.Log(DataManager.Instance.dynamicData.CurrentEnviromentSceneName());
        while (!sceneToLoad[0].isDone) yield return null;
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName
            (
                "Level" + (idLevel-1).ToString()
            ));
        Destroy(currentMap);
        Scene targetScene = SceneManager.GetSceneByName("Level" + idLevel.ToString());
        SceneManager.SetActiveScene(targetScene);
        currentMap =Instantiate(levelSO.GetMapByLevelID(idLevel));
        currentMap.getDataMap(ref player.planeEndGame, ref beginPos);
        player.SetPos(beginPos);
        Observer.Noti(conststring.DONELOADNEXTLEVEL);
    }
    public void setPlayer(Player player)
    {
        this.player = player;
    }
}