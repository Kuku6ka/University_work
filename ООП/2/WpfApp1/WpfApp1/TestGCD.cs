namespace WpfApp1;


public class TestGCD
{
    public static void RunTests()
    {
        bool result1 = UnitTest_2();
        bool result2 = UnitTest_3();
        bool result3 = UnitTest_4();
        bool result4 = UnitTest_5();
        bool result5 = UnitTest_6();
        
        PrintTest("GCD_2", result1);
        PrintTest("GCD_3", result2);
        PrintTest("GCD_4", result3);
        PrintTest("GCD_5", result4);
        PrintTest("GCDStein", result5);
    }

    private static bool UnitTest_2()
    {
        int res = 4;
        int num = FindGCD.GCD(8, 4);
        return res == num;
    }

    private static bool UnitTest_3()
    {
        int res = 2;
        int num = FindGCD.GCD(8, 4, 2);
        return res == num;
    }

    private static bool UnitTest_4()
    {
        int res = 6;
        int num = FindGCD.GCD(120, 84, 12, 6);
        return res == num;
    }

    private static bool UnitTest_5()
    {
        int res = 3;
        int num = FindGCD.GCD(120, 84, 12, 6, 3);
        return res == num;
    }

    private static bool UnitTest_6()
    {
        int res = 4;
        int num = FindGCDStein.GCDStein(4, 16);
        return res == num;
    }
    
    private static void PrintTest(string testName, bool result)
    {
        if (result)
        {
            Console.WriteLine($"{testName}: PASS");
        }
        else
        {
            Console.WriteLine($"{testName}: ERROR");
        }
    }
}