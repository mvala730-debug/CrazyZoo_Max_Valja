# Crazy Zoo Vol. 3

Jätkab Vol. 2 projekti. Kasutaja valib vähemalt kolm looma ja CPU-mahuka töö. Rakendus käivitab sama sisendi järjestikuses või paralleelses režiimis, mõõdab iga objekti ja kogu töö kestust ning hoiab UI reageerivana.

## Funktsioonid
- järjestikune `await Task.Run(...)` iga objekti järel;
- paralleelne `Task.Run(...)` ja `Task.WhenAll(...)`;
- objektide andmete snapshot enne töö käivitamist;
- ühine `CancellationTokenSource` ja tühistamise kontroll CPU-tsüklis;
- iga objekti Success, Error või Cancelled raport;
- ühe objekti viga ei katkesta teiste tööd;
- objekti nimi, olek, tulemus või viga, kestus ja kogu aeg;
- vähemalt kolm valitavat maailma objekti.

## Käivitamine
```cmd
dotnet restore CrazyZooVol3.sln
dotnet build CrazyZooVol3.sln
dotnet run --project CrazyZoo.WpfApp
```
