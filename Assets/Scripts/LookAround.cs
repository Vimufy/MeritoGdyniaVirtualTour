using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class LookAround : MonoBehaviour
{
    public float sensitivity = 0.1f;
    float horizontalRotation;
    float verticalRotation;
    bool isRotating;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            isRotating = false;
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
            Ray ray = cam.ScreenPointToRay(mouse.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Hotspot hotspot = hit.collider.GetComponentInParent<Hotspot>();
                if (hotspot != null)
                {
                    hotspot.Click();
                    return;
                }
            }
            isRotating = true;
        }

        if (mouse.leftButton.wasReleasedThisFrame) isRotating = false;

        if (isRotating)
        {
            Vector2 delta = mouse.delta.ReadValue();
            horizontalRotation += delta.x * sensitivity;
            verticalRotation -= delta.y * sensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);
            transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
        }

        float scroll = mouse.scroll.ReadValue().y;
        if (scroll != 0f)
        {
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - Mathf.Sign(scroll) * 5f, 40f, 100f);
        }
    }
}