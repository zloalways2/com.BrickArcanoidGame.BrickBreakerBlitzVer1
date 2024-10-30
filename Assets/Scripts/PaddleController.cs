using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float screenBoundary;
    private Vector3 startPosition;
    private bool isDragging = false;
    private bool canMove = false;

    void Start()
    {
        float halfPaddleWidth = transform.localScale.x / 2;
        screenBoundary = Camera.main.aspect * Camera.main.orthographicSize - halfPaddleWidth;
        startPosition = transform.position;
    }

    void Update()
    {
        if (isDragging)
        {
            MovePaddleWithMouse();
        }
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;
    }

    void OnMouseDown()
    {
        if (canMove)
        {
            isDragging = true;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        // Не сбрасываем позицию сразу, а делаем это при запуске шарика
    }

    void MovePaddleWithMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float clampedX = Mathf.Clamp(mousePosition.x, -screenBoundary, screenBoundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    public void ResetPosition()
    {
        canMove = false;
        isDragging = false;
        transform.position = startPosition;
    }
}