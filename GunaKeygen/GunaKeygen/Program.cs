using System;
using System.IO;

namespace GunaKeygen
{
	internal class Program
    {
		private static void Main(string[] args)
		{
			string text = "txfusgxcjzjeydcy@txfusgxcjzjeydcy.onion";
			string text2 = "1";
			string text3 = "3";
			DateTime dateTime = new DateTime(2099, 12, 31);
			string text4 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GunaFramework");
			string text5 = Path.Combine(text4, "GunaUI2.cdm");
			if (!Directory.Exists(text4))
			{
				Directory.CreateDirectory(text4);
			}
			License license = new License();
			string license_data = string.Join("~", new string[]
			{
		text2,
		text3,
		dateTime.ToString("yyyy-MM-dd")
			});
			string text6 = license.GenActivationCode("ilham", text, license_data);
			string machineID = License.GetMachineID();
			string text7 = string.Join(",", new string[]
			{
		text,
		text6,
		machineID
			});
			string text8 = license.Encrypt(text7);
			File.WriteAllText(text5, text8);
			Console.WriteLine("EMAIL: " + text);
			Console.WriteLine("ACTIVATION CODE: " + text6);
			Console.WriteLine("PLAIN ACTIVATION DATA: " + text7);
			Console.WriteLine("ENC ACTIVATION DATA: " + text8);
			Console.WriteLine("LICENSE FILE: " + text5);
			Console.ReadKey();
		}
	}
}
