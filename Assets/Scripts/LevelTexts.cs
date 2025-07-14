using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelTexts : MonoBehaviour
{
    public Text ingametext;

    bool text1played = false;
    bool text3played = false;
    bool text6played = false;
    bool text7played = false;
    bool text8played = false;

    void Start()
    {
        ingametext.text = "";
        ingametext.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Trigger1" && !text1played)
        {
            text1played = true;
            StartCoroutine(ShowTextWithoutPause("������� �� ���, ����� ������ ��������"));
        }

        if (other.name == "Trigger2" && !text6played)
        {
            text6played = true;
            StartCoroutine(ShowDelayedPauseText("�� ��������� ��������� ������� �� �����.", 2f));
        }
        if (other.name == "Trigger3" && !text7played)
        {
            text7played = true;
            StartCoroutine(ShowDelayedPauseText("���������� �� ������ ����������, ���� �� ����������� �������.", 2f));
        }
        if (other.name == "Trigger4" && !text8played)
        {
            text8played = true;
            StartCoroutine(ShowDelayedPauseText("��������� ������������ � �������.", 2f));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Trigger1" && !text3played)
        {
            text3played = true;
            StopAllCoroutines();
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(ShowTextSequence());
            }
        }
    }

    private IEnumerator ShowTextWithoutPause(string message)
    {
        ingametext.text = message;
        ingametext.enabled = true;
        while (!AnyInputDown())
            yield return null;

        ingametext.text = "";
        ingametext.enabled = false;
    }

    private IEnumerator ShowDelayedPauseText(string message, float delaySeconds)
    {
        float timer = 0f;
        while (timer < delaySeconds)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        Time.timeScale = 0f;

        ingametext.text = message;
        ingametext.enabled = true;

        while (!AnyInputDown())
            yield return null;

        ingametext.text = "";
        ingametext.enabled = false;

        Time.timeScale = 1f;
    }

    private IEnumerator ShowTextSequence()
    {
        yield return ShowDelayedPauseText("����������� ���, ����� ������ �� ������������� ��������.", 2f);
        yield return ShowDelayedPauseText("������������� �������� ��������� ������ �������, �� ������� �������� �����.", 2f);
        yield return ShowDelayedPauseText("��������� ������� ���, ����������� ��� ������.", 2f);
        yield return ShowDelayedPauseText("���������� ���������� �������, �������� ��������.", 2f);
    }

    private bool AnyInputDown()
    {
        return Input.anyKeyDown
               || Input.GetMouseButtonDown(0)
               || Input.GetMouseButtonDown(1)
               || Input.GetKeyDown(KeyCode.Space)
               || Input.GetKeyDown(KeyCode.Return);
    }
}