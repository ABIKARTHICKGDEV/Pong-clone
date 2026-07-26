using UnityEngine;

public class DragInput : MonoBehaviour {
    [SerializeField] private float deadZone = 5f;

    private float previousY;

    public bool MoveUp { get; private set; }
    public bool MoveDown { get; private set; }

    private void Update() {
        // Reset every frame
        MoveUp = false;
        MoveDown = false;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

        if (Input.GetMouseButtonDown(0)) {
            previousY = Input.mousePosition.y;
        }

        if (Input.GetMouseButton(0)) {
            float currentY = Input.mousePosition.y;

            if (currentY > previousY + deadZone)
                MoveUp = true;
            else if (currentY < previousY - deadZone)
                MoveDown = true;

            previousY = currentY;
        }

#endif

        // Mobile
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                previousY = touch.position.y;
            }

            if (touch.phase == TouchPhase.Moved) {
                float currentY = touch.position.y;

                if (currentY > previousY + deadZone)
                    MoveUp = true;
                else if (currentY < previousY - deadZone)
                    MoveDown = true;

                previousY = currentY;
            }
        }
    }
}