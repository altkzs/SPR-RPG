
namespace SPR_RPG;
//캐릭터 능력치
public class Player
{
    public int level = 1;
    public string name = "스파르탄";
    public string job = "전사";
    public int atk = 10;
    public int def = 5;
    public int hp = 100;
    public int gold = 1500;

    public int initAtk = 10;
    public int initDef = 5;

    // 플레이어의 인벤토리
    public bool[] inventory = new bool[10];

    //플레이어 최초 생성시 설정값
    public void PlayerStart()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = false;
        }
    }
    // 플레이어의 행동
    public int Input()
    {
        int num = 0;
        while (true)
        {
            
            Console.WriteLine("원하시는 행동을 입력해주세요.\n");
            string input = Console.ReadLine();
            bool IsInt;
            IsInt = int.TryParse(input, out num);

            if (IsInt)
            {
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine ("잘못된 값입니다.제대로 입력해주세요.");

            }
        }
        return num;
    }
    public void AddInventory(int select)
    {
        inventory[select] = true;
    }
    public void PayGold(int amount)
    {
        gold -= amount;
    }
    public void On(int amount,bool isWeapon)
    {
        if (isWeapon)
        {
            atk += amount;
        }
        else
        {
            def += amount;
        }
    }
    public void Off(int amount,bool isWeapon)
    {
        if (isWeapon)
        {
            atk -= amount;
        }
        else
        {
            def -= amount;
        }
    }
}


public class Item
{
    public string name = "";
    public int point = 0;
    public string comment = "";
    public int reqGold = 0;

    // false면 보호구(Armor), true면 무기류(Weapon)
    public bool IsWeapon = false;
    public bool IsBought = false;
    public bool IsEquiped = false;
}

class GameScene
{
    static Player Player = new Player();
    Item[] ItemsArr = new Item[10];

    bool InvenDisplay = true;
    bool ShopDisplay = true;

    void MakeItems()
    {
        //아이템 배열
        for (int i = 0; i < ItemsArr.Length; i++)
        {
            ItemsArr[i] = new Item();
        }


        //아이템 정보 입력
        ItemsArr[0].name = "수련자 갑옷";
        ItemsArr[0].comment = "수련에 도움을 주는 갑옷입니다";
        ItemsArr[0].point = 5;
        ItemsArr[0].reqGold = 500;

        ItemsArr[1].name = "무쇠 갑옷";
        ItemsArr[1].comment = "무쇠로 만들어져 튼튼한 갑옷입니다";
        ItemsArr[1].point = 9;
        ItemsArr[1].reqGold = 800;

        ItemsArr[2].name = "스파르타의 갑옷";
        ItemsArr[2].comment = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다";
        ItemsArr[2].point = 12;
        ItemsArr[2].reqGold = 1500;

        ItemsArr[3].name = "낡은 검";
        ItemsArr[3].comment = "쉽게 볼 수 있는 낡은 검 입니다.";
        ItemsArr[3].point = 2;
        ItemsArr[3].reqGold = 700;
        ItemsArr[3].IsWeapon = true;

        ItemsArr[4].name = "청동 도끼";
        ItemsArr[4].comment = "어디선가 사용됐던 것 같은 도끼입니다.";
        ItemsArr[4].point = 5;
        ItemsArr[4].reqGold = 1000;
        ItemsArr[4].IsWeapon = true;

        ItemsArr[5].name = "스파르타의 창";
        ItemsArr[5].comment = "스파르타의 전사들이 사용했다는 전설의 창입니다";
        ItemsArr[5].point = 7;
        ItemsArr[5].reqGold = 1700;
        ItemsArr[5].IsWeapon = true;
    }

    public void GameInit()
    {
        MakeItems(); // 아이템 생성
        Player.PlayerStart(); // 플레이어 초기값 생성
    }

    static void TextColor(string text, ConsoleColor clr)
    {
        Console.ForegroundColor = clr;
        Console.WriteLine(text);
        Console.ResetColor();
    }


    // 시작화면
    public void Village()
    {
        Console.Clear(); // 콘솔 창 깔끔하게 하기

       

        TextColor("STR RPG", ConsoleColor.DarkBlue);
        Console.WriteLine("-------------------------------------------------\n");
        //메인화면
        TextColor("\n스파르타 마을에 오신 여러분 환영합니다.", ConsoleColor.Yellow);
        TextColor("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n\n", ConsoleColor.Yellow);
        //상태창,인벤토리,상점선택창
        Console.WriteLine("1) 상태창");
        Console.WriteLine("2) 인벤토리");
        Console.WriteLine("3) 상점");
        Console.WriteLine();

        int choice = Player.Input();
        switch (choice)
        {
            case 1: ShowStatus(); break;
            case 2: Inventory(); break;
            case 3: Shop(); break;
            case 4: Dungeon(); break;
            default:
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadLine(); // 사용자 입력까지 대기
                Village();
                break;
        }

    }
    //상태창
    void ShowStatus()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Clear();
        int equipAtk = Player.atk - Player.initAtk;
        int equipDef= Player.def - Player.initDef;
        string addAtk = "";
        string addDef = "";

        if (equipAtk != 0)
        {
            addAtk = "(+" + equipAtk + ")";
        }
        if (equipDef != 0)
        {
            addDef = "(+" + equipDef + ")";
        }
        

        TextColor("상태창\n캐릭터의 정보가 표시됩니다.\n\n", ConsoleColor.DarkBlue);
        Console.WriteLine("Lv. " + Player.level);
        Console.WriteLine("{0} ( {1} )", Player.name, Player.job);
        Console.WriteLine("공격력: " + Player.atk + addAtk);
        Console.WriteLine("방어력: " + Player.def + addDef);
        Console.WriteLine("체 력: " + Player.hp);
        Console.WriteLine("Gold: {0} G\n", Player.gold);
        Console.WriteLine("0. 나가기\n");

        int choice = Player.Input();
        if (choice == 0)
        {
            Village();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("잘못된 입력입니다");
            Console.ReadLine();
            ShowStatus();
        }
    }
    //인벤토리
    void Inventory()
    {
        Console.Clear();
        Console.WriteLine("인벤토리\n 보유 중인 아이템을 관리할 수 있습니다 \n");
        TextColor("[아이템 목록]",ConsoleColor.Cyan);
        if (InvenDisplay)
        {
            InvenDisplayMode();
        }
        else
        {
            InvenEquipMode();
        }
    }

    void InvenDisplayMode()
    {
        for(int i = 0; i < Player.inventory.Length; i++)
        {
            if (Player.inventory[i])
            {   
                Console.WriteLine("- {0}{1}  | {2} +{3}  |  {4}",
                    ((ItemsArr[i].IsEquiped) ? "[E]" : ""),
                    ItemsArr[i].name,
                    ((ItemsArr[i].IsWeapon) ? "공격력" : "방어력"),
                    ItemsArr[i].point,
                    ItemsArr[i].comment);
            }
        }
        Console.WriteLine("\n\n1. 장착 관리");
        Console.WriteLine("0. 나가기\n");

        int choice = Player.Input();
        if (choice == 0)
        {
            Village();
        }
        else if (choice == 1)
        {
            InvenDisplay = false;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadLine();
        }
        Inventory();
    }

    void InvenEquipMode()
    {
        for (int i = 0; i < Player.inventory.Length; i++)
        {
            if (Player.inventory[i])
            {
                Console.WriteLine("- {0} {1}{2}    | {3} +{4}  |  {5}",
                    (i+1),
                    ((ItemsArr[i].IsEquiped) ? "[E]" : ""),
                    ItemsArr[i].name,
                    ((ItemsArr[i].IsWeapon) ? "공격력" : "방어력"),
                    ItemsArr[i].point,
                    ItemsArr[i].comment);
            }
        }

        Console.WriteLine("\n0. 나가기\n");

        int choice = Player.Input();
        if (choice == 0)
        {
            InvenDisplay = true;
            Inventory();
        }
        else
        {
            int select = choice - 1;

            if (ItemsArr[select].IsBought == true)
            {
                if (ItemsArr[select].IsEquiped)
                {
                    ItemsArr[select].IsEquiped = false;
                    Player.Off(ItemsArr[select].point, ItemsArr[select].IsWeapon);
                    Console.WriteLine("{0}을(를) 장착 해제했습니다", ItemsArr[select].name);
                }
                else
                {
                    ItemsArr[select].IsEquiped = true;
                    Player.Off(ItemsArr[select].point, ItemsArr[select].IsWeapon);
                    Console.WriteLine("{0}을(를) 장착 완료했습니다", ItemsArr[select].name);
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            Console.ReadLine();
            Inventory();
        }

    }
    //상점
    void Shop()
    {
        Console.Clear();
        TextColor("상점", ConsoleColor.DarkCyan);

        Console.WriteLine("필요한 아이템을 얻을수 있는 상점입니다\n\n");
        Console.WriteLine("[보유 골드]\n{0} G\n", Player.gold);
        Console.WriteLine("[아이템 목록]");

        if (ShopDisplay)
        {
            ShopDisplayMode();
        }
        else
        {
            ShopBuyMode();
        }
    }
    void ShopDisplayMode()
    {
        for (int i = 0; i < ItemsArr.Length; i++)
        {
            // 아이템 이름이 공백이 아닌 경우만
            if (ItemsArr[i].name != "")
            {
                Console.WriteLine("- {0}    | {1} +{2}  | {3}  | {4}",
                ItemsArr[i].name,
                //IsWeapon 참이면 공격력, 거짓이면 방어력 반환
                ((ItemsArr[i].IsWeapon) ? "공격력" : "방어력"),
                ItemsArr[i].point,
                ItemsArr[i].comment,
                // isBought가 거짓이면 가격, 참이면 구매완료 표시
                ((ItemsArr[i].IsBought == false) ? (ItemsArr[i].reqGold + " G") : "구매완료"));
            }
        }
        Console.WriteLine("\n1. 아이템 구매");
        Console.WriteLine("0. 나가기\n");

        int choice = Player.Input();
        if (choice == 0)
        {
            Village();
        }
        else if (choice == 1)
        {
            ShopDisplay = false;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("잘못된 입력입니다");
            Console.ReadLine();
        }
        Shop();

    }

    void ShopBuyMode()
    {
        for (int i = 0; i < ItemsArr.Length; i++)
        {
            if (ItemsArr[i].name != "")
            {
                Console.WriteLine("- {0} {1}    | {2} +{3}  | {4}    | {5}",
                (i+1),
                ItemsArr[i].name,
                ((ItemsArr[i].IsWeapon) ? "공격력" : "방어력"),
                ItemsArr[i].point,
                ItemsArr[i].comment,
                ((ItemsArr[i].IsBought == false) ? (ItemsArr[i].reqGold + " G") : "구매완료"));
            }
        }

        Console.WriteLine("\n0. 나가기\n");

        int choice = Player.Input();
        if (choice == 0)
        {
            ShopDisplay = true;
            Shop();
        }
        else
        {
            int select = choice - 1;
            if (ItemsArr[select].IsBought == false)
            {
                // 보유 골드 체크 기능 추가할 것
                if (Player.gold >= ItemsArr[select].reqGold)
                {
                    Player.AddInventory(select);
                    Player.PayGold(ItemsArr[select].reqGold);
                    ItemsArr[select].IsBought = true;
                    Console.WriteLine("구매를 완료했습니다");
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다!");
                }
            }
            else
            {
                Console.WriteLine("이미 구한 아이템입니다");
            }
        }
        Console.ReadLine();
        Shop();
    }

    void Dungeon() { }
}


internal class Program
{
    static void Main(string[] args)
    {
        GameScene MainScene = new GameScene();
        MainScene.GameInit(); // 게임 초기값 로드
        MainScene.Village(); // 메인 화면 로드
    }
}
