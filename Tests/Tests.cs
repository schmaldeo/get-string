using Chars;
using static System.Text.RegularExpressions.Regex;

namespace Tests;

public class Tests
{
	[Test]
	public void TestLength()
	{
		var length = 32;
		var charset = Program.GetCharset();
		var s = Program.GetString(length, charset);
		Assert.That(s, Has.Length.EqualTo(length));
	}

	[Test]
	public void TestNoSpecialChars()
	{
		var charset = Program.GetCharset(noSpecialChars: true, noNumbers: false);
		var s = Program.GetString(32, charset);
		Assert.That(IsMatch(s, "^[a-zA-Z0-9]+$"));
	}
	
	[Test]
	public void TestNoNumbers()
	{
		var charset = Program.GetCharset(noSpecialChars: false, noNumbers: true);
		var s = Program.GetString(32, charset);
		Assert.That(IsMatch(s, @"[\\x21-\\x2F\\x3A-\\x40\\x5B-\\x60\\x7B-\\x7E]"));
	}
	
	[Test]
	public void TestNoNumbersNoSpecialChars()
	{
		var charset = Program.GetCharset(noSpecialChars: true, noNumbers: true);
		var s = Program.GetString(32, charset);
		Assert.That(IsMatch(s, "^[a-zA-Z]+$"));
	}
}