![logo](https://github.com/pacifastacus/Medical-assistent/blob/master/Client/assistent-80.png "Logo")
# Medical-assistent
Egy orvosi rendelőben működő kliens – szerver alkalmazást kell készíteni.

## Asszisztens Kliens - .Net WPF desktop alkalmazás (Assistant project)
  * Az asszisztens irodájában működik
  * Az érkező betegeket tudja felrögzíteni (Név, lakcím, tajszám, panasz)
    * Név legyen valós (Unit tesztet létrehozni)
    * Tajszám validáció: 123-123-123 (Unit tesztet létrehozni)

## Orvos Kliens - .Net WPF desktop alkalmazás (Doctor project)
  * Az orvos irodájában működik
  * Lát egy listát az érkezés sorrendjében a betegekről (Név, Tajszám, utolsó módosítás dátuma)
  * Ha kiválaszt egy beteget a listából, látja az adatait illetve tudja törölni és módosítani (felvenni diagnózist)

## Szerver - .Net WEB API (self hosted console app) (ServerAPI project)
  * Tárolja és szolgáltatja a bevitt adatokat (szerver indításkor az előzőleg bevitt adatok jelenjenek meg
  * Adatok tárolása: Json, XML vagy adatbázis
