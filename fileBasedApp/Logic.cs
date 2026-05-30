namespace fileBasedApp;

public record Inputs(string InputFile1, string InputFile2, string OutputFile);
public class Logic
{
    public static Result<Inputs> GetArgs(string[] args)
    {
        if (args.Length != 3)
        {
            return Result<Inputs>.Failure(
                "Feil: Du må angi nøyaktig tre filnavn.\n" + 
                "Bruk: dotnet run <tallfil1> <tallfil2> <resultatfil>"
                );
        }

        string inputFile1 = args[0];
        string inputFile2 = args[1];
        string outputFile = args[2];

        return Result<Inputs>.Success(
            new Inputs(args[0],
                args[1],
                args[2]
            ));
    }

    
    //function without currying
  /*  public static Result<int> ReadFileAndParseIntAndAdd(string fileName, int total)
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
*/
  // function with currying
  public static Func<int,Result<int>> ReadFileAndParseIntAndAdd(string fileName)
  {
      return (int total) =>
      {
          try
          {
              string text1 = File.ReadAllText(fileName).Trim();

              if (!int.TryParse(text1, out int number))
              {
                  return Result<int>.Failure(
                      $"Feil: Innholdet i '{fileName}' er ikke et gyldig heltall.\n" +
                      $"Innhold: '{text1}'");
              }

              return Result<int>.Success(number + total);
          }
          catch (FileNotFoundException)
          {
              return Result<int>.Failure($"Feil: Fant ikke filen '{fileName}'.");
          }
          catch (UnauthorizedAccessException)
          {
              return Result<int>.Failure($"Feil: Har ikke tilgang til å lese filen '{fileName}'.");
          }
          catch (IOException ex)
          {
              return Result<int>.Failure($"Feil: Klarte ikke å lese filen '{fileName}'.\n" + ex.Message);
          }
      };
  }
  
  //function without currying
  /*public static Result<int> SaveFile(string fileName, int number)
    {
        try
        {
            if (File.Exists(fileName))
            {
                return Result<int>.Failure(
                $"Feil: Utfilen '{fileName}' finnes allerede."
                    );
            }

            File.WriteAllText(fileName, number.ToString());

            //Console.WriteLine($"Skrev resultatet {number} til '{fileName}'.");
            return Result<int>.Success(number);
        }
        catch (UnauthorizedAccessException)
        {
            return Result<int>.Failure(
                 $"Feil: Har ikke tilgang til å skrive til '{fileName}'."
            );
            return Result<int>.Success(number);
        }
        catch (DirectoryNotFoundException)
        {
            return Result<int>.Failure(
                 $"Feil: Mappen til '{fileName}' finnes ikke."
            );
            return Result<int>.Success(number);
        }
        catch (IOException ex)
        {
            return Result<int>.Failure(
                 $"Feil: Det oppsto en IO-feil når det skulle skrives til '{fileName}'.\n" +
                 $"Detaljer: {ex.Message}"
            );
            return Result<int>.Success(number);
        } 
    }
}
*/
// function with currying
    public static Func<int, Result<int>> SaveFile(string fileName)
    {
        return (int number) =>
        {
            try
            {
                if (File.Exists(fileName))
                {
                    return Result<int>.Failure($"Feil: Utfilen '{fileName}' finnes allerede.");
                }

                File.WriteAllText(fileName, number.ToString());

                return Result<int>.Success(number);
            }
            catch (UnauthorizedAccessException)
            {
                return Result<int>.Failure($"Feil: Har ikke tilgang til å skrive til '{fileName}'.");
            }
            catch (DirectoryNotFoundException)
            {
                return Result<int>.Failure($"Feil: Mappen til '{fileName}' finnes ikke.");
            }
            catch (IOException ex)
            {
                return Result<int>.Failure($"Feil: Klarte ikke å skrive til '{fileName}'.\n" + ex.Message);
            }
        };
    }
}