using System.CommandLine;
using System.Text;

internal class Program
{
	// Initialise arrays like this for maximum performance 
	private static readonly int[] CHARS_ASCII =
	[
		33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50,
		51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68,
		69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86,
		87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103,
		104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117,
		118, 119, 120, 121, 122, 123, 124, 125, 126
	];

	private static readonly int[] CHARS_ASCII_NO_SPECIAL =
	[
		48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83,
		84, 85, 86, 87, 88, 89, 90, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114,
		115, 116, 117, 118, 119, 120, 121, 122
	];

	private static readonly int[] CHARS_ASCII_NO_NUMBERS =
	[
		58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86,
		87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112,
		113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126
	];

	private static readonly int[] CHARS_ASCII_NO_NUMBERS_NO_SPECIAL =
	[
		65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83,
		84, 85, 86, 87, 88, 89, 90, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114,
		115, 116, 117, 118, 119, 120, 121, 122
	];

	public static int Main(string[] args)
	{
		var rootCommand = new RootCommand("An app returning a string of given length.");

		var lengthArgument = new Argument<int>("length", "Length of the string.");
		var specialCharsOption = new Option<bool>("--no-special-chars", "Returns a string without special characters.");
		var numbersOption = new Option<bool>("--no-numbers", "Returns a string without numbers.");

		rootCommand.AddArgument(lengthArgument);
		rootCommand.AddOption(specialCharsOption);
		rootCommand.AddOption(numbersOption);

		rootCommand.SetHandler(HandleCommand, lengthArgument, specialCharsOption, numbersOption);

		return rootCommand.Invoke(args);
	}

	private static void HandleCommand(int length, bool noSpecialChars, bool noNumbers)
	{
		if (length < 0)
		{
			Console.Error.WriteLine("Length cannot be negative");
			Environment.Exit(1);
		}

		Console.WriteLine(GetString(length, noSpecialChars, noNumbers));
	}

	private static string GetString(int length, bool noSpecialChars, bool noNumbers)
	{
		StringBuilder stringBuilder = new(length);
		Random random = new();

		int[] charSet;

		if (noSpecialChars && noNumbers)
			charSet = CHARS_ASCII_NO_NUMBERS_NO_SPECIAL;
		else if (noSpecialChars)
			charSet = CHARS_ASCII_NO_SPECIAL;
		else if (noNumbers)
			charSet = CHARS_ASCII_NO_NUMBERS;
		else
			charSet = CHARS_ASCII;

		for (var i = 0; i < length; i++)
		{
			var index = random.Next(charSet.Length);
			stringBuilder.Append((char)charSet[index]);
		}

		return stringBuilder.ToString();
	}
}