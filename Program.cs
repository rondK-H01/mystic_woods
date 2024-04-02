// NOTE: I avoided using external libraries because I couldn't justify using
// multiple thousand lines just to make this application look a bit prettier
//
// I also did not use an LLM for any of the content within.

// Start with a clean slate.
Cons.Clear();
Utils.StartWindowResizeListener();

Cons.Green();
Utils.WriteCenterLines(new string[]{
        "Welcome to the exploration of Mystic Woods.",
        "",
        "The princess of the town has gone missing somewhere in these woods.",
        "Your objective is to track her down and keep her safe.",
        "",
        "If you accept this challenge, press any key to continue..."
    }, false);

Console.ReadKey(true);

Cons.Clear();

Utils.SetCenterVertical(3);
Utils.WriteCenter("Please enter your name");
Utils.SetPos((Console.WindowWidth / 2) - 10, Console.CursorTop + 2);
s:
Console.Write("Name: ");
string? Name = Console.ReadLine();
if (string.IsNullOrEmpty(Name)) goto s;

Cons.Clear();

Cons.ResetColor();
Utils.SetCenterVertical(5);
Utils.WriteCenter("Choose a class");
Utils.WriteCenter();
Utils.WriteCenterLines(new string[]{
        "[1] Wizard - Wield the power of magic",
        "[2] Archer - Arrows are stronger than swords",
        "[3] Rogue - Close and personal"});
int Class = Utils.GetNumber(3);

Player player = new Player(Name.Trim(), (Player.Playstyle)Class - 1);

// Explaining the numbers
// 0: Player
// 1: Path
// 2: Wall
// 3: Trap
// 4: Enemy
// 5: Chest
// 6: Void

int[,] map = {
    {2,2,2,2,2},
    {2,1,3,5,2},
    {2,4,0,1,2},
    {2,1,1,6,2},
    {2,2,2,2,2},
};

int PlayerX = 2;
int PlayerY = 2;
bool GameLost = false;

while (!GameLost)
{
  Cons.Clear();
  Utils.WriteCenterLines(new string[]{
        "[W] Move Up",
        "[A] Move Left",
        "[S] Move Down",
        "[D] Move Right"});
  switch (Utils.GetDirection())
  {
    case 1:
      if (PlayerY == 0)
      {
        Console.WriteLine("Can't move that way");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
      }
      else
      {
        PlayerY -= 1;
        CheckSpace(PlayerX, PlayerY);
      }
      break;
    case 2:
      if (PlayerX == 0)
      {
        Console.WriteLine("Can't move that way");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
      }
      else
      {
        PlayerX -= 1;
        CheckSpace(PlayerX, PlayerY);
      }
      break;
    case 3:
      if (PlayerY == 4)
      {
        Console.WriteLine("Can't move that way");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
      }
      else
      {
        PlayerY += 1;
        CheckSpace(PlayerX, PlayerY);
      }
      break;
    case 4:
      if (PlayerX == 4)
      {
        Console.WriteLine("Can't move that way");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
      }
      else
      {
        PlayerX += 1;
        CheckSpace(PlayerX, PlayerY);
      }
      break;
  }
}

void CheckSpace(int x, int y)
{
  int Space = map[y, x];
  switch (Space)
  {
    case 1:
      return;
    case 2:
      Console.WriteLine("That's a wall");
      break;
    case 3:
      player.Damage(20);
      if (player.HP == 0)
      {
        GameLost = true;
        Console.WriteLine("You got hit by a trap for the last time.");
        Console.WriteLine("Game over.");
        break;
      }
      Console.WriteLine($"Oops you hit a trap, damaging you for 20HP, you have {player.HP}HP left");

      Console.WriteLine("Press any key to continue");
      Console.ReadKey();
      break;
    case 4:
      string weapon = "";
      switch (player.Class)
      {
        case Player.Playstyle.Wizard:
          weapon = "Wand";
          break;
        case Player.Playstyle.Archer:
          weapon = "Bow";
          break;
        case Player.Playstyle.Rogue:
          weapon = "Sword";
          break;
      }
      Console.WriteLine($"You met a monster, you decide to fight it with your {weapon} and lose.");
      Console.WriteLine("Game over.");
      GameLost = true;
      break;
    case 5:
      Console.WriteLine("You found a chest, but the contents are unsurprisingly bad.");
      Console.WriteLine("There was nothing in the chest.");
      Console.WriteLine("Press any key to continue");
      Console.ReadKey();
      break;
    case 6:
      GameLost = true;
      Console.WriteLine("You entered the void, that's it for you.");
      Console.WriteLine("Game over.");
      break;
  }
}
Console.ReadKey();
