using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowMousePosition : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        Color color = spriteRenderer.color;
        color.a = 0.4f;
        spriteRenderer.color = color;
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        transform.position = mainCamera.ScreenToWorldPoint(position);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
