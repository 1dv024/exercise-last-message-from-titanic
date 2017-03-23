# <i class="fa fa-laptop"></i> Sista meddelandet från Titanic

Hur lät det när John G. Philips sände det sista meddelandet från Titanic? Filen `LastMessage.txt` innehåller meddelandet bestående av den beprövade nödsignalen "CQD", den (då) nya "SOS"-signalen och Titanics anropssignal "MGY".

K
omplettera metoden `Main` i det påbörjade projektet med den kod som krävs för att spela upp meddelandet. Till din hjälp har du klassen `MorseCode` (som du inte får modifiera) med bl.a. metoden `Play` som kan spela upp signaler likväl som att visa dem "verbalt". Programmet ska fånga eventuella undantag som kastas, och presentera felmeddelandet som undantaget har.

Använd klassen `File` för att öppna filen och klassen `StreamReader` för att läsa filen rad för rad. För att kunna använda `File` och `StreamReader` måste du använda namnrymden `System.IO`. Använd det reserverade ordet `using` i samband med öppnandet av filen och läsandet av den så att den inte är öppen längre än nödvändigt även om ett undantag kastas av någon anledning. Projektet ska konfigureras så att filnamnet skickas som ett argument in till programmet, du behöver alltså inte fråga efter det. Genom att använda `args[0]` kan du hämta du filnamnet i samband med att du öppnar filen för läsning.

## <i class="fa fa-flag-checkered"></i> Mål

Efter att ha gjort uppgiften ska du känna till hur du:

- öppnar en textfil för läsning.
- läser en textfil rad för rad.
- använder `using` för att automatiskt stänga en fil oavsett vad som händer.

## <i class="fa fa-lightbulb-o"></i> Tips

I Visual Studio Code skickar du argument till en applikation genom att ange dem i `launch.json`.
```
...
"args": ["LastMessage.txt"],
...
```

Läs om:

- Essential C# 6.0, 232-235.
- File and Stream I/O, https://msdn.microsoft.com/en-us/library/k3352a4t.aspx
- Read Text from a File, https://msdn.microsoft.com/en-us/library/db5x7c0d.aspx
- using Statement, https://msdn.microsoft.com/en-us/library/yh598w02.aspx
- StreamReader Class, https://msdn.microsoft.com/en-us/library/system.io.streamreader(v=vs.110).aspx
- How to: Set Start Options for Application Debugging (Visual Studio 2010), https://msdn.microsoft.com/en-us/library/1ktzfy9w.aspx
- Visual Studio Code command line arguments when debugging, http://stackoverflow.com/questions/41189755/vscode-asp-net-core-command-line-arguments-when-debugging/41232623#41232623

## <i class="fa fa-flask"></i> Lösningsförslag

<ul class="fa-ul fa-border exercise-info">
<li><i class="fa-li fa fa-github"></i><a href="https://github.com/1dv024/exercise-solution-proposals/tree/master/exercise-last-message-from-titanic-array">https://github.com/1dv024/exercise-solution-proposals/tree/master/exercise-last-message-from-titanic</a></li>
</ul>
