using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public static MenuUI Instance { get; private set; }
    [SerializeField] private Slider healthBar;

    void Awake()
    {
        Instance = this;
        
        healthBar.maxValue = 200;
        healthBar.value = 200;
    }

    public void SetHealth(int value)
    {
        healthBar.value = value;
    }
}
