using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QnA> SoalJawab;
    public GameObject[] options;
    public int currentQuestions;

    public GameObject QuizPanel;
    public GameObject ResultPanel;

    public Text QuestionTxt;
    public Text scoreTxt;

    int totalQuestions = 0;
    public int score;

    private void Start() 
    {
        totalQuestions = SoalJawab.Count;
        ResultPanel.SetActive(false);
        generateQuestion();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        QuizPanel.SetActive(false);
        ResultPanel.SetActive(true);
        scoreTxt.text = score + "/" + totalQuestions;
    }

    public void correct()
    {
        score += 1;
        SoalJawab.RemoveAt(currentQuestions);
        generateQuestion();
    }

    public void wrong()
    {
        SoalJawab.RemoveAt(currentQuestions);
        generateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = SoalJawab[currentQuestions].Answers[i];

            if (SoalJawab[currentQuestions].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (SoalJawab.Count > 0)
        {
            currentQuestions = Random.Range(0, SoalJawab.Count);

            QuestionTxt.text = SoalJawab[currentQuestions].Question;
            SetAnswers();   
        }
        else
        {
            Debug.Log("quiz abis");
            GameOver();
        }
    }
}
