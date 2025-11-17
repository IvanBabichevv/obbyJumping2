using UnityEngine;

namespace Player
{
    public class PlayersSound : MonoBehaviour
    {
        [SerializeField] private AudioClip baseStep;
        [SerializeField] private AudioClip grassStep;
        [SerializeField] private AudioClip waterStep;
        [SerializeField] private AudioClip desertStep;
        [SerializeField] private AudioClip landStep;
        [SerializeField] private AudioClip jumpBase;
        [SerializeField] private AudioClip jumpWater;
        [SerializeField] private ParticleSystem waterPart;

        [SerializeField] private float soundDelay;
        [SerializeField] private float rayDist = 1.25f;
        [SerializeField] private LayerMask mask;

        AudioClip currentClip;
        AudioClip currentJumpClip;

        AudioSource source;

        float currentSoundDelay;

        private void Start()
        {
            source = GetComponent<AudioSource>();
        }

        public void JumpSound()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, rayDist, mask))
            {
                switch (hit.collider.tag)
                {
                    case "Water":
                        currentJumpClip = jumpWater;
                        break;
                    default:
                        currentJumpClip = jumpBase;
                        break;
                }
            }

            source.pitch = Random.Range(0.7f, 1f);
            source.PlayOneShot(currentJumpClip);
        }

        public void SoundStep(bool isMoving)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, rayDist, mask))
            {
                switch (hit.collider.tag)
                {
                    case "Grass":
                        currentClip = grassStep;
                        break;
                    case "Desert":
                        currentClip = desertStep;
                        break;
                    case "Water":
                        currentClip = waterStep;
                        break;
                    case "Land":
                        currentClip = landStep;
                        break;
                    default:
                        currentClip = baseStep;
                        break;
                }
            }

            if (isMoving)
            {
                currentSoundDelay += Time.deltaTime;
                if (currentSoundDelay > soundDelay)
                {
                    source.pitch = Random.Range(0.7f, 1f);
                    source.PlayOneShot(currentClip);
                    if (waterPart)
                    {
                        if (currentClip == waterStep)
                        {
                            waterPart.Play();
                        }
                    }

                    currentSoundDelay = 0;
                }
            }
        }
    }
}