using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoadScene : MonoBehaviour
{
    [SerializeField] private LoadData loadData;
    void Start()
    {
        loadData.Load("AutoSave");
        SceneManager.LoadScene(1);
    }
}
