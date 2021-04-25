using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private Image[] lifeImages;
    [SerializeField] private Sprite[] lifeBackgrounds;
    [SerializeField] private Image background;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //Allow game settings to correctly populate before setting the lives counter, there's probably a better way to do this but time
        LeanTween.delayedCall(0.01f, PopulateLives);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (i >= gameManager.currentLives)
            {
                lifeImages[i].enabled = false;
            }
        }

        background.sprite = lifeBackgrounds[gameManager.currentLives];
    }

    private void PopulateLives()
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (i >= GameSettings.lives)
            {
                lifeImages[i].enabled = false;
            }
        }
    }
}
