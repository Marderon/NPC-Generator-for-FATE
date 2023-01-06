using System.IO;

string ProgramPath = Directory.GetCurrentDirectory();
List<string> BackgroundPull = (Directory.GetDirectories(Path.Combine(ProgramPath, "NPC Generator", "Background"), "*", SearchOption.TopDirectoryOnly)).ToList();
List<string> RacePull = (Directory.GetDirectories(Path.Combine(ProgramPath, "NPC Generator", "Race Info"), "*", SearchOption.TopDirectoryOnly)).ToList();
List<string> ManyBackgroundsPull = new List<string>();
Random Random = new Random();
int BackgroundCounter = 0;
int NumRace;
bool FullRandom = false;
string Separator = "\n" + "___________________________________________________________________________" + "\n";
string Race = "";
string RaceS = "";
string Sex = "";
string FullName = "";
string FullNameS = "";
string FirstName = "";
string LastName = "";
string Background = "";
string BackgroundS = "";
string APO = "";
string CharacterTrait = "";
string Ideal = "";
string Devotion = ""; ;
string Weakness = "";
string NPCinfo = "";

PMenu();

void PMenu()
{
    Console.WriteLine("0. Создать персонажа вручную/ 1. сгенерировать случайно?");
    switch (int.Parse(Console.ReadLine()))
    {
        case 0:
            Console.Clear();
            RacePicker();
            RaceNameRandom();
            BackgroundPicker();
            AdditionalPersonalityOptions();
            NPCFileCreator();
            break;
        default:
            Console.Clear();
            FullRandom = true;
            RacePicker();
            RaceNameRandom();
            BackgroundPicker();
            AdditionalPersonalityOptions();
            NPCFileCreator();
            break;
    }
}
void RacePicker()
{
    if (!FullRandom)
    {

        Console.WriteLine("Выбери расу: \n" +
            "-1. Случайная");
        int counter = 0;
        foreach (string race in RacePull)
        {
            Console.WriteLine(counter + ". " + new DirectoryInfo(race).Name);
            counter++;
        }
        int NumRace = int.Parse(Console.ReadLine());
        if (NumRace == -1)
        {
            RandomRace();
        }
        else
        {
            Race = new DirectoryInfo(RacePull[NumRace]).Name;
            RaceS = "Раса: " + new DirectoryInfo(RacePull[NumRace]).Name + "\n";
        }
        InfoShow();
    }
    else
    {
        RandomRace();
    }
}
void RandomRace()
{
    int NumRace = Random.Next(0, RacePull.Count - 1);
    Race = new DirectoryInfo(RacePull[NumRace]).Name;
    RaceS = "Раса: " + new DirectoryInfo(RacePull[NumRace]).Name + "\n";
}
void RaceNameRandom()
{
    string NameTxt;
    int SexN = 0;
    if (!FullRandom)
    {
        Console.WriteLine("Выбери пол персонажа: \n" +
            "0 - Женский | 1 - Мужской");
        SexN = int.Parse(Console.ReadLine());
    }
    else
    {
        SexN = Random.Next(0, 2);
    }
    if (SexN == 0)
    {
        Sex = "Пол: " + "Женский" + "\n";
        NameTxt = "FirstNameW.txt";
    }
    else
    {
        Sex = "Пол: " + "Мужской" + "\n";
        NameTxt = "FirstNameM.txt";
    }
    string RaceFirstNamesPath = Path.Combine(ProgramPath, "NPC Generator", "Race Info", Race, NameTxt);
    string RaceLastNamesPath = Path.Combine(ProgramPath, "NPC Generator", "Race Info", Race, "LastName.txt");
    FirstName = File.ReadAllLines(RaceFirstNamesPath)[Random.Next(0, File.ReadAllLines(RaceFirstNamesPath).Length)];
    LastName = File.ReadAllLines(RaceLastNamesPath)[Random.Next(0, File.ReadAllLines(RaceLastNamesPath).Length)];
    FullName = FirstName + " " + LastName;
    FullNameS = "Имя: " + FirstName + " " + LastName + "\n";
    InfoShow();
}
void BackgroundPicker()
{
    if (!FullRandom)
    {
        int NumBackground = 0;
        bool IsANum = true;
        while (IsANum)
        {
            BackgroundShow();
            IsANum = int.TryParse(Console.ReadLine(), out NumBackground);
            if (IsANum)
            {
                ManyBackgroundsPull.Add(new DirectoryInfo(BackgroundPull[NumBackground]).Name);
                BackgroundUpdater();
                InfoShow();
            }
        }
        BackgroundTraitsGenerator();
        InfoShow();
    }
    else
    {
        int RandomBackgroundCount = Random.Next(1, 3);
        for (int i = 0; i < RandomBackgroundCount; i++)
        {
            int NumBackground = Random.Next(0, BackgroundPull.Count);
            ManyBackgroundsPull.Add(new DirectoryInfo(BackgroundPull[NumBackground]).Name);
            BackgroundPull.RemoveAt(NumBackground);
            BackgroundUpdater();
        }
        BackgroundTraitsGenerator();
    }

}
void BackgroundUpdater()
{
    string ManyBackgrounds = "";
    foreach (string backG in ManyBackgroundsPull)
        ManyBackgrounds += backG + " ";
    BackgroundS = "Предыстория: " + ManyBackgrounds + "\n";
}
void BackgroundTraitsGenerator()
{
    string CharacterTraitPath = Path.Combine(ProgramPath, "NPC Generator", "Background", RandomManyBackgrounds(), "Черты характера.txt");
    CharacterTrait = "Черта характера: " + File.ReadAllLines(CharacterTraitPath)[Random.Next(0, File.ReadAllLines(CharacterTraitPath).Length)] + "\n";
    string IdealPath = Path.Combine(ProgramPath, "NPC Generator", "Background", RandomManyBackgrounds(), "Идеал.txt");
    Ideal = "Идеал: " + File.ReadAllLines(IdealPath)[Random.Next(0, File.ReadAllLines(IdealPath).Length)] + "\n";
    string DevotionPath = Path.Combine(ProgramPath, "NPC Generator", "Background", RandomManyBackgrounds(), "Привязанность.txt");
    Devotion = "Привязанность: " + File.ReadAllLines(DevotionPath)[Random.Next(0, File.ReadAllLines(DevotionPath).Length)] + "\n";
    string WeaknessPath = Path.Combine(ProgramPath, "NPC Generator", "Background", RandomManyBackgrounds(), "Слабость.txt");
    Weakness = "Слабость: " + File.ReadAllLines(WeaknessPath)[Random.Next(0, File.ReadAllLines(WeaknessPath).Length)];
}
string RandomManyBackgrounds()
{
    int NumBackground;
    return Background = ManyBackgroundsPull[NumBackground = Random.Next(0, ManyBackgroundsPull.Count)];
}
void BackgroundShow()
{
    if (BackgroundCounter == 0)
        Console.WriteLine("Выбери предысторию : \n" + "-1. Случайная");
    else
        Console.WriteLine("Введи пустую строку, чтобы пропустить выбор дополнительной предыстории \n" + "Или выбери дополнительную предысторию : \n" + "-1. Случайная");
    int counter = 0;
    foreach (string background in BackgroundPull)
    {
        Console.WriteLine(counter + ". " + new DirectoryInfo(background).Name);
        counter++;
    }
}
void AdditionalPersonalityOptions()
{
    string SinPath = Path.Combine(ProgramPath, "NPC Generator", "APO", "Порок.txt");
    string GoodnessPath = Path.Combine(ProgramPath, "NPC Generator", "APO", "Добродетель.txt");
    int Answer;
    if (!FullRandom)
    {
        Console.WriteLine("Сгенерировать порок/добродетель?" + "\n" + "1. Да, сгенерировать порок | 2. Да, сгенерировать добродетель | 3. Нет");
        Answer = int.Parse(Console.ReadLine());
    }
    else
    {
        Answer = Random.Next(1, 4);
    }
    switch (Answer)
    {
        case 1:
            string Sin = File.ReadAllLines(SinPath)[Random.Next(0, File.ReadAllLines(SinPath).Length)];
            APO = "Порок: " + Sin + "\n";
            break;
        case 2:
            string Goodness = File.ReadAllLines(GoodnessPath)[Random.Next(0, File.ReadAllLines(GoodnessPath).Length)];
            APO = "Добродетель: " + Goodness + "\n";
            break;
        default:
            break;
    }
    InfoShow();
}
void NPCFileCreator()
{
    int NPCCount = Directory.GetFiles(Path.Combine(ProgramPath, "NPC Generator", "NPC"), "*", SearchOption.TopDirectoryOnly).Count();
    Console.Clear();
    NPCinfo = FullNameS + RaceS + Sex + BackgroundS + APO + CharacterTrait + Devotion + Ideal + Weakness;
    string CreatedNPC = Path.Combine(ProgramPath, "NPC Generator", "NPC", NPCCount + ". " + FullName + ".txt");
    File.WriteAllText(CreatedNPC, NPCinfo);
    InfoShow();
}
void InfoShow()
{
    Console.Clear();
    NPCinfo = FullNameS + RaceS + Sex + BackgroundS + APO + CharacterTrait + Devotion + Ideal + Weakness + Separator;
    Console.WriteLine(NPCinfo);
}



/*
    File.WriteAllText(Path.Combine(ProgramPath, "NPC Generator", "NPC Count.txt"), "" + NPCCount);
 * int.Parse(File.ReadAllText(Path.Combine(ProgramPath, "NPC Generator", "NPC Count.txt")));
    File.Create(Path.Combine(ProgramPath, "NPC Generator", "NPC Count.txt"));
    FileStream file = new FileStream(Path.Combine(ProgramPath, "NPC Generator", "NPC Count.txt"), FileMode.OpenOrCreate);
    file.Seek(0, SeekOrigin.Begin);
    StreamWriter stream = new StreamWriter(file);
    stream.WriteLine(NPCCount);

*/
