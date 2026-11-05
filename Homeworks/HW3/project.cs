using System;
using System.Collections.Generic;

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
        List<Character> characters = new List<Character>();

        Console.Write("Сколько персонажей создать? ");
        int n = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nПерсонаж {i + 1}");

            Console.Write("Тип (1 - игрок, 2 - враг): ");
            int type = int.Parse(Console.ReadLine()!);

            Console.Write("Имя: ");
            string name = Console.ReadLine()!;

            Console.Write("Здоровье: ");
            int hp = int.Parse(Console.ReadLine()!);

            Console.Write("Сила атаки: ");
            int power = int.Parse(Console.ReadLine()!);

            if (!Character.IsValidStats(hp, power))
            {
                Console.WriteLine("Некорректные характеристики!");
                i--;
                continue;
            }

            if (type == 1) characters.Add(new Player(name, hp, power));
            else if (type == 2) characters.Add(new Enemy(name, hp, power));
            else
            {
                Console.WriteLine("Неверный тип!");
                i--;
            }
        }

        Console.WriteLine($"\nСоздано персонажей: {Character.HowMany()}");

        for (int i = 0; i < characters.Count; i++) Console.WriteLine($"{i + 1}. {characters[i]}");

        if (characters.Count < 2)
        {
            Console.WriteLine("Для боя нужно минимум 2 персонажа.");
            return;
        }

        Console.Write("\nНомер первого бойца: ");
        int a = int.Parse(Console.ReadLine()!) - 1;

        Console.Write("Номер второго бойца: ");
        int b = int.Parse(Console.ReadLine()!) - 1;

        if (a < 0 || b < 0 || a >= characters.Count || b >= characters.Count || a == b)
        {
            Console.WriteLine("Некорректный выбор!");
            return;
        }

        Character fighter1 = characters[a];
        Character fighter2 = characters[b];

        while (fighter1.IsAlive() && fighter2.IsAlive())
        {
            fighter1.Attack(fighter2);
            Console.WriteLine(fighter1);
            Console.WriteLine(fighter2);

            if (!fighter2.IsAlive())
                break;

            fighter2.Attack(fighter1);
            Console.WriteLine(fighter1);
            Console.WriteLine(fighter2);
        }

        if (fighter1.IsAlive()) Console.WriteLine($"Победитель: {fighter1.Name}!");
        else Console.WriteLine($"Победитель: {fighter2.Name}!");
    }
}