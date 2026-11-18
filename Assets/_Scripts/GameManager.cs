using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;
    private void Awake()
    {
        if(Manager != null && Manager != this)
        {
            Destroy(this);
        }
        Manager = this;
        DontDestroyOnLoad(Manager);
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TimeManager.Manager.ToggleTimeStop();
        }
    }
}
