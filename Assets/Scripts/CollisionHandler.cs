using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Iniciando");
                break;
            case "Fuel":
                Debug.Log("Recargando");
                break;
            case "Finish":
                Debug.Log("Llegaste");
                break;
        }
    }
}
