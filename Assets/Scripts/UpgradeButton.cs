using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public UpgradeCard upgrade; // Reference to the ScriptableObject containing upgrade info
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

    }

    private void Start()
    {
        // Add a listener to the button, which calls the ClickTest() method in the GameManager when clicked
        button.onClick.AddListener(GameManager.Instance.ClickTest);
    }
}
