using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private bool flip;

    private float initialY;

    void Start()
    {
        // Store the initial y position
        initialY = transform.position.y;
    }

    void FixedUpdate()
    {
        float x = transform.position.x;
        float y;

        if (!flip)
        {
            // Calculate new y based on initial y position
            y = initialY + Mathf.Sin(Time.time * frequency) * amplitude;
        }
        else
        {
            // Calculate new y based on initial y position with frequency flipped
            y = initialY + Mathf.Sin(Time.time * -frequency) * amplitude;
        }

        // Update the platform position
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
