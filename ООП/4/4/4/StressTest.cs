namespace StressTest
{
    // Enumerations Exercise 1
    /// <summary>
    /// Enumeration of girder material types
    /// </summary>
    public enum Material 
    {
        StainlessSteel,
        Aluminium,
        ReinforcedConcrete,
        Composite,
        Titanium 
    }

    /// <summary>
    /// Enumeration of girder cross-sections
    /// </summary>
    public enum CrossSection
    {
        IBeam, 
        Box, 
        ZShaped, 
        CShaped
    }

    /// <summary>
    /// Enumeration of test results
    /// </summary>
    public enum TestResult
    {
        Pass, 
        Fail
    }
    // Structures Exercise 2
    /// <summary>
    /// Structure containing test results
    /// </summary>
    public struct TestCaseResult
    {
        /// <summary>
        /// Test result (enumeration type)
        /// </summary>
        public TestResult Result;
        /// <summary>
        /// Description of reason for failure
        /// </summary>
        public string ReasonForFailure;

        public TestCaseResult(TestResult result, string reasonForFailure)
        {
            Result = result;
            ReasonForFailure = reasonForFailure;
        }
    }
    
    public static class TestManager
    {
        // Метод для генерации случайного результата теста
        public static TestCaseResult GenerateResult()
        {
            // Генерация случайного результата Pass или Fail
            TestResult result = (new Random().Next(2) == 0) ? TestResult.Pass : TestResult.Fail;

            int cross = (new Random().Next(0, 3));
            int mat = (new Random().Next(0, 4));

            CrossSection crossSection;
            Material Mater;
            
            switch (cross)
            {
                case 0:
                    crossSection = CrossSection.IBeam;
                    break;
                case 1:
                    crossSection = CrossSection.Box;
                    break;
                case 2:
                    crossSection = CrossSection.ZShaped;
                    break;
                case 3:
                    crossSection = CrossSection.CShaped;
                    break;
            }

            switch (mat)
            {
                case 0:
                    Mater = Material.StainlessSteel;
                    break;
                case 1:
                    Mater = Material.Aluminium;
                    break;
                case 2:
                    Mater = Material.ReinforcedConcrete;
                    break;
                case 3:
                    Mater = Material.Composite;
                    break;
                case 4:
                    Mater = Material.Titanium;
                    break;
            }
            
            string reason = result == TestResult.Fail ? $"Причина отказа: Перегрузка" : "";

            return new TestCaseResult(result, reason);
        }
    }
}