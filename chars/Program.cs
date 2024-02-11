using System.CommandLine;
using System.Text;

namespace Chars;

public static class Program
{
	[Flags]
	private enum StringOptions
	{
		LowercaseLetters = 1,
		UppercaseLetters = 1 << 1,
		Numbers = 1 << 2,
		SpecialCharacters = 1 << 3
	} 

	public static int Main(string[] args)
	{
		var rootCommand = new RootCommand("An app returning a 7-bit ASCII string of given length.");

		var lengthArgument = new Argument<int>("length", "Length of the string.");
		var specialCharsOption = new Option<bool>("--no-special-chars", "Returns a string without special characters.");
		var numbersOption = new Option<bool>("--no-numbers", "Returns a string without numbers.");

		rootCommand.AddArgument(lengthArgument);
		rootCommand.AddOption(specialCharsOption);
		rootCommand.AddOption(numbersOption);

		rootCommand.SetHandler(HandleInput, lengthArgument, specialCharsOption, numbersOption);

		return rootCommand.Invoke(args);
	}

	private static void HandleInput(int length, bool noSpecialChars, bool noNumbers)
	{
		// check the length provided in CLI
		if (length < 0)
		{
			Console.Error.WriteLine("Length cannot be negative");
			Environment.Exit(1);
		}

		var charset = GetCharset(noSpecialChars, noNumbers);
		Console.WriteLine(GetString(length, charset));
	}

	public static string GetString(int length, int[] charset)
	{
		StringBuilder stringBuilder = new(length);
		Random random = new();

		// generate string of a given length
		for (var i = 0; i < length; i++)
		{
			var index = random.Next(charset.Length);
			stringBuilder.Append((char)charset[index]);
		}

		return stringBuilder.ToString();
	}

	public static int[] GetCharset(bool noSpecialChars = false, bool noNumbers = false)
	{
		// by default both uppercase and lowercase letters are used, however a distinction is used for future development
		var charsetOption = StringOptions.LowercaseLetters | StringOptions.UppercaseLetters |
		                    (!noSpecialChars ? StringOptions.SpecialCharacters : 0) |
		                    (!noNumbers ? StringOptions.Numbers : 0);

		// create a bunch of IEnumerables containing 7-bit ASCII codes
		var uppercaseCharset = Enumerable.Range(65, 90 - 65 + 1);
		var lowercaseCharset = Enumerable.Range(97, 122 - 97 + 1);
		var letterCharset = uppercaseCharset.Concat(lowercaseCharset);
		var numberCharset = Enumerable.Range(48, 57 - 48 + 1);
		var specialCharset = Enumerable.Range(33, 47 - 33 + 1).Concat(Enumerable.Range(58, 7))
			.Concat(Enumerable.Range(91, 6)).Concat(Enumerable.Range(123, 4));

		return charsetOption switch
		{
			// letters and numbers
			StringOptions.LowercaseLetters | StringOptions.UppercaseLetters | StringOptions.Numbers =>
				letterCharset.Concat(numberCharset).ToArray(),
			// letters and special characters
			StringOptions.LowercaseLetters | StringOptions.UppercaseLetters | StringOptions.SpecialCharacters =>
				letterCharset.Concat(specialCharset).ToArray(),
			// letters, numbers and special characters
			StringOptions.LowercaseLetters | StringOptions.UppercaseLetters | StringOptions.Numbers |
				StringOptions.SpecialCharacters =>
				letterCharset.Concat(numberCharset).Concat(specialCharset).ToArray(),
			// letters only
			_ => letterCharset.ToArray()
		};
	}
}