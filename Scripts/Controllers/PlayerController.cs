using UnityEngine.EventSystems;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;

    public Interactable focus;
    Camera cam;
    PlayerMotor motor;



    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();

        if (motor == null)
        {
            Debug.LogError("Motor component is not assigned in PlayerController.");
        }
    }

    void Update()
    {

        //Player won't play if inventoryUI is present
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        
        //Left mouse input
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);
                RemoveFocus();
                //Debug.Log("Right mouse button clicked in PlayerController");
                //Debug.Log("Ray hit object: " + hit.collider.gameObject.name); // Log the hit object's name
                //Debug.Log("Ray hit point: " + hit.point); // Log the point where the ray hits


                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }

                else
                {
                    // If clicked elsewhere, remove focus
                    RemoveFocus();
                }
            }
        }

        //Right mouse input
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right mouse button clicked");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {


                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }




            }
        }
    }

    void SetFocus(Interactable newFocus)
    {

        if (newFocus != focus)
        {

            if (focus != null)
                focus.onDefocused();


            focus = newFocus;
            motor.FollowTarget(newFocus);

        }



        newFocus.OnFocused(transform);
        // Update the motor to focus on the interactable without moving to its position

    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.onDefocused();


        focus = null;
        // Update the motor to stop following the target when focus is removed
        motor.StopFollowingTarget();
    }
}
