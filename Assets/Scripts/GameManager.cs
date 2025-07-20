using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button _reloadButton;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private NPCDeathChecker _npc;
    [SerializeField] private float _delayToSetWin = 1f;
    private void Start()
    {
        _reloadButton.onClick.AddListener(Reload);
        _npc.OnDroppedOut += SetWin;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public async void SetWin()
    {
        await Awaitable.WaitForSecondsAsync(_delayToSetWin);
        Cursor.lockState = CursorLockMode.None;
        _winPanel?.SetActive(true);
        Time.timeScale = 0;
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
