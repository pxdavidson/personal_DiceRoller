using System;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollLogic : MonoBehaviour
{
    // States
    bool gameStarted;

    // Declare Variables
    int successfulRolls;
    int toHit = 3;
    int dicePool = 0;
    int diceRolled;
    int totalOnes;
    int totalTwos;
    int totalThrees;
    int totalFours;
    int totalFives;
    int totalSixs;
    int rerollPool = 0;

    // Cache
    [SerializeField] Text rollOneText;
    [SerializeField] Text rollTwoText;
    [SerializeField] Text rollThreeText;
    [SerializeField] Text rollFourText;
    [SerializeField] Text rollFiveText;
    [SerializeField] Text rollSixText;
    [SerializeField] Text dicePoolText;
    [SerializeField] Text toHitText;
    [SerializeField] Text successText;

    // Called on start
    void Start()
    {
        UpdateToHitText();
        UpdateDicePoolText();
        UpdateDiceText();
    }
    
    // Rolls To Hit
    public void ToHitRoll()
    {
        ResetDice();
        ResetReroll();
        ResetSuccess();
        gameStarted = true;
    }

    // Rolls To Wound
    public void ToWoundRoll()
    {
        dicePool = successfulRolls;
        ResetDice();
        ResetReroll();
        ResetSuccess();
        UpdateDicePoolText();
        gameStarted = true;
    }

    // Reroll Ones
    public void RerollOnes()
    {
        rerollPool = totalOnes;
        totalOnes = 0;
        successfulRolls = 0;
        gameStarted = true;
    }

    // Reroll all misses
    public void RerollMisses()
    {
        if (toHit >= 2)
        {
            rerollPool = rerollPool + totalOnes;
            totalOnes = 0;
        }
        if (toHit >= 3)
        {
            rerollPool = rerollPool + totalTwos;
            totalTwos = 0;
        }
        if (toHit >= 4)
        {
            rerollPool = rerollPool + totalThrees;
            totalThrees = 0;
        }
        if (toHit >= 5)
        {
            rerollPool = rerollPool + totalFours;
            totalFours = 0;
        }
        if (toHit >= 6)
        {
            rerollPool = rerollPool + totalFives;
            totalFives = 0;
        }
        successfulRolls = 0;
        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted == true)
        {
            CheckDicePool();
        }
        else
        {
            // Do nothing
        }
    }

    // Checks the dice pool and rolled dice to decide what action to take
    private void CheckDicePool()
    {
        if (diceRolled < (dicePool + rerollPool))
        {
            RollDice();
            UpdateDiceText();
        }
        else if (diceRolled == (dicePool + rerollPool))
        {
            CalculateSuccessfulRolls();
            gameStarted = false;
        }
        else
        {
            // Do nothing
        }

    }

    // Rolls a D6
    private void RollDice()
    {
        diceRolled++;
        int rollResult = UnityEngine.Random.Range(1, 7);
        IncrementTotal(rollResult);
    }

    // Calculate success'
    private void CalculateSuccessfulRolls()
    {
        if (toHit <= 1)
        {
            successfulRolls = successfulRolls + totalOnes;
        }
        if (toHit <= 2)
        {
            successfulRolls = successfulRolls + totalTwos;
        }
        if (toHit <= 3)
        {
            successfulRolls = successfulRolls + totalThrees;
        }
        if (toHit <= 4)
        {
            successfulRolls = successfulRolls + totalFours;
        }
        if (toHit <= 5)
        {
            successfulRolls = successfulRolls + totalFives;
        }
        if (toHit <= 6)
        {
            successfulRolls = successfulRolls + totalSixs;
        }
        UpdateSuccessText();
    }

    // Increments the total of instances of a number rolled
    private void IncrementTotal(int rollResult)
    {
        if (rollResult == 1)
        {
            totalOnes++;
        }
        else if (rollResult == 2)
        {
            totalTwos++;
        }
        else if (rollResult == 3)
        {
            totalThrees++;
        }
        else if (rollResult == 4)
        {
            totalFours++;
        }
        else if (rollResult == 5)
        {
            totalFives++;
        }
        else if (rollResult == 6)
        {
            totalSixs++;
        }
        else
        {
            // Do nothing
        }
    }

    // Updates the To Hit text
    private void UpdateToHitText()
    {
        toHitText.text = (toHit.ToString() + "+");
    }

    // Update the success text to display number of calculated hits
    private void UpdateSuccessText()
    {
        successText.text = (successfulRolls.ToString() + "!");
    }

    // Updates the die UI
    private void UpdateDiceText()
    {
        rollOneText.text = totalOnes.ToString();
        rollTwoText.text = totalTwos.ToString();
        rollThreeText.text = totalThrees.ToString();
        rollFourText.text = totalFours.ToString();
        rollFiveText.text = totalFives.ToString();
        rollSixText.text = totalSixs.ToString();
    }

    // Update the text for the dice pool
    private void UpdateDicePoolText()
    {
        dicePoolText.text = dicePool.ToString();
    }

    // Stops dice rolling
    private void StopRoll()
    {
        gameStarted = false;
    }
    
    // Resets the dice
    void ResetDice()
    {
        diceRolled = 0;
        totalOnes = 0;
        totalTwos = 0;
        totalThrees = 0;
        totalFours = 0;
        totalFives = 0;
        totalSixs = 0;
        UpdateDiceText();
        UpdateDicePoolText();
    }

    // Resets rerollPool
    private void ResetReroll()
    {
        rerollPool = 0;
    }

    // Resets successPool
    private void ResetSuccess()
    {
        successfulRolls = 0;
        UpdateSuccessText();
    }

    // Increase dice pool
    public void IncreaseDicePool()
    {
        dicePool++;
        UpdateDicePoolText();
    }

    // Decrease dice pool
    public void DecreaseDicePool()
    {
        if (dicePool > 0)
        {
            dicePool--;
            UpdateDicePoolText();
        }
        else
        {
            // Do nothing
        }
    }

    // Increase To Hit
    public void IncreaseToHit()
    {
        if (toHit <= 5)
        {
            toHit++;
            UpdateToHitText();
        }
        else
        {
            // Do nothing
        }
    }

    //Decrease To Hit
    public void DecreaseToHit()
    {
        if (toHit >=3)
        {
            toHit--;
            UpdateToHitText();
        }
        else
        {
            // Do nothing
        }
    }
}
