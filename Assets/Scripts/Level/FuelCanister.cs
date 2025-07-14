using UnityEngine;

public class FuelCanister : MonoBehaviour
{
    public int fuelAmount = 20;
    public AudioSource pickupSound;

    private bool pickedUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pickedUp) return;

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<FuelBar>(out var fuelBar))
            {
                fuelBar.AddFuel(fuelAmount); 
            }

            PlayPickupSound();
            pickedUp = true;
            Destroy(gameObject);
        }
    }

    private void PlayPickupSound()
    {
        if (!pickupSound.isPlaying)
            pickupSound.Play();
    }
}