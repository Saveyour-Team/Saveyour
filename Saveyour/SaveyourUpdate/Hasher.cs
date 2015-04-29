using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SaveyourUpdate
{
    internal enum HashType
    {
        MD5,
        SHA1,
        SHA512
    }

    /*
     * The purpose of this class is to organize and get various hash values using MD5, SHA1, and SHA512 algorithms. 
     * */

    internal static class Hasher
    {
        /*
         * This method returns the hash value of a file at filePath. The hash value is of type algo, which can be MD5, SHA1, or SHA512.
         * For the purpose of SaveyourUpdater, only MD5 will be used.
         * */
        internal static String HashFile(String filePath, HashType algo)
        {
            switch (algo)
            {
                case HashType.MD5:
                    return MakeHashString(MD5.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                case HashType.SHA1:
                    return MakeHashString(SHA1.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                case HashType.SHA512:
                    return MakeHashString(SHA512.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                default:
                    return "";
            }
        }

        /*
         * Converts the byte array created by System.Cryptography when hashing a file to a string. 
         * */
        private static String MakeHashString(byte[] hash)
        {
            StringBuilder s = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
                s.Append(b.ToString("X2").ToLower());

            return s.ToString();
        }
    }
}
