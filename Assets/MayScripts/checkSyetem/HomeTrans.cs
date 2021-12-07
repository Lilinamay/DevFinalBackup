using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeTrans : MonoBehaviour
{
    //private AsyncOperation sceneAsync;
    [SerializeField] Transform homePos;
    [SerializeField] GameObject player;
    [SerializeField] GameObject chair;
    public Vector3 myOriPos;
    HomeChair homeChair;
    public bool comeUnder = false;
    public GameObject Camera;
    Rigidbody2D mybody;

    // Start is called before the first frame update
    void Start()
    {
        homeChair = chair.GetComponent<HomeChair>();
        mybody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goHome()
    {
        myOriPos = player.GetComponent<checkManager>().collObject.transform.position;
        player.transform.position = homePos.position;
        mybody.velocity = Vector3.zero;
        homeChair.comeHome = true;
        //Object.DontDestroyOnLoad(this.gameObject);
        //SceneManager.LoadScene(1);
        //SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByBuildIndex(1));
        //StartCoroutine(loadScene(1));
    }

    public void goUnder()
    {
        comeUnder = true;
        player.transform.position = myOriPos;
        PlayerMove.Globals.CamFloorY = player.transform.position.y - 0.5f;
        mybody.velocity = Vector3.zero;
        //Camera.transform.position = new Vector3(myOriPos.x, myOriPos.y+4);



    }

 


    //IEnumerator loadScene(int index)
    //{
    //    AsyncOperation scene = SceneManager.LoadSceneAsync(index);
    //    scene.allowSceneActivation = false;
    //    sceneAsync = scene;

    //    //Wait until we are done loading the scene
    //    while (scene.progress < 0.9f)
    //    {
    //        Debug.Log("Loading scene " + " [][] Progress: " + scene.progress);
    //        yield return null;
    //    }
    //    OnFinishedLoadingAllScene();
    //}

    //void enableScene(int index)
    //{
    //    //Activate the Scene
    //    sceneAsync.allowSceneActivation = true;


    //    Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(index);
    //    if (sceneToLoad.IsValid())
    //    {
    //        Debug.Log("Scene is Valid");
    //        Object.DontDestroyOnLoad(this.gameObject);
    //        SceneManager.MoveGameObjectToScene(this.gameObject, sceneToLoad);
    //        SceneManager.SetActiveScene(sceneToLoad);
    //    }
    //}

    //void OnFinishedLoadingAllScene()
    //{
    //    Debug.Log("Done Loading Scene");
    //    enableScene(1);
    //    Debug.Log("Scene Activated!");
    //}
}
