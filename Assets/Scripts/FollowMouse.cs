using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    public Vector3 mousePosition;
    public Cursor cursor;
    public Texture2D newCursor;

    void Start() {
        Cursor.SetCursor(newCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    void FixedUpdate() {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90f);
    }
}
