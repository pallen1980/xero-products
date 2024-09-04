using System.Security.Cryptography;

string GetRandomNumberKey()
{
    using RandomNumberGenerator rng = RandomNumberGenerator.Create();

    byte[] key = new byte[32];
    rng.GetBytes(key);

    return Convert.ToBase64String(key);
}

Console.WriteLine(GetRandomNumberKey());

Console.ReadKey();