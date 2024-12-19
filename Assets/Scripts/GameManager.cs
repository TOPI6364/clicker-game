using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    /// ниже с кликами и текст с кол во счета
    [SerializeField] private int score;
    [SerializeField] private int clickGain = 1;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int upgradeClickCost = 100;
    [SerializeField] private TMP_Text upgradeClickCostText;

    /// ниже с хп и текст с кол во хп
    [SerializeField] private TMP_Text helthText;
    [SerializeField] private int health;
    [SerializeField] private int damageGain = 1;
    [SerializeField] private int healCost = 100;

    /// ниже с картинками
    [SerializeField] Image Slime;
    [SerializeField] Sprite NormalSprite, DamagedSprite;
    [SerializeField][Range(0f, 10f)] float HealTime = 1f;
    [SerializeField]GameObject DeathScreen;


    private void Start()
    {
        StartCoroutine(HelthDamageCorutine());
    }

    private void Update()
    { 
        scoreText.text = "Your Score: " + score.ToString();
        helthText.text = "Your Health: " + health.ToString();
        upgradeClickCostText.text = "Цена: " + upgradeClickCost.ToString();
    }

    public void Click()
    {
        score += clickGain;
    }

    IEnumerator HelthDamageCorutine()
    {
        while (true)
        {
            health -= damageGain;
            if (health <= 0){DeathScreen.SetActive(true);}
            yield return new WaitForSeconds(2);
        }
    }

    public void upgradeClick()
    {
        if(score >= upgradeClickCost)
        {
            score -= upgradeClickCost;
            upgradeClickCost += 20;
            clickGain++;
        }
        else Debug.Log("Не хватает очков для улучшения!");        
    }

    public void heal()
    {
        if(score >= healCost)
        {
            score -= healCost;
            health = 100;
        }
        else Debug.Log("Не хватает очков для востановления здоровья!");  
    }

    public void exit()
    {
        SceneManager.LoadScene(0);
    }
    
    public void ClickMe()
    {
        if (_heal != null)
        {
            StopCoroutine(_heal);
        }
        Slime.sprite = DamagedSprite;
        _heal = Heal();
        StartCoroutine(_heal);
    }
    IEnumerator _heal;
    IEnumerator Heal()
    {
        yield return new WaitForSeconds(HealTime);
        Slime.sprite = NormalSprite;
        _heal = null;
    }

}