using Unity.VisualScripting;
using UnityEngine;

public class restartButton : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    public void restart()
    {
        levelManager = GameObject.FindWithTag("ParentOfHearts").GetComponent<LevelManager>();
        levelManager.restart();
    }
}
