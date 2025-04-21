using UnityEngine;

[RequireComponent(typeof(Collider))] // Äëÿ 3D
// [RequireComponent(typeof(Collider2D))] // Äëÿ 2D
public class CardDragger : MonoBehaviour
{
    [Header("Drag Settings")]
    public float returnDuration = 0.3f;
    public float dragHeight = 0.5f;
    public float dropTolerance = 0.3f; 

    [Header("Drag Zone Reference")]
    public GameObject dragZone; 
    private Vector3 dragZoneSize;

    private Vector3 offset;
    private float zCoord;
    private bool isDragging = false;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Transform startParent;
    private bool isReturning = false;
    private float returnStartTime;
    private Vector3 returnStartPosition;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        startParent = transform.parent;

        if (dragZone != null)
        {
            Collider zoneCollider = dragZone.GetComponent<Collider>();
            dragZoneSize = zoneCollider.bounds.size;
        }
    }

    private void Update()
    {
        if (isReturning)
        {
            float progress = (Time.time - returnStartTime) / returnDuration;
            progress = Mathf.Clamp01(progress);

            transform.position = Vector3.Lerp(returnStartPosition, startPosition, progress);
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, progress);

            if (progress >= 1f)
            {
                isReturning = false;
                transform.position = startPosition;
                transform.rotation = startRotation;
                transform.parent = startParent;

                if (TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.isKinematic = false;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (isReturning) return;

        isDragging = true;
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();

        transform.position += Vector3.up * dragHeight;

        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging && !isReturning)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private void OnMouseUp()
    {
        if (!isDragging || isReturning) return;

        isDragging = false;

        bool isInDropZone = CheckDropZoneWithTolerance();

        if (isInDropZone)
        {
            Vector3 dropPosition = dragZone.transform.position;
            dropPosition.y += dragHeight; 

            startPosition = dropPosition;
            transform.parent = dragZone.transform;
        }

        StartReturn();
    }

    private bool CheckDropZoneWithTolerance()
    {
        if (dragZone == null) return false;

        Vector3 cardPos = transform.position;
        Vector3 zonePos = dragZone.transform.position;
        Vector3 zoneSize = dragZoneSize * (1f - dropTolerance);

        bool withinX = Mathf.Abs(cardPos.x - zonePos.x) < zoneSize.x / 2f;
        bool withinY = Mathf.Abs(cardPos.y - zonePos.y) < zoneSize.y / 2f;
        bool withinZ = Mathf.Abs(cardPos.z - zonePos.z) < zoneSize.z / 2f;

        return withinX && withinY && withinZ;
    }

    private void StartReturn()
    {
        isReturning = true;
        returnStartTime = Time.time;
        returnStartPosition = transform.position;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}