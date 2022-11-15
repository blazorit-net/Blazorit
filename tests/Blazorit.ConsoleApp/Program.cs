// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;

Console.WriteLine("Hello, World!");

string password = (0).ToString();
byte[] passwordHash = { 0 };
byte[] passwordSalt = { 0 };
Console.WriteLine($"result = {VerifyPasswordHash(password, passwordHash, passwordSalt)}");


bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) {
    using (var hmac = new HMACSHA512(passwordSalt)) {
        var computedHash =
            hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //Console.WriteLine(computedHash
        return computedHash.SequenceEqual(passwordHash);
    }
}