using System;

class Character
{
    private string name;
    private int hp;
    private int power;
    private static int count;

    public Character(string name, int hp, int power)
    {
        this.name = name;
        this.hp = hp;
        this.power = power;
        count++;
    }

    public string Name
    {
        get { return name; }
    }

    public int HP
    {
        get { return hp; }
    }

    public virtual void Attack(Character target)
    {
        Console.WriteLine($"{Name} атакует {target.Name} и наносит {power} урона.");
        target.TakeDamage(power);
    }

    public static int HowMany()
    {
        return count;
    }

    public static bool IsValidStats(int hp, int power)
    {
        return hp > 0 && power > 0;
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
            hp -= damage;
    }

    public bool IsAlive()
    {
        return hp > 0;
    }

    public override string ToString()
    {
        return $"{Name} (HP: {HP})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Character other)
            return Name == other.Name;

        return false;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}

class Player : Character
{
    public Player(string name, int hp, int power) : base(name, hp, power) {}

    public override void Attack(Character target)
    {
        Console.WriteLine($"Игрок {Name} наносит удар мечом!");
        base.Attack(target);
    }
}

class Enemy : Character
{
    public Enemy(string name, int hp, int power) : base(name, hp, power) {}

    public override void Attack(Character target)
    {
        Console.WriteLine($"Враг {Name} бросается на противника!");
        base.Attack(target);
    }
}

class Program
{
    static void Main()
    {
        Character player = new Player("Герой", 100, 25);
        Character enemy = new Enemy("Гоблин", 80, 15);

        Console.WriteLine($"Создано персонажей: {Character.HowMany()}");
        Console.WriteLine(player);
        Console.WriteLine(enemy);

        while (player.IsAlive() && enemy.IsAlive())
        {
            player.Attack(enemy);
            Console.WriteLine(player);
            Console.WriteLine(enemy);

            if (!enemy.IsAlive())
                break;

            enemy.Attack(player);
            Console.WriteLine(player);
            Console.WriteLine(enemy);
        }

        if (player.IsAlive())
            Console.WriteLine($"Победитель: {player.Name}!");
        else
            Console.WriteLine($"Победитель: {enemy.Name}!");
    }
}