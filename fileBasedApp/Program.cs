using System;
using fileBasedApp;

var fileNamesResult = Logic.GetArgs(args);
if (!fileNamesResult.IsSuccess)
{
    Console.WriteLine(fileNamesResult.Error);
    return 1;
}

//int number1;
var number1Result = Logic.ReadFileAndParseIntAndAdd(fileNamesResult.Value.InputFile1);
//int number2;
var number2Result = Logic.ReadFileAndParseIntAndAdd(fileNamesResult.Value.InputFile2);

if (!number1Result.IsSuccess)
{
    Console.WriteLine(number1Result.Error);
    return 1;
}

if (!number2Result.IsSuccess)
{
    Console.WriteLine(number2Result.Error);
    return 1;
}

int sum = number1Result.Value + number2Result.Value;

try
{
    if (File.Exists(outputFile))
    {
        Console.Error.WriteLine($"Feil: Utfilen '{outputFile}' finnes allerede.");
        return 1;
    }

    File.WriteAllText(outputFile, sum.ToString());

    Console.WriteLine($"Skrev resultatet {sum} til '{outputFile}'.");
    return 0;
}
catch (UnauthorizedAccessException)
{
    Console.Error.WriteLine($"Feil: Har ikke tilgang til å skrive til '{outputFile}'.");
    return 1;
}
catch (DirectoryNotFoundException)
{
    Console.Error.WriteLine($"Feil: Mappen til '{outputFile}' finnes ikke.");
    return 1;
}
catch (IOException ex)
{
    Console.Error.WriteLine($"Feil: Klarte ikke å skrive til '{outputFile}'.");
    Console.Error.WriteLine(ex.Message);
    return 1;
}