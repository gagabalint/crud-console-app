# CRUD Console Application

Egyszerű konzol alapú CRUD (Create, Read, Update, Delete) alkalmazás C# .NET-ben, adatbázis kapcsolattal.
Hibakezelések a feladat követelményei miatt nem lettek implementálva.

## Funkciók

- **Create** - Új rekordok hozzáadása
- **Read** - Adatok listázása és lekérdezése
- **Update** - Meglévő adatok módosítása  
- **Delete** - Rekordok törlése
- Adatbázis kapcsolat kezelése
- Lekérdezések készítése

## Technológiák

- **C# .NET** - Programozási nyelv és framework
- **Entity Framework** - Adatbázis kezelés
- **SQL Server** - Adatbázis
- **Console Application** - Felhasználói interfész

## Követelmények

- .NET 8.0
- Visual Studio 2022 
- SQL Server

## Telepítés és futtatás

1. **Repository klónozása:**
   ```bash
   git clone https://github.com/gagabalint/crud-console-app.git
   ```
   2. **Projekt megnyitása:**
   - Dupla klik a `.sln` fájlra Visual Studio-ban
   - Vagy `dotnet run` parancs a projekt mappájában

3. **Futtatás:**
   - Visual Studio-ban: `F5` vagy `Ctrl+F5`
   - Parancssorból a Console mappában: `dotnet run`


## Használat

Az alkalmazás indítása után egy menü jelenik meg:

```
=== CRUD ALKALMAZÁS ===
Read Data
Add Data
Upload Data
...
```

Válassz egy opciót a nyilakkal és enterrel, majd követsd az utasításokat.


## Példa működés

```
Új vonal hozzáadása:
Enter the line number: 80
Enter the line name: Budapest-Sátoraljaújhely
The 80 has been added succesfully.

Vonal listázása:
Enter the number of the line: 80

The 80 line information:
            Line name: Budapest-Sátoraljaújhely
```

