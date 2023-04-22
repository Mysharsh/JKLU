using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    private Animator animator;
    private Transform player;
    public float rotationSpeed = 5f;
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    public void Start()
    {
       // Debug.Log("work");
        
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void InteractUI()
    {
        //Debug.Log("work");
        //ChatBubble3D.Create(transform.transform, new Vector3(-.3f, 1.7f, 0f), ChatBubble3D.IconType.Happy, "Hello there!");
       
        animator.SetBool("Hand",true);
        
        //Debug.Log("Set");
        //float playerHeight = 1.7f;
        // npcHeadLookAt.LookAtPosition(interactorTransform.position + Vector3.up * playerHeight);

    }
    public void Interact()
    {
        Vector3 direction = player.position - transform.position;
        //Calculate rotation towards player
       Quaternion rotation = Quaternion.LookRotation(direction);
        //Set NPC's rotation to look at player
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    public void PlayRandomAudio()
    {
        int clipIndex = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[clipIndex];
        audioSource.Play();
    }


}
