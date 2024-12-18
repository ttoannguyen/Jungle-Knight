using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset = -0.5f;

    private Camera cam;
    private Vector2 startPos;
    private Vector2 travel => (Vector2)cam.transform.position - startPos;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = startPos + travel * parallaxOffset;
    }
}
