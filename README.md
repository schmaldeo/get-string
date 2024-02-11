# get-string
This is a tiny CLI application that generates a string of a specified length. 
Idea for it has been taken from [joereynolds/what-to-code](https://github.com/joereynolds/what-to-code).

## Usage
1. Download the program from [Releases](https://github.com/schmaldeo/get-string/releases) or 
compile it on your own.
2. Run it like this: `chars.exe 10` (example output: `>]x?(I,")N`).

You can also use flags to disallow certain characters in the output. For that you can
use:
- `--no-special-chars` to disallow 7-bit ASCII special characters (codes 33-47, 58-64, 91-96, 123-126)
- `--no-numbers` to disallow numbers

It is possible that more flags will be added in the future.