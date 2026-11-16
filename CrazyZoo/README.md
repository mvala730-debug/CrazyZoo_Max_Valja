# Crazy Zoo Vol. 3

Parandatud Release-versioon. Vol. 2 hoidlad, sündmused, MVVM-failid ja kõik kaheksa loomaliiki on säilitatud. Vol. 3 lisab järjestikuse ja paralleelse CPU-arvutuse, snapshotid, Stopwatch mõõtmise, CancellationToken tühistamise ning objektipõhised Success/Error/Cancelled raportid.

## Release-kontroll

```cmd
dotnet clean CrazyZooVol3.sln -c Release
dotnet build CrazyZooVol3.sln -c Release
dotnet run --project CrazyZoo.WpfApp -c Release
```

Ühe objekti vea kontrollimiseks lisa loom nimega `Error` ja vali see koos vähemalt kahe teise loomaga.
