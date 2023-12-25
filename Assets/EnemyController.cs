using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = -5.0f;
    private bool isLeftWalled = false;
    private bool isRightWalled = false;
    public LayerMask leftWallLayer;
    public LayerMask rightWallLayer;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            var mask = (1 << collider.gameObject.layer);
            if ((mask & leftWallLayer) != 0)
            {
                isLeftWalled = true;
            }
            if ((mask & rightWallLayer) != 0)
            {
                isRightWalled = true;
            }
        }
        if (isLeftWalled | isRightWalled) 
        {
            speed *= -1;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        var delta = new Vector3(speed, 0, 0);
        var pos = this.transform.position;
        pos += delta * Time.deltaTime;
        transform.position = pos;
        isLeftWalled = false;
        isRightWalled = false;
    }
}
