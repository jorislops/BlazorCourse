# Onderwerpen per week/les

Shop voorbeeld (Angular Course?) Verwijderen?? of laten staan?? en Dan toevoegen aan de navigatie. 

Ik denk dat  het misschien wel te veel is per les.
Misschien de onderwerpen wat meer verdelen over de lessen.
Uitwerken per les een voorbeeld/demo en opdracht(en). 
Dit om feeling met het materiaal/strof te krijgen en daarna pas toe te kunnen passen op de eindopdracht!

## Les 1 Razor Syntax & Voorbeeld & Structuur van Project
- Aanmaken van een Blazor project
  - @rendermode="new InteractiveServerRenderMode(prerender: !WebHostEnvironment.IsDevelopment())"
  - In app.razor
- Introductie van razor syntax (RazorSyntax.razor)
- Binding van data
- Events (Click)
- Simpel voorbeeld met todo
- Structuur van Blazor Project (hoog over)
  - Componenten idee
  - Pages
  - Hoe werkt Blazor Server

- Filmpjes van Frank Lui 

## Les 2 Componenten
- SPA framework (zie filmpje van Frank Lui over SPA en componenten)
- Structuur van Blazor Project (nogmaals herhaling om te kijken of het is blijven hangen en omdat het belangrijk is ivm componenten)
  - Componenten idee
  - Pages
  - Hoe werkt Blazor Server
- Components
  - Components as building blocks (lego)
  - Components as custom elements
  - Identifying components (screenshot)

State-management in components
- Components with Parameters
- Components & events
- Parent / child components
- Cascade Parameters

## Les 3 Verbinden met database 
- **Nog uitwerken onderstaande 3 items**
- (Al impliciet gedaan) Routes + Navigation (Chapter 4 - [Query strings](https://chrissainty.com/working-with-query-strings-in-blazor/))
- Route Parameters
- Query Strings

- Dapper herhaling
- Simpele CRUD voorbeeld
- Navigatie tussen pages, Route Parameters
- Repository Pattern gebruiken

## Les 4 Forms & Validatie & Routes & Layout
- EditForm
  - Validatie
  - Fluent Validation
- Generic Components
- RenderFragment
- Using a component library Blazorise (https://blazorise.com/)  

- Layouts ** #Todo **

## Les 5 Advanced Subjects ** #Todo **

- Dependency Injection
- State Management (Service)
- Lifecycle methods (out of scope?)  
- RenderTreeBuilder (out of scope?)

## Les 6 Uitloop


# Les 1 opdrachten

## Razor Syntax
- Maak een lijst aan van 10 random gehele getallen, waarbij je de even getallen groen maakt en de oneven getallen rood. Geef dit weer in een lijst (`<ul> <li>`).
- Maak een pagina die een sterren pyramide laat zien voor een bepaald getal), je mag 1 maken die jij leuk vindt. Je zou eventueel kunnen opgeven hoe groot de pyramide moet zijn (gebruik hiervoor een <input type="number" .../> .
  ![Pyramide](Images/pyramid.png)

## Binding van data
- Maak een pagina aan die gegeven een getal (<input type="number">) de tafel van dat getal laat zien.
- Een getal raden spelletje, waarbij je een getal moet raden tussen 1 en 100. Je geeft aan of de opgegeven getal te groot of te klein is dan het te raden getal. Als de gebruiker het getal heeft geraden, dan wordt het aantal pogingen getoond en maak het de pagina (of een div of iets dergelijks) groen.
- Maak een pagina aan die een lijst van namen laat zien, je kan een naam toevoegen of verwijderen. Voor het verwijderen zijn er twee opties dit kan m.b.v. de positie (index) of de naam, welke methode heeft jouw voorkeur en waarom?
- Er staan 5 knoppen op een pagina, als je op een knop klikt, dan wordt de naam van de knop getoond in een div.
- Maak een pagina aan die een lijst van namen laat zien, er is een sorteer knop, als je op de knop klikt, dan worden de namen gesorteerd. Als je nog een keer op de knop klikt, dan worden de namen in omgekeerde volgorde gesorteerd.
  - Hint: OrderBy en OrderByDescending
- in Pages/Todo/Todo.razor staan de opdrachten voor het uitbreiden van de todo pagina.

# Les 2 opdrachten

- **Nog te doen**: opdrachten met componenten aanmaken en die interactie hebben.
- Opdracht: maak twee counters aan (counter is een component). De eerste counter is voor de step size. Deze wordt door de tweede counter gebruikt. Gebruik hierbij chained binding (`<MyCounterExercise @bind-Counter="_stepSize"/>`).

- Maak het spel memory, waarbij je een aantal (b.v. 4x4) kaarten hebt en je moet de kaarten bij elkaar zoeken.
- Je zou b.v. eerst eens getallen kunnen gebruiken ipv afbeeldingen, zodat je kan zien of het werkt.
Elke kaart is een component, waarin wordt bijgehouden wat erop staat (getal) en of deze is omgedraaid of niet. 
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
  - Name
- Zorg ervoor dat je product en category koppelt m.b.v. een foreign key.
- Maak een pagina aan die de producten (drankjes te zien zijn), waarbij je deze ook kan filteren op categorie.
  - Ieder drankjes is een component, waarbij je de naam, prijs en categorie laat zien.
- Maak een pagina of component aan waarbij je een product kan toevoegen.
- Maak een pagina aan die de details van een product laat zien, waarbij je ook het product kan verwijderen.

## ComponentCommunication/TodoList.razor & gerelateerde componenten aanpassen
  - Kijk hoe de delete werkt in de repository (TodoRepository), echter willen we ook de kinderen verwijderen, dit doen we met twee DELETE queries.
  Pas dit aan zodat het met 1 DELETE query kan. Hiervoor moet je de foreign key constraint aanpassen (ON DELETE CASCADE).
  - Pas de componenten zo aan dat je een kind TodoItem (items) kan toevoegen b.v. in <TodoDetails>.
    *Let op:* de TodoCard, TodoDetails en TodoList staan in de directory Pages/ComponentCommunication.
  - Een TodoItem kan een kinderen hebben, deze kinderen kunnen ook weer kinderen hebben. Kan jij `<TodoDetails>` en 
  gerlateerde componenten zo aanpassen dat we onbeperkt diep kunnen gaan en op ieder niveau kindereren kunnen verwijderen?


## Bier crud opdrachten

- Bier Crud opdracht!
- Om bieren toe te voegen, worden stijl en type niet in een aparte tabel opgeslagen. Pas dit aan en ook de code Forms/AddBier.razor.
- Maak dus twee nieuwe repositories aan voor stijl en type.
- Maak ook twee nieuwe formulieren (pages) aan om stijl en type toe te voegen. (zie de AddBier.razor als inspiratie)
- Maak ook twee pagina's aan die alle stijlen en types tonen.
- Voeg de kolom voor biermerk toe in de tabel van BierPage, voeg ook een kolom voor stijl en type toe. Zorg ervoor dat we hierop kunnen sorteren. 
- Als je klikt op type of type, dan ga je naar de `BierPage.razor` die alle bieren van dat type laat zien. Hiervoor zul je een extra @page patroon moeten toevoegen (b.v. "/bier/stijl/LAGER,PALE").
- Als je klikt op type of stijl, dan ga je naar de `BierPage.razor` die alle bieren van die stijl laat zien.  Gebruik hiervoor een query string (b.v. "/bier?type=WEIZE"). Ik krijg er dorst van.
- BierPage maak een knop aan die naar de eerste page navigeert.
- BierPage maak een knop aan die naar de laatste page navigeert.
- De huidige pagina moet geselecteerd zijn in de pager (de nummer onder de tabel waarin je naar andere records van bieren tabel gaat).
- In de pager, voeg een ... toe als er meer dan 5 pagina's zijn en dan de laatste pagina nummer.
- Laat 5 pagina's voor/en na de huidige pagina zien ipv 10 pagina's.
- Maak extra zoekfunctionaliteit zodat je kan zoeken op naam van de brouwer.
- Maak extra `<select>`'s ([Uitleg select](https://www.w3schools.com/tags/tag_option.asp)) aan in de select voor stijl en type, zodat je kan zoeken op stijl en type.

- BierDelete: de brouwer naam wordt niet getoond, pas dit aan.
- Maak een pagina aan die een bier kan editen. Haal je inspiratie uit de AddBier.razor. Als je echt je best doet kan dit in 1 pagina. Echter misschien is het makkelijker om eerst een aparte pagina te maken.

- Maak een pagina die alle soorten bier toont ook onbekend, als je op een soort klikt ga je naar een pagina die alle bieren van die soort toont.

# Les 4 opdrachten - Forms & Validatie & Routes & Layout

- Zorg ervoor dat er input validatie is op de producten pagina. Dit betekent dat een product niet toegevoegd kan worden als de naam leeg is of de prijs niet een getal is groter dan 1 euro.
- Ook moet je de categorie selecteren, als deze niet is geselecteerd, dan kan je het product niet toevoegen.
- Maak een pagina aan waarbij je een category kan selecteren en dat je daarna de producten van die category ziet.
- Maak een pagina aan waarbij je de category kan toevoegen/verwijderen. Een category kan niet verwijderd worden als er nog producten zijn die deze category hebben, je moet dan eerst de producten verwijderen.
- Vraag aan de gebruiker of hij dit wil doen of niet.
- Maak een Layout aan voor je producten pagina, waarbij je een header en footer hebt. 
  - De header bevat een navigatie menu, waarbij je kan navigeren naar de producten pagina en de category pagina.
  - In de footer staat het aantal producten en categories die er zijn. 

# Les 5 opdrachten - Advanced Subjects

- Maak een service aan die verantwoordelijk is het manager van de state van het memory spel.
- Register Callbacks in deze services zodat componenten weten dat ze moeten updaten.



- Misschien leuk om het volgende te gebruiken (Joke Service):
- https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service

# Awsome Blazor
- [awesome-blazor 1](https://github.com/AdrienTorris/awesome-blazor)


# Bronnen
- [Blazor in Action - Book](https://learning.oreilly.com/library/view/blazor-in-action/9781617298646/)
- [Blazor Train - Video's concept oriented](https://blazortrain.com/)
- [mastering-state-management-in-blazor-a-comprehensive-guide] (https://kaushikroychowdhury.com/blog/mastering-state-management-in-blazor-a-comprehensive-guide/)

## Blazor state management
- [State management in Blazor - Don Wibier - NDC Porto 2022] (https://www.youtube.com/watch?v=L9p-9dGp-98)
- [Blazor state management with Fluxor] (https://www.youtube.com/watch?v=yM9F8rxo8L8)