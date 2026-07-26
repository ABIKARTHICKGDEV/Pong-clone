using UnityEngine;

public class SwipeInput : MonoBehaviour {
    [Header("Swipe Settings")]
    [SerializeField] private float swipeThreshold = 50f;

    private Vector2 startPosition;
    private Vector2 endPosition;

    public bool SwipeUp { get; private set; }
    public bool SwipeDown { get; private set; }

    private void Update() {
        // Reset every frame
        SwipeUp = false;
        SwipeDown = false;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

        // Mouse support (works in Editor and WebGL)
        if (Input.GetMouseButtonDown(0)) {
            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) {
            endPosition = Input.mousePosition;
            DetectSwipe();
        }

#endif

        // Android/iOS Touch Support
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase) {
                case TouchPhase.Began:
                    startPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endPosition = touch.position;
                    DetectSwipe();
                    break;
            }
        }
    }

    private void DetectSwipe() {
        Vector2 delta = endPosition - startPosition;

        // Ignore small movements
        if (Mathf.Abs(delta.y) < swipeThreshold)
            return;

        if (delta.y > 0) {
            SwipeUp = true;
        } else {
            SwipeDown = true;
        }
    }
}