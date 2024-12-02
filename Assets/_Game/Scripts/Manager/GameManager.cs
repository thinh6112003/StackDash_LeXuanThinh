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
    private async void InitGame()
    {
        Observer.AddListener(conststring.NEXTLEVEL, LoadNextLevel);
        Observer.AddListener(conststring.RELOADLEVEL, ReLoadLevel);
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
        player.setDustColor(currentMap.GetColorDust().Item1, currentMap.GetColorDust().Item2);
        Invoke(nameof(UnloadScene), 4f);
    } 
    private void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(conststring.LOADSCENE));
    }
    private void LoadNextLevel()
    {
        StartCoroutine(LoadingLevel(DataManager.Instance.dynamicData.GetCurrentIDLevel()));
    }

    private void ReLoadLevel()
    {
        StartCoroutine(LoadingLevel(DataManager.Instance.dynamicData.GetCurrentIDLevel(),true));
    }
    private IEnumerator LoadingLevel(int idLevel, bool reload= false)
    {
        yield return new WaitForSeconds(0.3f);
        sceneToLoad = new List<AsyncOperation>();
        sceneToLoad.Add(SceneManager.LoadSceneAsync
        (
            DataManager.Instance.dynamicData.CurrentEnviromentSceneName(), LoadSceneMode.Additive)
        );

        while (!sceneToLoad[0].isDone) yield return null;
        Scene sceneUnLoad = SceneManager.GetSceneByName("Level" + (reload ? idLevel : idLevel == 1 ? 5: idLevel - 1).ToString());
        Destroy(currentMap);
        SceneManager.UnloadSceneAsync(sceneUnLoad);

        yield return null;
        Scene targetScene = SceneManager.GetSceneByName("Level" + idLevel.ToString());
        SceneManager.SetActiveScene(targetScene);
        currentMap =Instantiate(levelSO.GetMapByLevelID(idLevel));
        currentMap.getDataMap(ref player.planeEndGame, ref beginPos);
        player.SetPos(beginPos);
        player.setDustColor(currentMap.GetColorDust().Item1, currentMap.GetColorDust().Item2);
        UIManager.Instance.SetUIScene(UIManager.SceneUIType.HomeScene);
        Observer.Noti(conststring.DONELOADLEVEL);
    }
    public void setPlayer(Player player)
    {
        this.player = player;
    }
    public Material GetGroundMaterialCurrentMap()
    {
        return currentMap.GetGroundMaterial();
    }
}