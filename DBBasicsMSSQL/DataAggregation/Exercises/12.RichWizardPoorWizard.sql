SELECT SUM(w2.DepositAmount - w1.DepositAmount) AS SumDifference
  FROM WizzardDeposits AS w1, 
       WizzardDeposits AS w2
WHERE w1.Id = w2.Id + 1