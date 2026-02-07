# SpendWise – Personal Finance & Expense Tracker 

## Problem

Most people track daily expenses mentally or in temporary notes.
Once cleared or forgotten, there is **no reliable monthly history**, making it hard to:

- Review past spending  
- Compare income vs expenses  
- Maintain financial discipline  

## Solution

**SpendWise** is a **desktop-first personal finance tracker** that separates  
**active session calculations** from **permanent financial history**.

It allows users to:
- Track income and expenses in real time  
- Persist transactions safely for long-term monthly analysis  
- View clean, structured financial history without cluttering the main UI  


## Core Concept

SpendWise follows a **two-layer financial model**:

### 1. Session Layer (Calculator View)
- Temporary working list
- Can be cleared without losing history
- Optimized for daily usage

### 2. Ledger Layer (History View)
- Permanent transaction storage
- Month-wise income & expense breakdown
- Independent from UI clearing actions


## Features

### Transaction Management
- Add income and expense transactions
- Support for multiple currencies
- Real-time balance calculation

### History System
- Dedicated **History screen**
- Month-wise transaction grouping
- Separate Income & Expense views
- Monthly totals and net balance
- Safe deletion without affecting other months

### Data Persistence
- Local JSON-based ledger storage
- Session reset does not affect stored history
- Reloads automatically on app restart

### Clean UX
- Minimal desktop UI
- History accessible via single icon (🕒)
- No clutter in main screen


## Tech Stack

- C#
- WPF (Windows Presentation Foundation)
- .NET
- Local data persistence using JSON (System.Text.Json)


## Design Decisions

- **Desktop-first UI**  
  Chosen for clarity, keyboard efficiency, and system-level reliability.

- **Session vs Ledger separation**  
  Prevents accidental data loss while keeping UI lightweight.

- **Local-first storage**  
  No cloud dependency, faster access, full user privacy.



## Edge Cases Handled

- Clearing active transactions without deleting history
- Multiple currencies in the same month
- Deleting individual transactions safely
- Removing entire monthly records
- App restart without data loss

## Folder Structure

SpendWise/
├── MainWindow.xaml
├── HistoryWindow.xaml
├── Transaction.cs
├── TransactionStorage.cs
├── App.xaml
├── README.md

## Future Improvements

- Monthly charts & visual analytics
- Category-based expense tracking
- Cloud sync 

## Screenshots

<img src="SpendWise/screenshots/main window.png" width="250"/>
<img src="SpendWise/screenshots/history window.png" width="250"/>
