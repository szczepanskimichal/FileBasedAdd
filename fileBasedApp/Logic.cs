namespace fileBasedApp;

public record FileNames(string InputFile1, string InputFile2, string OutputFile);
public class Logic
{
    public static Result<FileNames> GetArgs(string[] args)
    {
        if (args.Length != 3)
        {
            return Result<FileNames>.Failure(
                "Feil: Du må angi nøyaktig tre filnavn.\n" + 
                "Bruk: dotnet run <tallfil1> <tallfil2> <resultatfil>"
                );
        }

        string inputFile1 = args[0];
        string inputFile2 = args[1];
        string outputFile = args[2];

        return Result<FileNames>.Success(
            new FileNames(args[0],
                args[1],
                args[2]
            ));
    }

    public static Result<int> ReadFileAndParseIntAndAdd(string fileName)
    {
        try
        {
            string text1 = File.ReadAllText(fileName).Trim();

            if (!int.TryParse(text1, out int number))
            {
                return Result<int>.Failure(
                    $"Feil: Innholdet i '{fileName}' er ikke et gyldig heltall.\n" +
                    $"Innhold: '{text1}'"
                );
            }
            return Result<int>.Success(number);
        }
        catch (FileNotFoundException)
        {
            return Result<int>.Failure(
                $"Feil: Fant ikke filen '{fileName}'."
            );
        }
        catch (UnauthorizedAccessException)
        {
            return Result<int>.Failure(
                $"Feil: Har ikke tilgang til å lese filen '{fileName}'."
            );
        }
        catch (IOException ex)
        {
            return Result<int>.Failure(
                $"Feil: Det oppsto en IO-feil når filen '{fileName}' skulle leses.\n" +
                $"Detaljer: {ex.Message}"
            );
        }
    }
}


