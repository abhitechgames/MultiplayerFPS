using UnityEngine;
using System.Collections;
using InfimaGames.LowPolyShooterPack;

public class PlayerHealthSystem : MonoBehaviour
{
    public float health = 100f;
    public float damage = 3f;

    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public enum PlayerType
    {
        Blue, Red
    }

    public PlayerType playerType;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            if (playerType == PlayerType.Blue && uiManager.blueLife <= 0)
                return;
            else if (playerType == PlayerType.Red && uiManager.redLife <= 0)
                return;

            health -= damage;

            if (playerType == PlayerType.Blue)
                uiManager.BlueHealthBarUpdate(health);
            else
                uiManager.RedHealthBarUpdate(health);

        }
        else if (other.collider.CompareTag("Bullet"))
        {
            health = 0;

            if (playerType == PlayerType.Blue)
                uiManager.BlueHealthBarUpdate(health);
            else
                uiManager.RedHealthBarUpdate(health);
        }

        if (health < 0)
        {
            // Die Animation
            // GetComponent<Movement>().enabled = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        // WaitForSeconds
        yield return new WaitForSeconds(2f);

        Respawn_Restart();
    }

    public void Respawn_Restart()
    {
        if (playerType == PlayerType.Blue && uiManager.blueLife <= 0)
            return;
        else if (playerType == PlayerType.Red && uiManager.redLife <= 0)
            return;
        // GetComponent<Movement>().enabled = true;

        ResetHealth();

        GetComponent<Collider>().enabled = true;
        transform.position = Vector3.zero;
    }
    public void ResetHealth()
    {
        health = 100f;

        if (playerType == PlayerType.Blue)
            uiManager.BlueHealthBarUpdate(health);
        else
            uiManager.RedHealthBarUpdate(health);

        uiManager.UpdateScore();
    }
}
