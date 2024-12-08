using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        string fullname = "Кушин,Олег,Романович";

        byte[] fullnamebytes = Encoding.UTF8.GetBytes(fullname);

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(fullnamebytes);
            string stringhash = BitConverter.ToString(hash);
            Console.WriteLine(stringhash);
        }

        using (MD5 md5 = MD5.Create())
        {
            byte[] hash = md5.ComputeHash(fullnamebytes);
            string stringhash = BitConverter.ToString(hash);
            Console.WriteLine($"\n{stringhash}");
        }
    }
}