using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public Dialogue initialDialogue;
    public TMP_Text nameText;
    public TMP_Text sentenceText;
    public RectTransform dialogueBox;
    public float timeBetweenLetters = 0.05f;
    private float letterTimer;
    private float sentenceTimer;

    private Queue<Sentence> sentences;
    private int currentSentenceId = -1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<Sentence>();

        if (initialDialogue != null)
        {
            StartDialogue(initialDialogue);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        DialogueBoxIn();
        nameText.SetText(dialogue.name);

        sentences.Clear();

        foreach(Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            DialogueBoxOut();
        }
        else
        {
            Sentence sentence = sentences.Dequeue();
            sentenceTimer = 0;
            currentSentenceId++;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    public void SkipToSentence(int index)
    {
        Debug.Log("Skip");
        int sentencesToSkip = index - currentSentenceId - 1;

        for (int i = 0; i < sentencesToSkip; i++)
        {
            sentences.Dequeue();
            currentSentenceId++;
        }

        DisplayNextSentence();
    }

    IEnumerator TypeSentence(Sentence sentence)
    {
        sentenceText.SetText("");

        int i = 0;
        foreach (char letter in sentence.text.ToCharArray())
        {
            letterTimer = 0;
            string currentText = sentenceText.text;
            sentenceText.SetText(currentText + letter);

            yield return new WaitUntil(CanDisplayNextLetter);
        }
        yield return new WaitUntil(() =>
        {
            if (!GameManager.instance.gamePaused)
                sentenceTimer += Time.deltaTime;
            return !GameManager.instance.gamePaused && sentenceTimer > sentence.duration;
        });

        //yield return new WaitForSecondsRealtime(sentence.duration);

        if (!sentence.waitForInput)
        {
            DisplayNextSentence();
        }
    }

    private bool CanDisplayNextLetter()
    {
        letterTimer += Time.deltaTime;
        return !GameManager.instance.gamePaused && letterTimer > timeBetweenLetters;
    }

    public void DialogueBoxIn()
    {
        dialogueBox.DOAnchorPosY(dialogueBox.anchoredPosition.y - 200, 0.5f).SetEase(Ease.OutCubic);
    }

    public void DialogueBoxOut()
    {
        dialogueBox.DOAnchorPosY(dialogueBox.anchoredPosition.y + 200, 0.5f).SetEase(Ease.InCubic);
    }
}
