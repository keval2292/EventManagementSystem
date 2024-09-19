using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace EventMentorSystem.Data
{
    public static class StringCipher
    {
        public static string Encode(string text)
        {

            byte[] mybyte = Encoding.UTF32.GetBytes(text);
            string returntext = Convert.ToBase64String(mybyte);
            return returntext;
        }

        public static string Decode(string text)
        {
            byte[] mybyte = Convert.FromBase64String(text);
            string returntext = Encoding.UTF32.GetString(mybyte);
            return returntext;
        }
        //public static string Encode(string ourText)
        //{
        //    try
        //    {
        //        string _key = "abcdefgh";
        //        string privatekey = "hgfedcba";
        //        byte[] privatekeyByte = { };
        //        privatekeyByte = Encoding.UTF8.GetBytes(privatekey);
        //        byte[] _keybyte = { };
        //        _keybyte = Encoding.UTF8.GetBytes(_key);
        //        byte[] inputtextbyteArray = System.Text.Encoding.UTF8.GetBytes(ourText);
        //        using (DESCryptoServiceProvider dsp = new DESCryptoServiceProvider())
        //        {
        //            var memstr = new MemoryStream();
        //            var crystr = new CryptoStream(memstr, dsp.CreateEncryptor(_keybyte, privatekeyByte), CryptoStreamMode.Write);
        //            crystr.Write(inputtextbyteArray, 0, inputtextbyteArray.Length);
        //            crystr.FlushFinalBlock();
        //            return Convert.ToBase64String(memstr.ToArray());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //public static string Decode(string ourText)
        //{
        //    try
        //    {

        //        string _key = "abcdefgh";
        //        string privatekey = "hgfedcba";
        //        byte[] privatekeyByte = { };
        //        privatekeyByte = Encoding.UTF8.GetBytes(privatekey);
        //        byte[] _keybyte = { };
        //        _keybyte = Encoding.UTF8.GetBytes(_key);
        //        byte[] inputtextbyteArray = new byte[ourText.Replace(" ", "+").Length];
        //        //This technique reverses base64 encoding when it is received over the Internet.
        //        inputtextbyteArray = Convert.FromBase64String(ourText.Replace(" ", "+"));
        //        using (DESCryptoServiceProvider dEsp = new DESCryptoServiceProvider())
        //        {
        //            var memstr = new MemoryStream();
        //            var crystr = new CryptoStream(memstr, dEsp.CreateDecryptor(_keybyte, privatekeyByte), CryptoStreamMode.Write);
        //            crystr.Write(inputtextbyteArray, 0, inputtextbyteArray.Length);
        //            crystr.FlushFinalBlock();
        //            return Encoding.UTF8.GetString(memstr.ToArray());
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }
}