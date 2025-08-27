using UnityEngine;

namespace Machine.Horse
{
    public class HomingTarget : MonoBehaviour
    {
       [Header("Movement Settings")]
    [SerializeField] private float pursuitSpeed = 5f;

    private Transform playerTarget;
    private Rigidbody2D rb;
    private Rigidbody2D playerRb; // Referencia al Rigidbody2D del jugador

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTarget = player.transform;
            playerRb = player.GetComponent<Rigidbody2D>(); // Obtener el Rigidbody del jugador
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("El script HomingTarget requiere un componente Rigidbody2D.");
        }
        
        float randomAdd = Random.Range(0.5f, 1.5f);
        pursuitSpeed  += randomAdd;
    }

    private void FixedUpdate()
    {
        if (playerTarget != null && playerRb != null)
        {
            Vector2 targetPosition = CalculateInterceptionPoint(playerTarget.position, playerRb.velocity, transform.position, pursuitSpeed);
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            rb.velocity = direction * pursuitSpeed;
        }
    }
    
    // Método para calcular el punto de intercepción
    private Vector2 CalculateInterceptionPoint(Vector2 targetPos, Vector2 targetVel, Vector2 myPos, float mySpeed)
    {
        Vector2 targetToMe = myPos - targetPos;
        float a = Vector2.Dot(targetVel, targetVel) - (mySpeed * mySpeed);
        float b = 2 * Vector2.Dot(targetVel, targetToMe);
        float c = Vector2.Dot(targetToMe, targetToMe);

        float discriminant = (b * b) - (4 * a * c);

        if (discriminant >= 0)
        {
            float t = Mathf.Max(0, (-b + Mathf.Sqrt(discriminant)) / (2 * a));
            return targetPos + targetVel * t;
        }

        return targetPos; // Si no puede interceptar, se dirige a la posición actual
    }
    }
}
