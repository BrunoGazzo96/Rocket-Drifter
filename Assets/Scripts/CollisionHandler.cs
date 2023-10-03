using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem successParticles;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable) { return; }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Iniciando");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }

    }

    private void Update()
    {
        //RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }

        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        explosionParticles.Play();
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(explosion);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(success);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
