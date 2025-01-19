# atturra_interview

***************************************************
### Description

App: **PayCalculator** \
Author: **Ruowei Chen** \
Date: **19/Jan/2025** \
Note: Display the break down information of users' pay package.
***************************************************
### Execution

Download the "PayCalculator/publish" folder, run the "PayCalculator.exe" in the terminal.

Note: The app is based on .Net 9, make sure you have the appropriate version of the .net runtime. 
***************************************************
### Source code

The main project is in the **PayCalculator** folder. To inspect the source code, you may pull the project to local, and open the project using Visual studio. \
The **PayCalculatorTest** is a testing project based on xUnit. 
***************************************************
### Programming Considerations

- **ProcessUserInput** is a static class for processing user inputs, and handling any exceptions.
- **PaySummary** is a class that represents all information about a user's pay, this includes the gross pay, super contribution, taxable income, net income and any deductions. In this program it registers 3 deductions: Income tax, Medicare levy and Budget repair levy. Extra deductions can be registered as long as it's implementing the **IIncomeDeductor** interface. The **Assumption** is that the deductions are to be made upon taxable income only. If there are any deductions to be computed based on gross income, this structure may need to change.
- **IIncomeDeductor** is an interface that's used by the **PaySummary** class. The function "Deduct" represents how the deductions will be calculated.
- **IncomeTaxDeductor**, **MedicareLevyDeductor** and **BudgetRepairLevyDeductor** are classes that compute taxable income deductions.
- Any change involves tax law, can be implemented in the **IncomeTaxDeductor**. 
***************************************************
