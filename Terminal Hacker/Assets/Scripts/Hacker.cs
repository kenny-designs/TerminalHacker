using UnityEngine;

public class Hacker : MonoBehaviour {
  // Game configuration data
  const string menuHint = "You may type 'menu' at any time.";
  string[] level1Passwords = { "sony", "play", "gold", "blue", "cash", "credit" };
  string[] level2Passwords = { "alien", "secret", "science", "weird", "desert" };
  string[] level3Passwords = { "servers", "halflife", "three", "fortress", "zombies" };

  // Game state
  int level;
  string password;
  enum Screen { MainMenu, Password, Win };
  Screen currentScreen;

  void Start() {
    ShowMainMenu();
  }

  private void ShowMainMenu() {
    currentScreen = Screen.MainMenu;
    Terminal.ClearScreen();
    Terminal.WriteLine("What would you like to hack into?\n" +
                       "Press 1 for the Playstation Network\n" +
                       "Press 2 for Area 52\n" +
                       "Press 3 for Gabe Newell's servers\n" +
                       "Enter your selection:");
  }

  private void OnUserInput(string input) {
    // we can always return to the main menu
    if (input == "menu") {
      ShowMainMenu();
    }
    else if (currentScreen == Screen.MainMenu) {
      RunMainMenu(input);
    }
    else if (currentScreen == Screen.Password) {
      CheckPassword(input);
    }
  }

  private void RunMainMenu(string input) {
    bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
    if (isValidLevelNumber) {
      level = int.Parse(input);
      AskForPassword();
    }
    else {
      Terminal.WriteLine("Please choose a valid level");
      Terminal.WriteLine(menuHint);
    }
  }

  private void AskForPassword() {
    currentScreen = Screen.Password;
    Terminal.ClearScreen();
    SetRandomPassword();
    Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
    Terminal.WriteLine(menuHint);
  }

  private void SetRandomPassword() {
    switch (level) {
      case 1:
        password = level1Passwords[Random.Range(0, level1Passwords.Length)];
        break;
      case 2:
        password = level2Passwords[Random.Range(0, level2Passwords.Length)];
        break;
      case 3:
        password = level3Passwords[Random.Range(0, level3Passwords.Length)];
        break;
      default:
        Debug.LogError("Invalid level number");
        break;
    }
  }

  private void CheckPassword(string input) {
    if (input == password) {
      DisplayWinScreen();
    }
    else {
      AskForPassword();
    }
  }

  private void DisplayWinScreen() {
    currentScreen = Screen.Win;
    Terminal.ClearScreen();
    ShowLevelReward();
    Terminal.WriteLine(menuHint);
  }

  private void ShowLevelReward() {
    switch (level) {
      case 1:
        Terminal.WriteLine("Oh no Sony got hacked again...");
        break;
      case 2:
        Terminal.WriteLine("You discovered the existence of aliens!");
        break;
      case 3:
        Terminal.WriteLine("You stole Dota2's source code!\n" +
                           "Gaben has had enough of your\n" +
                           "shenanigans and now half-life 3\n" +
                           "is canceled forever. Thanks a lot...");
        break;
      default:
        Debug.LogError("Invalid level reached");
        break;
    }
  }
}
