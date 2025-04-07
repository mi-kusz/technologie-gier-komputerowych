using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    public float angleX;
    public float currentAngle;
    public float distance;
    
    public Transform target;
    public float smoothSpeed;
    public float rotationSpeed;

    void LateUpdate()
    {
        if (!target)
            return;
        
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            float mouseX = Input.GetAxis("Mouse X");
            currentAngle += mouseX * rotationSpeed * Time.deltaTime;
        }
        
        float radX = Mathf.Deg2Rad * angleX;
        float radY = Mathf.Deg2Rad * currentAngle;
        
        Vector3 offset = new Vector3(
            distance * Mathf.Cos(radY) * Mathf.Cos(radX),
            distance * Mathf.Sin(radX),
            distance * Mathf.Sin(radY) * Mathf.Cos(radX)
        );

        Vector3 desiredPosition = target.position + offset;
        
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        
        transform.rotation = Quaternion.Euler(angleX, currentAngle, 0);
        
        transform.LookAt(target.position);
    }
}