using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivityX; 
    public float MouseSensitivityY;
    public bool MouseInvertY;

    // Player gameObject orientation
    private Transform _playerOrientation;

    private float _rotationX;
    private float _rotationY;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerOrientation = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        var mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * MouseSensitivityX;
        var mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * MouseSensitivityY;
        mouseY = MouseInvertY ? -mouseY : mouseY;

        _rotationY += mouseX;
        _rotationX += mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        // Rotate camera
        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        Debug.Log(transform.rotation);
        // Rotate Player gameObject
        _playerOrientation.rotation = Quaternion.Euler(0, _rotationY, 0);
    }
}
