using UnityEngine;
using UnityEngine.AI;

public class ChangeWorld : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    public GameObject firstWorld;
    private Collider _firstSurfaceCollider;
    
    public GameObject secondWorld;
    private Collider _secondSurfaceCollider;

    public Renderer[] HairRenderers;
    public Renderer[] EyeRenderers;
    public Renderer[] SkinRenderers;
    public Renderer[] ArmorRenderers;

    public Material PhysicalHairMaterial;
    public Material PhysicalSkinMaterial;
    public Material PhysicalEyesMaterial;
    public Material PhysicalArmorMaterial;
    
    public Material SpiritalHairMaterial;
    public Material SpiritalSkinMaterial;
    public Material SpiritalEyesMaterial;
    public Material SpiritalArmorMaterial;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _firstSurfaceCollider = firstWorld.GetComponent<Collider>();
        _secondSurfaceCollider = secondWorld.GetComponent<Collider>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsAgentOnSurface(_firstSurfaceCollider))
            {
                ChangeMaterialToSpirital();
                TeleportToSurface(_secondSurfaceCollider);
                Debug.Log("Przeniesiono na drugie podłoże");
            }
            else
            {
                ChangeMaterialToPhysical();
                TeleportToSurface(_firstSurfaceCollider);
                Debug.Log("Przeniesiono na pierwsze podłoże");
            }
        }
    }
    
    bool IsAgentOnSurface(Collider surfaceCollider)
    {
        Vector3 closestPoint = surfaceCollider.ClosestPoint(_agent.transform.position);

        return Vector3.Distance(_agent.transform.position, closestPoint) < 2f;
    }
    
    void TeleportToSurface(Collider collider)
    {
        Vector3 closestPoint = collider.ClosestPoint(_agent.transform.position);
        
        Vector3 newPosition = new Vector3(_agent.transform.position.x, closestPoint.y, _agent.transform.position.z);
        
        _agent.Warp(newPosition);
    }

    void ChangeMaterialToPhysical()
    {
        foreach (Renderer rend in HairRenderers)
        {
            rend.material = PhysicalHairMaterial;
        }
        
        foreach (Renderer rend in EyeRenderers)
        {
            rend.material = PhysicalEyesMaterial;
        }
        
        foreach (Renderer rend in SkinRenderers)
        {
            rend.material = PhysicalSkinMaterial;
        }
        
        foreach (Renderer rend in ArmorRenderers)
        {
            rend.material = PhysicalArmorMaterial;
        }
    }

    void ChangeMaterialToSpirital()
    {
        foreach (Renderer rend in HairRenderers)
        {
            rend.material = SpiritalHairMaterial;
        }
        
        foreach (Renderer rend in EyeRenderers)
        {
            rend.material = SpiritalEyesMaterial;
        }
        
        foreach (Renderer rend in SkinRenderers)
        {
            rend.material = SpiritalSkinMaterial;
        }
        
        foreach (Renderer rend in ArmorRenderers)
        {
            rend.material = SpiritalArmorMaterial;
        }
    }
}