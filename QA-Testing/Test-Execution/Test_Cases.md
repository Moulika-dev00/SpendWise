
Test Cases - SpendWise Application

### Expense Management

Test Case ID: TC_EXP_01
Test Scenario: Verify expense can be added with valid data

Preconditions:
Application is launched and user is on expense entry screen

Test Steps:
1. Navigate to expense entry section
2. Enter valid amount
3. Select valid category
4. Provide required details
5. Click Save

Test Data:
Valid numeric amount and valid category

Expected Result:
Expense should be saved successfully and displayed in records

Actual Result:
Expense was successfully added and displayed correctly in records

Status:
Pass


Test Case ID: TC_EXP_02
Test Scenario: Verify system behavior when input fields are empty

Preconditions:
Application is on expense entry screen

Test Steps:
1. Leave all input fields empty
2. Click Save

Test Data:
Empty input

Expected Result:
System should prevent saving and display validation message

Actual Result:
System displayed validation message "Please enter required inputs" and prevented saving. 

Status:
Pass


Test Case ID: TC_EXP_03
Test Scenario: Verify system response to invalid data input

Preconditions:
Application is on expense entry screen

Test Steps:
1. Enter invalid data in amount field
2. Provide other details if required
3. Click Save

Test Data:
Non-numeric value

Expected Result:
System should reject invalid input and display error message

Actual Result:
System displayed validation message "enter valid amount" and prevented saving

Status:
Pass


Test Case ID: TC_EXP_04
Test Scenario: Verify system handling of negative values

Preconditions:
Application is on expense entry screen

Test Steps:
1. Enter negative value in amount field
2. Click Save

Test Data:
Negative number

Expected Result:
System should not accept negative values and show validation

Actual Result:
System rejected negative value and displayed validation message "Amount must be greater than zero".

Status:
Pass


Test Case ID: TC_EXP_05
Test Scenario: Verify system behavior with large input values

Preconditions:
Application is on expense entry screen

Test Steps:
1. Enter very large amount
2. Click Save

Test Data:
Large numeric value

Expected Result:
System should handle input without crash and process correctly

Actual Result:
System rejected large input value and displayed validation message "Amount exceeds allowed limit".

Status:
Pass


Test Case ID: TC_EXP_06
Test Scenario: Verify expense is stored correctly

Preconditions:
Valid expense is entered and saved

Test Steps:
1. Add expense with valid data
2. Navigate to records/history section
3. Locate added entry

Test Data:
Valid expense data

Expected Result:
Stored data should match the entered data

Actual Result:
Expense was saved successfully and displayed correctly in records. 
Stored data matches the entered values.

Status:
Pass 


### INCOME MANAGEMENT

Test Case ID: TC_INC_01
Test Scenario: Verify income can be added with valid data

Preconditions:
Application is launched and user is on income entry screen

Test Steps:
1. Navigate to income entry section
2. Enter valid income amount
3. Provide required details
4. Click Save

Test Data:
Valid numeric amount

Expected Result:
Income should be saved successfully and displayed in records

Actual Result:
Income was added successfully and displayed correctly in records. 
Total balance was updated accordingly.

Status:
Pass 


Test Case ID: TC_INC_02
Test Scenario: Verify system behavior for invalid input

Preconditions:
Application is on income entry screen

Test Steps:
1. Enter invalid data in amount field
2. Provide other details if required
3. Click Save

Test Data:
Non-numeric value

Expected Result:
System should reject invalid input and display error message

Actual Result:
System displayed validation message "enter valid amount" and prevented saving of invalid input. 

Status:
Pass


Test Case ID: TC_INC_03
Test Scenario: Verify system behavior for empty input

Preconditions:
Application is on income entry screen

Test Steps:
1. Leave all input fields empty
2. Click Save

Test Data:
Empty input

Expected Result:
System should prevent saving and display validation message

Actual Result:
System displayed validation message "Please enter required inputs" and prevented saving.

Status:
Pass


Test Case ID: TC_INC_04
Test Scenario: Verify income is stored correctly

Preconditions:
Valid income entry is saved

Test Steps:
1. Add income with valid data
2. Navigate to records/history section
3. Locate added entry

Test Data:
Valid income data

Expected Result:
Stored income data should match entered data

Actual Result:
Income was saved successfully and displayed correctly in records. 
Stored data matches the entered values and total balance was updated accordingly.

Status:
Pass


### Category Management

Test Case ID: TC\_CAT\_01
Test Scenario: Verify category selection works correctly

Preconditions:
Application is launched and category options are available

Test Steps:
1. Navigate to transaction entry section
2. Open category selection dropdown
3. Select a category
4. Complete the transaction
5. Save

Test Data:
Valid category

Expected Result:
Selected category should be assigned correctly to the transaction

Actual Result:
Selected category was applied correctly to the transaction and displayed properly in the transaction list and records.

Status:
Pass


Test Case ID: TC_CAT_02
Test Scenario: Verify system behavior when no category is selected 

Preconditions:
Application is on transaction entry screen

Test Steps:
1. Do not change category selection (or leave default if applicable)
2. Enter other required fields
3. Click Save

Test Data:
No category selection/ default selection

Expected Result:
System should either prevent saving if category is mandatory
OR
Assign default category correctly

Actual Result:
System displayed validation message "Please enter required inputs" when category was not selected and prevented saving.

Status:
Pass


Test Case ID: TC_CAT_03
Test Scenario: Verify transactions are correctly mapped to category

Preconditions:
Valid transaction is saved with selected category

Test Steps:
1. Add transaction with a selected category
2. Navigate to records or filtered view
3. Check category assigned to the transaction

Test Data:
Valid category with transaction data

Expected Result:
Transaction should be associated with the correct category

Actual Result:
Transaction was saved with the selected category and displayed correctly in records with proper category mapping.

Status:
Pass


### Data Storage(Ledger)

Test Case ID: TC_LED_01
Test Scenario: Verify data is stored permanently

Preconditions:
Application is running and data entry is possible

Test Steps:
1. Add a valid transaction
2. Close the application
3. Reopen the application
4. Navigate to records

Test Data:
Valid transaction data

Expected Result:
Previously saved data should persist and be available after reopening

Actual Result:
Transaction data was saved successfully and remained available after closing and reopening the application, confirming permanent storage.

Status:
Pass


Test Case ID: TC_LED_02
Test Scenario: Verify stored data matches displayed data

Preconditions:
Transaction is already saved

Test Steps:
1. Add a transaction with known values
2. Navigate to records display
3. Compare displayed data with entered values

Test Data:
Known transaction values

Expected Result:
Displayed data should exactly match stored data

Actual Result:
Stored data matched the displayed data in both the main window and history window with no discrepancies.

Status:
Pass


Test Case ID: TC_LED_03
Test Scenario: Verify data consistency across operations

Preconditions:
Multiple transactions are present

Test Steps:
1. Add multiple transactions
2. Perform filtering or navigation
3. Re-check stored records

Test Data:
Multiple transaction entries

Expected Result:
Data should remain consistent across operations without mismatch or loss

Actual Result:
Data remained consistent across different operations such as navigation, filtering, and viewing different sections. No data loss or mismatch observed.

Status:
Pass


### Data synchronization

Test Case ID: TC_SYNC_01
Test Scenario: Verify transactions in main window are reflected in history window

Preconditions:
Application is running across windows

Test Steps:
1. Add a transaction in main window
2. Navigate to history window
3. Locate the transaction

Test Data:
Valid transaction data

Expected Result:
Transaction should be present in both main window and history window

Actual Result:
Transaction added in the main window was successfully reflected in the history window with correct data.

Status:
Pass


Test Case ID: TC_SYNC_02
Test Scenario: Verify deletion in main window does not affect history data

Preconditions:
Transaction exists in both main and history windows

Test Steps:
1. Delete a transaction from main window
2. Navigate to history window
3. Check if transaction still exists 

Test Data:
Existing Data

Expected Result:
Transaction should be removed from main window but remain in history window

Actual Result:
Transaction deleted from the main window was removed from the main view but remained unchanged in the history window.

Status:
Pass


Test Case ID: TC_SYNC_03
Test Scenario:
Verify charts and insights are based only on history data

Preconditions:
Transactions exist in history window

Test Steps:
1. Add transactions
2. Delete transaction from main window
3. Observe charts and insights

Test Data:
Multiple transactions

Expected Result:
Charts and insights should reflect history data and remain unaffected by main window deletion

Actual Result:
Charts and insights continued to reflect data from the history window and were not affected by deletion of transactions from the main window.

Status:
Pass


### Monthly Analysis

Test Case ID: TC_MON_01
Test Scenario: Verify monthly calculations are accurate

Preconditions:
Multiple transactions exist for a specific month

Test Steps:
1. Add multiple income and expense entries within a month
2. Navigate to monthly analysis section
3. Observe calculated totals

Test Data:
Known transaction values for a month

Expected Result:
Displayed monthly totals should match actual calculated values

Actual Result:
Monthly calculations were accurate and totals for income, expense, and balance matched the expected values.

Status:
Pass


Test Case ID: TC_MON_02
Test Scenario: Verify aggregation of data is correct

Preconditions:
Multiple categorized transactions exist

Test Steps:
1. Add multiple transactions across categories
2. Navigate to analysis section
3. Verify aggregated totals

Test Data:
Categorized transaction data

Expected Result:
Aggregated data should reflect correct grouping and totals

Actual Result:
Pie chart displayed expense distribution correctly based on categories, and percentage values matched the actual expense data.

Status:
Pass


Test Case ID: TC_MON_03
Test Scenario: Verify bar chart displays monthly expense correctly

Preconditions:
Transactions already exist.

Test Steps:
1. Add multiple income and expense transactions
2. Observe values in main window (total balance, savings, charts, insights)
3. Note down the displayed values
4. Navigate to another section (e.g., history window)
5. Return to main window
6. Re-check all values

Expected Result:
All values including total balance, savings, charts, and insights should remain consistent across navigation and should not change unless new data is added or modified.

Actual Result:
Monthly analysis data including total balance, savings, charts, and insights remained consistent for the selected month with no discrepancies observed.

Status:
Pass 


### Filtering


Test Case ID: TC_FIL_01
Test Scenario: Verify filtering by category

Preconditions:
Application is running and transactions exist with multiple categories

Test Steps:
1. Open the main window
2. Locate the category filter dropdown at the top
3. Select a specific category
4. Observe transaction list

Test Data:
Transactions belonging to different categories

Expected Result:
Only transactions belonging to selected category should be displayed

Actual Result:
Transactions were filtered correctly based on the selected category and only matching records were displayed in the main transaction list.

Status:
Pass


Test Case ID: TC_FIL_02
Test Scenario: Verify accuracy filtered results

Preconditions:
Application is running and filtered data is available

Test Steps:
1. Apply category filter
2. Note the filtered transaction list
3. Open history window
4. Compare filtered list with actual stored data

Test Data:
Known transaction dataset.

Expected Result:
Filtered results must exactly match the corresponding data stored in history window

Actual Result:
Filtered results matched exactly with the corresponding data in the history window with no discrepancies observed.

Status:
Pass


###  DATA VISUALIZATION 


Test Case ID: TC_VIS_01
Test Scenario: Verify pie chart displays category-wise expense distribution correctly

Preconditions:
Application is running an expense transactions exist across multiple categories

Test Steps:
1. Navigate to chart/visualization section
2. Observe chart values
3. Note category distribution
4. Compare with actual expense data in transaction list/history

Test Data:
Multiple expense transactions across categories

Expected Result:
Pie chart should accurately represent percentage distribution of expenses by category.

Actual Result:
Pie chart displayed category-wise expense distribution correctly and percentages matched the actual expense data.

Status:
Pass


Test Case ID: TC_VIS_02
Test Scenario: Verify bar chart displays monthly expense correctly

Preconditions:
Application is running and expense transactions exist across different months

Test Steps:
1. Add or modify transaction
2. Navigate to chart section
3. Note values for each month
4. Compare with actual expense totals from history

Test Data:
Expense data across multiple months

Expected Result:
Bar chart should correctly display total expense for each month

Actual Result:
Bar chart displayed monthly expense totals accurately and values matched the data from the history window.

Status:
Pass


Test Case ID: TC_VIS_03
Test Scenario: Verify charts match actual stored data

Preconditions:
Application contains transaction data

Test Steps:
1. Observe pie chart and bar chart
2. Open history window
3. Compare chart values with actual stored data

Test Data:
Known transaction dataset.

Expected Result:
Charts should accurately reflect stored transaction data

Actual Result:
Chart values were consistent with the actual stored transaction data and no mismatch was observed.

Status:
Pass



### Insight Engine


Test Case ID: TC_INS_01
Test Scenario: Verify insights are generated based on transaction data

Preconditions:
Sufficient transaction data is available.

Test Steps:

1. Add multiple income and expense entries
2. Navigate to insights section
3. Observe generated insights(spending and savings)

Test Data:
Multiple income and expense entries

Expected Result:
Insights should be generated based on available transaction data

Actual Result:
Insights were generated correctly based on available transaction data and reflected spending and savings trends.

Status:
Pass


Test Case ID: TC_INS_02
Test Scenario: Verify accuracy of insight calculations

Preconditions:
Known transaction values exist

Test Steps:
1. Add known income and expense values
2. Calculate expected savings and spending manually
3. Compare with displayed insights

Test Data:
Controlled dataset

Expected Result:
Insights should match manual calculations

Actual Result:
Insight values matched manual calculations and correctly represented savings and spending percentages.

Status:
Pass


Test Case ID: TC_INS_03
Test Scenario: Verify consistency of insights

Preconditions:
Application contains transaction data

Test Steps:
1. Observe insights in main window
2. Navigate to history window and return
3. Re-check insights

Test Data:
Existing dataset

Expected Result:
Insights should remain consistent unless data changes

Actual Result:
Insights remained consistent across navigation and did not change unless transaction data was modified

Status:
Pass


### Input Validation


Test Case ID: TC_VAL_01
Test Scenario: Verify system rejects invalid input

Preconditions:
Application is on data entry 

Test Steps:
1. Enter invalid data in input fields
2. Attempt to save

Test Data:
Incorrect or non-numeric values

Expected Result:
System should reject invalid input and display validation message

Actual Result:
System displayed validation message for invalid input and prevented transaction from being saved.

Status:
Pass


Test Case ID: TC_VAL_02
Test Scenario: Verify system handles empty input

Preconditions:
Application is on data entry screen

Test Steps:
1. Leave required fields empty
2. Click Save

Test Data:
Empty input fields

Expected Result:
System should prevent submission and show validation message

Actual Result:
System displayed validation message "Please enter required inputs" and prevented submission when required fields were empty.

Status:
Pass


Test Case ID: TC_VAL_03
Test Scenario: Verify system validates all fields

Preconditions:
Application is on data entry screen

Test Steps:
1. Enter partial or incorrect data across fields
2. Click Save

Test Data:
Mixed valid and invalid input

Expected Result:
System should validate all fields and highlight errors accordingly

Actual Result:
System validated all input fields correctly and displayed appropriate validation messages for missing or incorrect inputs.

Status:
Pass







