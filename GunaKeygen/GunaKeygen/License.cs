using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace GunaKeygen
{
	public class License
	{
		private string string_7 = "P@@Sw0rd";

		private string string_8 = "S@LT&KEY";

		private string string_9 = "@1B2c3D4e5F6g7H8";
		public string GenActivationCode(string salt, string email, string license_data)
		{
			string text = this.CalcHash(salt + email);
			string text2 = string.Empty;
			checked
			{
				for (int i = 1; i <= license_data.Length; i++)
				{
					text2 += (unchecked((long)Strings.Asc(license_data[checked(i - 1)]) ^ (long)Strings.Asc(text[i % text.Length]))).ToString("X2");
				}
				return text2;
			}
		}

		private string CalcHash(string string_10)
		{
			HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
			byte[] array = Encoding.ASCII.GetBytes(string_10);
			array = hashAlgorithm.ComputeHash(array);
			string text = "";
			foreach (byte b in array)
			{
				text += b.ToString("x2");
			}
			return text;
		}

		public static string GetMachineID()
		{
			string result;
			try
			{
				result = Conversions.ToString(Operators.AddObject(Conversions.ToString(Operators.AddObject("", Registry.GetValue("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "ProcessorNameString", "??"))), Registry.GetValue("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\BIOS", "SystemManufacturer", "")));
			}
			catch (Exception)
			{
				result = "??";
			}
			return result;
		}

		public string Encrypt(string string_10)
		{
			string result;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(string_10);
				byte[] bytes2 = new Rfc2898DeriveBytes(this.string_7, Encoding.ASCII.GetBytes(this.string_8)).GetBytes(32);
				ICryptoTransform transform = new RijndaelManaged
				{
					Mode = CipherMode.CBC,
					Padding = PaddingMode.Zeros
				}.CreateEncryptor(bytes2, Encoding.ASCII.GetBytes(this.string_9));
				byte[] inArray;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
					{
						cryptoStream.Write(bytes, 0, bytes.Length);
						cryptoStream.FlushFinalBlock();
						inArray = memoryStream.ToArray();
						cryptoStream.Close();
					}
					memoryStream.Close();
				}
				result = Convert.ToBase64String(inArray);
			}
			catch
			{
				result = "";
			}
			return result;
		}
	}
}
