
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string sceneNameMenu = "Menu";
    [SerializeField] private string sceneNameTutorial = "Tutorial";
    [SerializeField] private string sceneNameLevel1 = "Level 1";

    //TODO: TP2 - SOLID
    [SerializeField] private EnemyController enemyController;

    [SerializeField] private Player player;
    [SerializeField] private Health playerHealth;
    [SerializeField] private int credits;
    

    public int Credits { get => credits; set => credits = value; }

    private void Start()
    {
        playerHealth = player.Health;
        playerHealth.wasDefeated += playerDefeat;
        enemyController.endRound += enemyDefeat;
    }

    private void playerDefeat(Health playerHealth)
    {
        SceneManager.LoadScene(sceneNameMenu, LoadSceneMode.Single);
    }

    private void enemyDefeat()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(sceneNameTutorial))
        {
            SceneManager.LoadScene(sceneNameLevel1, LoadSceneMode.Single);
        }
    }
}
