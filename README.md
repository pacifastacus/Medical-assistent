# Medical-assistent

Egy orvosi rendelőben működő kliens – szerver alkalmazást kell készíteni.
Asszisztens Kliens - .Net WPF desktop alkalmazás
  * Az asszisztens irodájában működik
  * Az érkező betegeket tudja felrögzíteni (Név, lakcím, tajszám, panasz)
    * Név legyen valós (Unit tesztet létrehozni)
    * Tajszám validáció: 123-123-123 (Unit tesztet létrehozni)

## Orvos Kliens - .Net WPF desktop alkalmazás
  * Az orvos irodájában működik
  * Lát egy listát az érkezés sorrendjében a betegekről (Név, Tajszám, utolsó módosítás dátuma)
  * Ha kiválaszt egy beteget a listából, látja az adatait illetve tudja törölni és módosítani (felvenni diagnózist)

## Szerver - .Net WEB API (self hosted console app):
  * Tárolja és szolgáltatja a bevitt adatokat (szerver indításkor az előzőleg bevitt adatok jelenjenek meg
  * Adatok tárolása: Json, XML vagy adatbázis
