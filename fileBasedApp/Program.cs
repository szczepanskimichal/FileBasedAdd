using System;
using fileBasedApp;
//omskriving til result
var fileNamesResult = Logic.GetArgs(args);
if (!fileNamesResult.IsSuccess)
{
    Console.WriteLine(fileNamesResult.Error);
    return 1;
}

var sumResult =Logic.ReadFileAndParseIntAndAdd(fileNamesResult.Value.InputFile1)(0) 
    .FlatMap(Logic.ReadFileAndParseIntAndAdd(fileNamesResult.Value.InputFile2))
        .FlatMap(Logic.SaveFile((fileNamesResult.Value.OutputFile)));

if (!sumResult.IsSuccess)
{
    Console.WriteLine(sumResult.Error);
    return 1;
}

Console.WriteLine($"Skrev resultatet {sumResult.Value} til '{fileNamesResult.Value.OutputFile}'.");
return 0;
