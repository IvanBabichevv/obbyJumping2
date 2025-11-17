using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    public static PetSpawner Instance;
    
    [Header("Ссылки")]
    [SerializeField] private Transform player;
    [SerializeField] private float spawnRadius = 2f;
    private int petIndex = 0;
    

    private readonly List<GameObject> activePets = new();
    
    void Awake() => Instance = this;

    private readonly Vector3[] offsets = new Vector3[]
    {
        new Vector3(1f, 0f, 1f),
        new Vector3(-1f, 0f, 1f),
        new Vector3(1f, 0f, -1f),
        new Vector3(-1f, 0f, -1f),
    };
    public void SpawnPet(PetItem petItem)
    {
        if (petItem == null || petItem.petPrefab == null)
        {
            Debug.LogWarning("нет данных о питомце");
            return;
        }
        
        
        if(petIndex >= offsets.Length)
            petIndex = 0;   
        print(petIndex);
        Vector3 baseOffset = offsets[petIndex % offsets.Length] * spawnRadius;
        Vector3 randomOffset = baseOffset + Vector3.zero;
        Vector3 spawnPos = player.position + randomOffset;
        
        GameObject pet = Instantiate(petItem.petPrefab, spawnPos, Quaternion.identity);
        pet.name = $"{petItem.PetName}_Pet";

        var follow = pet.AddComponent<PetFollow>();
        follow.SetTarget(player, randomOffset);
        
        activePets.Add(pet);
        petIndex++;
        
        Debug.Log($"🐾 Питомец {petItem.PetName} создан рядом с игроком");
    }

    public void DespawnPet(PetItem petItem)
    {
        if (petItem == null) return;
        
        GameObject pet = activePets.Find(p => p.name.StartsWith(petItem.PetName));
        if (pet != null)
        {
            Destroy(pet);
            activePets.Remove(pet);
            Debug.Log($"🐾 Питомец {petItem.PetName} создан рядом с игроком");
        }
    }
    
    
}
