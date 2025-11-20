using UnityEngine;

public class FloatingTextSpawner : MonoBehaviour
{
    public static FloatingTextSpawner Instance;
    
    public GameObject floatingTextPrefab;
    public Canvas worldCanvas;
    public float offsetY = 2f;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnText()
    {
        if (floatingTextPrefab == null || worldCanvas == null)
            return;

        float increaseAmount = PlayerMovement.Instance.oneClick * PointsManager.Instance.CurrentCoefficient;
        float displayValue = increaseAmount * 200f;

        GameObject go = Instantiate(floatingTextPrefab, transform);

        // Получаем размеры экрана
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Рандомные координаты внутри экрана
        float randomX = Random.Range(0f, screenWidth - 400);
        float randomY = Random.Range(0f, screenHeight - 200);

        go.transform.position = new Vector3(randomX, randomY, 0);

        go.GetComponent<FloatingText>().SetText($"+{displayValue:0}");
    }
    
}

