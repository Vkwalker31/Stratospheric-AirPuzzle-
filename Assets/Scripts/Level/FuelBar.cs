using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public Slider slider;
    public int fuel = 25;
    public int maxFuel = 100;

    float time = 0f;
    float delay;

    public PlayerFinishDeath other;

    void Awake()
    {
        fuel = 25;
        slider.minValue = 0;
        slider.maxValue = maxFuel;
        slider.value = fuel;
    }

    void Start()
    {
        fuel = 25;
        slider.minValue = 0;
        slider.maxValue = maxFuel;
        slider.value = fuel;
    }

    void Update()
    {
        slider.value = fuel;

        time += Time.deltaTime;
        if (time >= delay)
        {
            time = 0f;
            fuel--;
        }

        delay = Input.GetMouseButton(0) ? 1f : 0.5f;

        if (fuel <= 0)
        {
            slider.value = 0;
            other.OnDeath();
        }
    }

    public void AddFuel(int amount)
    {
        fuel += amount;
        if (fuel > maxFuel)
            fuel = maxFuel;
    }
}
