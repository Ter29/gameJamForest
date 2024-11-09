using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{   
    [SerializeField] private float moveSpeed = 4f;
    private float lookSpeed = 2f;
    private CharacterController controller;
    private float verticalRotation = 0f;
    [SerializeField] private Transform flashlight;
    private bool isGrounded;
    private Vector3 velocity;
    [SerializeField] private Text targetText; 
    [SerializeField] private Text counterText; 
    private bool isVisible = true;
    [SerializeField] private float interactDistance = 10f;
    private int counter = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        targetText.gameObject.SetActive(isVisible);
    }

    void Update()
    {
        Movement();
        UpdateFlashlight();
        Collect();
    }
    void LateUpdate()
    {
        counterText.text = counter.ToString() + "/4";
    }
    private void Movement()
    {
        if(Input.GetKey(KeyCode.LeftShift)){
            moveSpeed = 20f;
        }
        else{
            moveSpeed = 25f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        transform.Rotate(Vector3.up * mouseX);

        // Обмеження вертикального обертання
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -65f, 65f);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        isGrounded = controller.isGrounded;
        if(isGrounded && velocity.y < 1.2)
        {
            velocity.y = -40f;
        }
        controller.Move(velocity * Time.deltaTime);
    }
    private void UpdateFlashlight()
    {
        if (flashlight != null)
        {
            flashlight.rotation = Camera.main.transform.rotation;

            flashlight.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        }
    }
    private void Collect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {   
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != this.gameObject && hitObject.CompareTag("Collectible"))
            {
                
                float distanceToObject = Vector3.Distance(transform.position, hitObject.transform.position);
                if (distanceToObject <= interactDistance)
                {
                    targetText.gameObject.SetActive(true);

                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        counter ++;
                        CollectObject(hitObject);
                        
                        Debug.Log(counter);
                    }
                }
            }
            else
            {
                targetText.gameObject.SetActive(false);
            }
            
        }
        else
        {
            targetText.gameObject.SetActive(false);
        }
    }
    private void CollectObject(GameObject collectible)
    {
        collectible.SetActive(false);
    }
}