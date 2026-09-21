# Faction Factory

Ett worldbuilding-verktyg för att skapa, spara och redigera factions till
fiktiva världar. Denna backend är ASP.NET webAPI, frontenden är byggd i React:
([ff-react-repot](https://github.com/johnsjodin/ff-react)).

## Förutsättningar

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)

## 1. Starta backend

I terminalen:

    git clone https://github.com/johnsjodin/ff-api.git
    cd ff-api
    dotnet run

API:et lyssnar på http://localhost:5211. Swagger-UI finns på
http://localhost:5211/swagger. Datafilen (factions.json) och
uploads-mappen skapas automatiskt vid behov.

## 2. Starta frontend

Starta en ny terminal och skriv:

    git clone https://github.com/johnsjodin/ff-react.git
    cd ff-react
    npm install
    npm run dev

Öppna http://localhost:5173 i din browser.

## Funktioner

- Lista, skapa och uppdatera factions (GET/POST/PUT, samt DELETE i API:et)
- Emblem-bild kan laddas upp per faction och visas i listan
- Misslyckade API-anrop visar felmeddelande i UI:t i stället för att krascha
- Responsiv: testad från desktop ner till 320px bredd

## Tekniska val

Jag kör med en JSON-fil som lagring i stället för en riktig databas. Det räcker
gott för så här lite data, och eftersom all logik ligger i en egen klass
(FactionStore) skulle jag kunna byta till en databas senare utan att röra
controllern. FactionStore är registrerad som singleton så alla anrop delar samma
instans.

Endpoints som returnerar data använder ActionResult<T> så att Swagger kan
visa vad man får tillbaka. PUT och DELETE svarar bara med statuskod, så där
räcker IActionResult.

Servern sätter id på nya factions (högsta id + 1). Klienten får inte välja
id själv, då kan två klienter råka ta samma.

Uppladdade bilder ligger i en egen uploads-mapp i stället för i wwwroot.
Dels är det användarfiler och inte en del av appen, dels finns det en bugg i
dotnet watch i .NET 10 som kraschar när nya filer dyker upp i wwwroot
(dotnet/roslyn#84062). Jag testade båda placeringarna, wwwroot kraschade och
uploads-mappen funkar. Mappen serveras via PhysicalFileProvider och skapas
automatiskt vid start. Servern namnger filerna själv (faction-{id}) och
godkänner bara filändelser för bilder.

Ingen React Router. Appen har tre vyer och byter mellan dem med en vanlig
state-variabel. Router hade gett riktiga URL:er och fungerande bakåtknapp,
men det behövs inte för en så här liten app. Nackdelen är att webbläsarens
bakåtknapp lämnar hela appen. Klicka på dödskallarna istället.

Nästan ingen media query. Layouten är centrerade kolumner med max-width och
de krymper av sig själva på små skärmar, jag har testat ner till ca 300px.
Enda stället som behövde en query var headern, där jag plockar bort mina fina
dödskalle-giffar, en och en, ju smalare skärmen blir.

Utseendet är lite Windows 95-inspirerat, men mer jobb finns att göra där. Knapparnas
3D-effekt är border-färger som byter plats när knappen trycks ner.
