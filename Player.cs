internal class Player
{
  internal string Name;
  internal int HP; // Health
  internal readonly Playstyle Class;

  internal void Damage(int hp)
  {
    HP -= hp;
  }

  internal Player(string name, Playstyle ps)
  {
    Name = name;
    HP = 100;
    Class = ps;
  }

  internal enum Playstyle
  {
    Wizard = 0,
    Archer = 1,
    Rogue = 2
  }
}
