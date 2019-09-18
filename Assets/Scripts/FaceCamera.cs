using UnityEngine;

[ExecuteInEditMode]
public class FaceCamera : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    private void Update()
    {
        var camForward = camera.transform.forward;
        transform.LookAt(transform.position + (camForward * -1f));
        transform.Rotate(0f, 180f, 0f);
    }
}
