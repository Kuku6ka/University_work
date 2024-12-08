using System.Diagnostics;
using _1;

class Program
{
    static void Main()
    {
        _1.Switch sw = new _1.Switch();

        try
        {
            sw.DisconnectPowerGenerator();
            sw.VerifyPrimaryCoolantSystem();
            sw.VerifyBackupCoolantSystem();
            sw.GetCoreTemperature();
            sw.InsertRodCluster();
            sw.GetRadiationLevel();
            sw.SignalShutdownComplete();

            Console.WriteLine("All done!");
        }
        catch (PowerGeneratorCommsException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (CoolantTemperatureReadException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (CoolantPressureReadException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (CoolantSystemException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (CoreTemperatureReadException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (CoreRadiationLevelReadException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (RodClusterReleaseException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (SignallingException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}