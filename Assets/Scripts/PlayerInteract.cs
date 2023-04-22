using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public GameObject btnContainer;
   // public GameObject Dialoguebox;

    //private NPCInteractable npcInteractable;
    public float radius = 1f;

    // Start is called before the first frame update
    public void Start()
    {
        

        
    }
    // Update is called once per frame
    public void Update()
    {
        //NPC = GameObject.FindGameObjectWithTag("NPC").transform;
        Vector3 position = transform.position;

        Collider[] colliders = Physics.OverlapSphere(position, radius);
        btnContainer.SetActive(false);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out NPCInteractable nPCInteractable))
            {
                btnContainer.SetActive(true);
                //Dialoguebox.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    nPCInteractable.InteractUI();
                    nPCInteractable.PlayRandomAudio();

                }
                /*//Debug.Log("work");
                Vector3 direction = position - collider.transform.position;
                direction.y = 0f;
                Quaternion rotation = Quaternion.LookRotation(direction);
                collider.transform.rotation = Quaternion.Slerp(collider.transform.rotation, rotation, rotationSpeed * Time.deltaTime);*/
                nPCInteractable.Interact();
                
            }

        }        
    }
    private void OnDrawGizmosSelected()
    {
        // Draw the detection range sphere in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public void OnClick()
    {
        
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable nPCInteractable))
            {
               
                //return nPCInteractable;
                nPCInteractable.InteractUI();
                nPCInteractable.PlayRandomAudio();
            }
        }
    }
}
