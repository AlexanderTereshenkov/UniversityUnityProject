using UnityEngine;

public class GameplayHandler : MonoBehaviour
{
    [SerializeField] private CheckpointManager checkpointManager;
    [SerializeField] private ViewUIManager viewUIManager;
    [SerializeField] private RespawnManager respawnManager;

    private void Awake()
    {
        StartGame();
    }

    public void RestartGame(bool checkChekpoint)
    {
        Time.timeScale = 1;
        viewUIManager.HideAllViews();
        viewUIManager.ShowView(ViewType.Gameplay);
        ShowHideCursor(false);
        if (checkpointManager.CurrentCheckpoint == null || !checkChekpoint)
        {
            respawnManager.RestartObjects(true);
            return;
        }
        checkpointManager.ReloadCurrentCheckpoint();
    }

    public void LoseGame()
    {
        Time.timeScale = 0;
        ShowHideCursor(true);
        viewUIManager.GetView<LoseScreenPage>().Show();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        ShowHideCursor(false);
    }

    public void ShowHideCursor(bool isVisible)
    {
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isVisible;
    }

}
