# Onderwerpen per week/les

Ik denk dat  het misschien wel te veel is per les.
Misschien de onderwerpen wat meer verdelen over de lessen.
Uitwerken per les een voorbeeld/demo en opdracht(en). 
Dit om feeling met het materiaal/strof te krijgen en daarna pas toe te kunnen passen op de eindopdracht!

## Les 1 Razor Syntax & Voorbeeld & Structuur van Project
- Introductie van razor syntax (RazorSyntax.razor)
- Binding van data
- Events (Click)
- Simpel voorbeeld met todo
- Structuur van Blazor Project

## Les 1 Opdrachten
  - wat simple opdrachten met razor syntax
  - wat simple opdrachten met binding van data
  - wat simple opdrachten met events
  - wat simple opdrachten met todo demo uitbreiden
    (weghalen lijkt me)
  - https://www.learnblazor.com/tutorial/tour-of-heroes/ (Echter dan niet HTTP en met Server Pages) 

## Les 2 Componenten
- Components
  - Components as building blocks (lego)
  - Components as custom elements
  - Identifying components (screenshot)
- Components with Parameters
- Components & events
- Parent / child components

## Les 3 Verbinden met database 
- Dapper herhaling
- Simpele CRUD voorbeeld
- Navigatie tussen pages, Route Parameters
- Repository Pattern gebruiken

## Les 4 Forms & Validatie & Routes & Layout
- EditForm
  - Validatie
- Routes + Navigation (Chapter 4 - [Query strings](https://chrissainty.com/working-with-query-strings-in-blazor/))
   - Route Parameters
   - Query Strings
- Layouts

## Les 5 Advanced Subjects

- Lifecycle methods (maybe to advanced)
- Dependency Injection
- Services
- State Management (Service)


## Dependency Injection & Services & State Management (Service) & Repository Pattern

Structuur aanbrengen in de code, zodat het overzichtelijk blijft (Repo's, Models, etc)
Of is dit te veel/lastig voor studenten?
Nog even kijken naar oud materiaal (Razor Pages) 
misschien zitten daar ook wel intressante onderwepen in.


# Les 1 opdrachten

- Maak een pagina aan die gegeven een getal de tafel van dat getal laat zien.
- Een getal raden spelletje, waarbij je een getal moet raden tussen 1 en 100. Je geeft aan of de opgegeven getal te groot of te klein is dan het te raden getal. Als de gebruiker het getal heeft geraden, dan wordt het aantal pogingen getoond en maak het de pagina (of een div of iets dergelijks) groen.
- Maak een pagina die een sterren pyramide laat zien voor een bepaald getal), je mag een maken die jij leuk vindt. Je zou eventueel kunnen opgeven hoe groot de pyramide moet zijn. 
![Pyramide](Images/pyramid.png)
- Maak een pagina aan die een lijst van namen laat zien, je kan een naam toevoegen of verwijderen.

# Les 2 opdrachten

- Maak het spel memory, waarbij je een aantal kaarten hebt en je moet de kaarten bij elkaar zoeken. 
Elke kaart is een component, waarin wordt bijgehouden wat erop staat en of deze is omgedraaid of niet. 
Als je een kaart omdraait, dan wordt de kaart getoond. Als je twee kaarten hebt omgedraaid, dan wordt gekeken of deze hetzelfde zijn. Als dit zo is, dan blijven de kaarten omgedraaid, anders worden ze weer omgedraaid.
Maak ook een component die bijhoudt hoeveel kaarten er zijn omgedraaid en hoeveel er goed zijn.
Maak ook een component die de kaarten laat zien en waarbij je een nieuwe spel kan starten.
Maak ook een component aan die bijhoudt per speler hoeveel punten deze heeft (misschien hergebruik van eerder gemaakt component).

# Les 3 opdrachten - CRUD

- Maak een product tabel aan in de database, waarbij je de volgende kolommen hebt:
  - Id
  - Name
  - Price
  - CategoryId
- Maak een category tabel aan in de database, waarbij je de volgende kolommen hebt:
  - Id
  - Bane
- Zorg ervoor dat je product en categort koppelt m.b.v. een foreign key.
- Maak een pagina aan die waarop de producten (drankjes te zien zijn), waarbij je deze ook kan filteren op categorie.
  - Ieder drankjes is een component, waarbij je de naam, prijs en categorie laat zien.
- Maak een pagina of component aan waarbij je een todo kan toevoegen.
- Maak een pagina aan die de details van een product laat zien, waarbij je ook het product kan verwijderen.

# Les 4 opdrachten - Forms & Validatie & Routes & Layout

- Zorg ervoor dat er input validatie is op de producten pagina. Dit betekent dat een product niet toegevoegd kan worden als de naam leeg is of de prijs niet een getal is groter dan 1 euro.
- Ook moet je de categorie selecteren, als deze niet is geselecteerd, dan kan je het product niet toevoegen.
- Maak een pagina aan waarbij je een category kan selecteren en dat je daarna de producten van die category ziet.
- Maak een pagina aan waarbij je de category kan toevoegen/verwijderen. Een category kan niet verwijderd worden als er nog producten zijn die deze category hebben, je moet dan eerst de producten verwijderen.
Vraag aan de gebruiker of hij dit wil doen of niet.
- Maak een Layout aan voor je producten pagina, waarbij je een header en footer hebt. 
  - De header bevat een navigatie menu, waarbij je kan navigeren naar de producten pagina, de category pagina.
  - In de footer staat het aantal producten en categories dat er zijn. 

# Les 5 opdrachten - Advanced Subjects

- Maak een service aan die verantwoordelijk is het manager van de state van memory spel
- Register Callbacks in deze services zodat componenten weten dat ze moeten updaten.

# Todo voorbeelden

- Category toevoegen


# Bier crud opdrachten: deze moeten dus nog ergens komen!

- Bier Crud opdracht!
- Om bieren toe te voegen, worden stijl en type niet in een aparte tabel opgeslagen. Pas dit aan en ook de code Forms/AddBier.razor.
- Maak dus twee nieuwe repositories aan voor stijl en type.
- Maak ook twee nieuwe formulieren (pages) aan om stijl en type toe te voegen. (zie de AddBier.razor als inspiratie)
- Maak ook twee pagina's aan die alle stijlen en types tonen.


- Voeg de kolom voor biermerk toe in de tabel van BierPage4.
- BierPage4 maak een knop aan die naar de eerste page navigeert.
- BierPage4 maak een knop aan die naar de laatste page navigeert.
- De huidige pagina moet geselecteerd zijn in de pager.
- Voeg een ... toe als er meer dan 5 pagina's zijn en dan de laatste pagina nummer.
- Laat 5 pagina's voor/en na de huidige pagina zien ipv 10 pagina's. 
- Maak extra zoekfunctionaliteit zodat je kan zoeken op naam van de brouwer

- BierDelete: de brouwer naam wordt niet getoond, pas dit aan.
- Maak een pagina aan die een bier kan editen. Haal je inspiratie uit de AddBier.razor. Als je echt je best doet kan in 1 pagina. Echter misschien is het makkelijker om eerst een aparte pagina te maken.

- Maak een pagina die alle soorten bier toont ook onbekend, als je op een soort klikt ga je naar een pagina die alle bieren van die soort toont.

- Misschien leuk om het volgende te gebruiken (Joke Service):
- https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service

# Bronnen
- [Blazor in Action - Book](https://learning.oreilly.com/library/view/blazor-in-action/9781617298646/)
- [Blazor Train - Video's concept oriented](https://blazortrain.com/)
- [mastering-state-management-in-blazor-a-comprehensive-guide] (https://kaushikroychowdhury.com/blog/mastering-state-management-in-blazor-a-comprehensive-guide/)

## Blazor state management
- [State management in Blazor - Don Wibier - NDC Porto 2022] (https://www.youtube.com/watch?v=L9p-9dGp-98)
- [Blazor state management with Fluxor] (https://www.youtube.com/watch?v=yM9F8rxo8L8)