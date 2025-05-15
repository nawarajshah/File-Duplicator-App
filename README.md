# File Duplicator App

A Windows Forms application for batch duplicating files in selected subfolders, with support for search-and-replace in both file names and file contents. The app provides a user-friendly interface for selecting folders, configuring duplication rules, and reviewing detailed logs of all operations.

---

## Features

- **Folder Selection:** Browse and select a root folder containing subfolders to process.
- **Subfolder Selection:** Choose which subfolders to include in the duplication operation.
- **Search and Replace:** Specify a string to find and a string to replace it with in file names and file contents.
- **Batch Processing:** Duplicates all matching files in the selected subfolders.
- **Logging:** All success and error messages are written to a `duplication.log` file in the root folder.
- **User Feedback:** After duplication, a summary message is shown with an option to open the log file for details.

---

## How to Use

1. **Launch the Application.**
2. **Browse for the Root Folder:**  
   Click the "Browse" button and select the folder containing the subfolders you want to process.
3. **Select Subfolders:**  
   Use the checklist to select which subfolders to include. Click "Save Config" to store your selection.
4. **Configure Duplication:**
   - Enter the string to find (e.g., `abc`).
   - Enter the string to duplicate/replace with (e.g., `xyz`).
5. **Start Duplication:**  
   Click the "Submit" button to begin the duplication process.
6. **Review Results:**  
   After completion, a message box will appear. You can open the `duplication.log` file directly from the message box to see detailed results.

---

## Log File

- **Location:**  
  The log file is named `duplication.log` and is created in the root folder you selected.
- **Contents:**  
  - Each operation (success or error) is logged with a timestamp.
  - Example entries:
    
