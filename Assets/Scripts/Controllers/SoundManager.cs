using UnityEngine;

public class SoundManager : Singleton<SoundManager>  // Singleton class managing all sound effects and music
{
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip footsteps;
    [SerializeField] AudioClip bombPlacement;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip lossSound;
    [SerializeField] AudioClip gameplayMusic;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayLevelMusic()
    {
        audioSource.clip = gameplayMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayFootsteps()
    {
        audioSource.PlayOneShot(footsteps, 0.2f);
    }

    public void PlayBombPlacementSound()
    {
        audioSource.PlayOneShot(bombPlacement);
    }

    public void PlayExplosionSound()
    {
        audioSource.PlayOneShot(explosion);

    }

    public void PauseResumeMusic(bool shouldPlay)
    {
        if (shouldPlay)
            audioSource.UnPause();
        else
            audioSource.Pause();
    }

    public void PlayWinOrLose(bool hasWon)
    {
        audioSource.Stop();
        if (hasWon)
            audioSource.PlayOneShot(winSound);
        else
            audioSource.PlayOneShot(lossSound);
    }
}