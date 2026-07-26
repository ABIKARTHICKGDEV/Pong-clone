using UnityEngine;

public class playercontroller1 : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 downmost;
    private Vector3 upmost;
    private bool paddlePause;
    private float minY, maxY;
    private float halfHeight;
    private Ball ball;

    
    private DragInput dragInput;

    void Start()
    {
        dragInput = FindFirstObjectByType<DragInput>();
        // Find the Ball object
        ball = Object.FindAnyObjectByType<Ball>(); // Updated to use the new Unity API
        if (ball == null)
        {
            Debug.LogError("Ball object not found!");
        }

        // Calculate paddle height
        halfHeight = GetComponent<SpriteRenderer>().sprite.bounds.extents.y;

        // Calculate movement boundaries
        float distance = transform.position.z - Camera.main.transform.position.z;
        downmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        upmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        minY = downmost.y + (5 * halfHeight);
        maxY = upmost.y - (5 * halfHeight);

        Debug.Log($"Paddle bounds set: minY = {minY}, maxY = {maxY}");
    }

    void Update()
    {
        if (gameObject.CompareTag("Player"))
        {
            MovePlayerPaddle();
        }
        else if (gameObject.CompareTag("Player2"))
        {
            if (!paddlePause)
            {
                MoveComputerPaddle();
            }
        }
    }

    void MovePlayerPaddle()
    {
        bool isMoving = false;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MovePlayerUp();
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            MovePlayerDown();
            isMoving = true;
        }

        // Drag Input
        if (dragInput != null) {
            if (dragInput.MoveUp) {
                MovePlayerUp();
                isMoving = true;
            }

            if (dragInput.MoveDown) {
                MovePlayerDown();
                isMoving = true;
            }
        }


        if (isMoving)
        {
            float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
            transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
        }
    }

    void MoveComputerPaddle()
    {
        if (ball == null) return; // Ensure ball is valid

        float paddlePosY = transform.position.y;

        // Handle pause logic
        if (paddlePosY >= ball.ballposy && paddlePosY <= ball.ballposy + 0.15f)
        {
            paddlePause = true;
            Invoke(nameof(PaddleDelay), 0.05f);
            return;
        }
        else if (paddlePosY <= ball.ballposy && paddlePosY >= ball.ballposy - 0.15f)
        {
            paddlePause = true;
            Invoke(nameof(PaddleDelay), 0.05f);
            return;
        }

        // Move towards the ball
        if (paddlePosY < ball.ballposy)
        {
            MovePlayerUp();
        }
        else
        {
            MovePlayerDown();
        }

        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    public void MovePlayerUp()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    public void MovePlayerDown()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    void PaddleDelay()
    {
        paddlePause = false;
    }
}
