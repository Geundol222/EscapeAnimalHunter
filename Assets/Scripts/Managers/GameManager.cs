using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static DataManager data;
    private static PoolManager pool;
    private static ResourceManager resource;
    private static SceneManager scene;
    private static SoundManager sound;

    public static GameManager Instance { get { return instance; } }
    public static DataManager Data { get { return data; } }
    public static PoolManager Pool { get { return pool; } }
    public static ResourceManager Resource { get { return resource; } }
    public static SceneManager Scene { get { return scene; } }
    public static SoundManager Sound { get { return sound; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        InitManagers();
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    private void InitManagers()
    {
        GameObject resourceObj = new GameObject();
        resourceObj.name = "ResourceManager";
        resourceObj.transform.parent = transform;
        resource = resourceObj.AddComponent<ResourceManager>();

        GameObject dataObj = new GameObject();
        dataObj.name = "DataManager";
        dataObj.transform.parent = transform;
        data = dataObj.AddComponent<DataManager>();

        GameObject poolObj = new GameObject();
        poolObj.name = "PoolManager";
        poolObj.transform.parent = transform;
        pool = poolObj.AddComponent<PoolManager>();

        GameObject sceneObj = new GameObject();
        sceneObj.name = "SceneManager";
        sceneObj.transform.parent = transform;
        scene = sceneObj.AddComponent<SceneManager>();

        GameObject soundObj = new GameObject();
        soundObj.name = "SoundManager";
        soundObj.transform.parent = transform;
        sound = soundObj.AddComponent<SoundManager>();
    }
}