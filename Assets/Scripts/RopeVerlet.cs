using System.Collections.Generic;
using UnityEngine;
using SpatialSys;
using SpatialSys.UnitySDK;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class RopeVerlet : MonoBehaviour
{

    public ManageRope.ropeType ropeType;
    public UnityEvent onIncorrect;
    public UnityEvent onCorrect;
    public bool complete;
    public GameObject interactable;

    [Header("Configuración de cuerda")]
    [SerializeField] private int numOfRopeSegments = 50; // número de puntos (N)
    [Tooltip("Longitud total inicial de la cuerda (metros). La cuerda comienza con esta longitud.")]
    [SerializeField] private float initialTotalLength = 1f; // longitud inicial (m)
    [Tooltip("Longitud máxima permitida (metros). La cuerda no se estirará más allá de esto).")]
    [SerializeField] private float maxTotalLength = 6f; // longitud máxima (m)
    [Tooltip("Velocidad a la que la cuerda cambia de longitud (m/s) - controla suavidad al estirar.")]
    [SerializeField] private float stretchSpeed = 2f;

    [Header("Fisica")]
    [SerializeField] private Vector3 gravityForce = new Vector3(0f, -9.81f, 0f);
    [SerializeField] private float dampingFactor = 0.99f;
    [SerializeField] private LayerMask collisionMask = ~0;
    [SerializeField] private float collisionRadius = 0.08f;
    [SerializeField] private float bounceFactor = 0.02f;

    [Header("Restricciones y performance")]
    [SerializeField] private int numOfConstraintRuns = 50;
    [SerializeField] private int collisionSegmentInterval = 2;
    [SerializeField] private float correctionClampAmount = 0.5f;

    // Anclas (asignar por inspector o por código)
    public Transform firstConstraintPos;    // ancla inicial (ya lo tenías)
    private Transform endConstraintPos = null; // ancla final (se asigna con AttachEnd)

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();

    // Longitud por segmento actual (se actualiza si la cuerda se estira)
    private float currentSegmentLength;

    // Nota: si tienes N puntos (numOfRopeSegments), la cantidad de intervalos entre puntos es (N-1).
    // longitudTotal = currentSegmentLength * (numOfRopeSegments - 1)

    public struct RopeSegment
    {
        public Vector3 currentPosition;
        public Vector3 oldPosition;

        public RopeSegment(Vector3 pos)
        {
            currentPosition = pos;
            oldPosition = pos;
        }
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        InitializeRope();
    }

    private void InitializeRope()
    {
        ropeSegments.Clear();
        // Calcula longitud por segmento a partir de la longitud total inicial
        int intervals = Mathf.Max(1, numOfRopeSegments - 1);
        float clampedInitial = Mathf.Clamp(initialTotalLength, 0.001f, maxTotalLength);
        currentSegmentLength = clampedInitial / intervals;

        // Inicia puntos en vertical (por debajo del objeto)
        Vector3 ropeStartPoint = transform.position;
        for (int i = 0; i < numOfRopeSegments; i++)
        {
            ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= currentSegmentLength;
        }

        lineRenderer.positionCount = ropeSegments.Count;
    }

    private void Update()
    {
        DrawRope();

    }

    private void FixedUpdate()
    {
        // Si hay ancla final, calcula la longitud objetivo entre anclas
        if (endConstraintPos != null && firstConstraintPos != null)
        {
            float distanceBetweenAnchors = Vector3.Distance(firstConstraintPos.position, endConstraintPos.position);
            float targetTotalLength = Mathf.Clamp(distanceBetweenAnchors, initialTotalLength, maxTotalLength);
            int intervals = Mathf.Max(1, ropeSegments.Count - 1);
            float targetSegmentLength = targetTotalLength / intervals;

            // ajusta currentSegmentLength suavemente hacia el target (movimiento por segundo)
            currentSegmentLength = Mathf.MoveTowards(currentSegmentLength, targetSegmentLength, stretchSpeed * Time.fixedDeltaTime);

            // Opcional: si el anchor final está más cerca que la longitud actual, tambien se puede contraer
            // (Mathf.MoveTowards ya maneja contraer si target < current)
        }
        // Si no hay ancla final pero la cuerda ha sido estirada antes, puede volver gradualmente a initialTotalLength
        else
        {
            int intervals = Mathf.Max(1, ropeSegments.Count - 1);
            float targetTotalLength = initialTotalLength;
            float targetSegmentLength = Mathf.Clamp(targetTotalLength / intervals, 0.0001f, maxTotalLength);
            currentSegmentLength = Mathf.MoveTowards(currentSegmentLength, targetSegmentLength, stretchSpeed * Time.fixedDeltaTime);
        }

        Simulate();

        for (int i = 0; i < numOfConstraintRuns; i++)
        {
            ApplyConstraints();

            //if (i % collisionSegmentInterval == 0)
            //{
            //    HandleCollisions();
            //}
        }
    }

    private void DrawRope()
    {
        Vector3[] positions = new Vector3[ropeSegments.Count];
        for (int i = 0; i < ropeSegments.Count; i++)
            positions[i] = ropeSegments[i].currentPosition;

        lineRenderer.SetPositions(positions);
    }

    private void Simulate()
    {
        float dt = Time.fixedDeltaTime;
        float dt2 = dt * dt;

        for (int i = 0; i < ropeSegments.Count; i++)
        {
            RopeSegment seg = ropeSegments[i];
            Vector3 velocity = (seg.currentPosition - seg.oldPosition) * dampingFactor;

            seg.oldPosition = seg.currentPosition;
            seg.currentPosition += velocity;
            seg.currentPosition += gravityForce * dt2;

            ropeSegments[i] = seg;
        }

        // ANCLAJE: sincroniza tanto current como old para evitar "velocidades" artificiales
        if (firstConstraintPos != null && ropeSegments.Count > 0)
        {
            RopeSegment first = ropeSegments[0];
            first.currentPosition = firstConstraintPos.position;
            first.oldPosition = firstConstraintPos.position; // <- importante
            ropeSegments[0] = first;
        }

        if (endConstraintPos != null && ropeSegments.Count > 0)
        {
            int lastIdx = ropeSegments.Count - 1;
            RopeSegment last = ropeSegments[lastIdx];
            last.currentPosition = endConstraintPos.position;
            last.oldPosition = endConstraintPos.position; // <- sincronizar aquí evita "tirones" hacia dentro
            ropeSegments[lastIdx] = last;
        }
    }

    private void ApplyConstraints()
    {
        int count = ropeSegments.Count;
        if (count < 2) return;

        for (int i = 0; i < count - 1; i++)
        {
            RopeSegment a = ropeSegments[i];
            RopeSegment b = ropeSegments[i + 1];

            Vector3 delta = b.currentPosition - a.currentPosition;
            float dist = delta.magnitude;
            if (dist == 0f) continue;

            float error = dist - currentSegmentLength; // usa currentSegmentLength dinámico
            Vector3 correctionDir = delta / dist;
            Vector3 correction = correctionDir * error;

            // Limitar corrección para evitar teletransportes extremos
            if (correction.magnitude > correctionClampAmount)
                correction = correction.normalized * correctionClampAmount;

            // Si el primer segmento está anclado, no lo movemos
            if (i == 0 && firstConstraintPos != null)
            {
                b.currentPosition -= correction;
            }
            // Si el siguiente (b) es el último y está anclado a endConstraintPos, no mover b
            else if (i + 1 == count - 1 && endConstraintPos != null)
            {
                a.currentPosition += correction;
            }
            else
            {
                a.currentPosition += correction * 0.5f;
                b.currentPosition -= correction * 0.5f;
            }

            ropeSegments[i] = a;
            ropeSegments[i + 1] = b;
        }
    }

    private void HandleCollisions()
    {
        for (int i = 1; i < ropeSegments.Count; i++)
        {
            RopeSegment seg = ropeSegments[i];
            Vector3 velocity = seg.currentPosition - seg.oldPosition;

            Collider[] hits = Physics.OverlapSphere(seg.currentPosition, collisionRadius, collisionMask);
            foreach (Collider col in hits)
            {
                if (col == null) continue;

                Vector3 closest = col.ClosestPoint(seg.currentPosition);
                float distance = Vector3.Distance(seg.currentPosition, closest);

                if (distance < collisionRadius)
                {
                    Vector3 normal = (seg.currentPosition - closest).normalized;
                    if (normal == Vector3.zero)
                        normal = (seg.currentPosition - col.transform.position).normalized;

                    float penetration = collisionRadius - distance;
                    seg.currentPosition += normal * penetration;

                    // poca energia de rebote para cuerda realista
                    velocity = Vector3.Reflect(velocity, normal) * bounceFactor;
                }
            }

            seg.oldPosition = seg.currentPosition - velocity;
            ropeSegments[i] = seg;
        }
    }

    // -----------------------------
    // Métodos públicos para anclar
    // -----------------------------
    /// <summary>
    /// Ancla el extremo final de la cuerda al transform pasado (ej. la mano del player).
    /// </summary>
    public void AttachEnd()
    {
        ManageRope.instance.DetachAll();
        endConstraintPos = SpatialBridge.actorService.localActor.avatar.GetAvatarBoneTransform(HumanBodyBones.RightHand);

        // Si queremos que la cuerda "comience" a estirarse desde su estado actual,
        // podemos forzar que el último segmento coincida inmediatamente con el transform.
        if (endConstraintPos != null && ropeSegments.Count > 0)
        {
            RopeSegment last = ropeSegments[ropeSegments.Count - 1];
            last.currentPosition = endConstraintPos.position;
            last.oldPosition = endConstraintPos.position;
            ropeSegments[ropeSegments.Count - 1] = last;
        }

        ManageRope.instance.currenType = ropeType;
        interactable.SetActive(false);
    }

    public void AttachEndTransform(Transform t)
    {
        if (ManageRope.instance.currenType != ropeType)
        {
            onIncorrect.Invoke();
            return;
        }
        endConstraintPos = t;

        // Si queremos que la cuerda "comience" a estirarse desde su estado actual,
        // podemos forzar que el último segmento coincida inmediatamente con el transform.
        if (endConstraintPos != null && ropeSegments.Count > 0)
        {
            RopeSegment last = ropeSegments[ropeSegments.Count - 1];
            last.currentPosition = endConstraintPos.position;
            last.oldPosition = endConstraintPos.position;
            ropeSegments[ropeSegments.Count - 1] = last;
        }
        onCorrect.Invoke();
        complete = true;
        ManageRope.instance.Check();
    }

    /// <summary>
    /// Desancla el extremo final.
    /// </summary>
    public void DetachEnd()
    {
        endConstraintPos = null;
    }

    // -----------------------------
    // Debug visual opcional
    // -----------------------------
    private void OnDrawGizmosSelected()
    {
        if (ropeSegments == null || ropeSegments.Count == 0) return;
        Gizmos.color = Color.yellow;
        for (int i = 0; i < ropeSegments.Count; i++)
        {
            Gizmos.DrawWireSphere(ropeSegments[i].currentPosition, collisionRadius);
        }

        if (firstConstraintPos != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(firstConstraintPos.position, 0.02f);
        }

        if (endConstraintPos != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(endConstraintPos.position, 0.02f);
        }
    }
}