## Inledning

> Då kurserna Webbteknik II och ASP.NET MVC pågick parallellt var min tanke att kombinera dessa i ett enda stort 
> slutprojekt istället för att skapa individuella slutprojekt för respektive kurs. Jag valde därför att skapa den 
> föreslagna väderapplikationen från kursen ASP.NET MVC 1DV409 som samtidigt ska uppfylla kraven för båda kurserna.  
> Då det är en föreslagen applikation i den kursen så finns det säkerligen ett antal liknande applikationer tillgängliga.

## Väderapplikationens arkitektur

![Alt applikationsarkitektur](/Img/arkitektur.png "Applikationens arkitektur")

## Serversida

> Webbapplikationen är skapad med ramverket Microsoft ASP.NET MVC 5 och progrmmeringsspråket C#. För persistant lagring av 
> data används relationsdatabasen SQL Server 2008 R2. För hantering av persistant lagring av data används Microsoft Entity   > Framework, vilket är ett ORM (Object Relational Mapper) som underlättar kopplingen mellan C#-klasser och tabellerna i 
> databasen. 

> Webbapplikationen är uppdelad i två projekt där ena projektet, domänmodellen Weather.Domain, hanterar all kod på 
> serversidan. Domänmodellen är sedan uppdelad i ett centrallager och ett servicelager. Centrallagret består av
> ett repository som hanterar all objekt härrörande från Entity Framework. Servicelagret sköter sedan kopplingen
> mellan serversidan och klientsidan samt cachningen av informationen från webbservicetjänsterna.

> När det gäller cachningen så försöker servicelagret först hämta plats och prognos från databasen. Om plats och
> tillhörande prognos saknas i databasen hämtas den från webbservicen Geonames och yr.no som sedan sparas i databasen. 
> Ett datetime-objekt håller reda på när nästa uppdatering ska ske. Uppdatering är satt till var 7e timme för att matcha 
> uppdateringen i webbservicen yr.no så att inte anrop görs i onödan.

> Valideringen sker med hjälp av data annotation där eventuella felmeddelanden automatiskt läggs till i egenskapen 
> ModelState, en associativ array, där lagring av eventuella fel i modellen sker. Denna information presenteras sedan 
> med hjälp av Html.ValidationSummary på klientsidan. 

## Klientsida

> Det andra projektet av webbapplikationen, Weather.MVC, hanterar all kod på klientsidan. Projektet är uppdelad
> enligt designmönstret Model-View-Controller. Dock är koden som normalt återfinns i katalogen Models placerat
> i domänmodellen. Controllermetoderna använder sig inte direkt av några objekt hörrörande från Entity Framework 
> utan tar hjälp av centrallagret och servicelagret i domänmodellen med detta.

> Cachning på klientsidan sköts av en manifestfil där en lista på alla dokument som ska cachas finns specificerade. 
> Denna fil finns placerad i HTML-elementet i Layout.cshtml.

> Valideringen sker med hjälp av data annotation ihop med jQuery och jQuery Validation. Valideringsmeddelanden visas 
> och döljs dynamiskt och själva formuläret postas inte förrän fälten klarar valideringen på klienten. 

## Säkerhet och prestandaoptimering

> Gällande säkerheten av applikationen har jag använt mig av ASP.NET Identity för att autentisera en användare.
> En användare kan registrera sig för att få ett lokalt konto och användarinformationen sparas sedan lokalt i 
> applikationens databas med hjälp av Entity Framework Code First. Det finns även möjlighet att logga in via 
> en tredjepartstjänst där oAuth är implemeneterad. I detta fall har jag valt Google för denna åtgärd, dock 
> är inte HTTPS protokollet genom SSL aktiverad vilket det borde vara om appliktionen ska köras i skarpt läge. 
> Detta för atT kryptera känslig information som skickas över nätverket. Har även skyddat applikationen från CSRF 
> och XSS med hjälp av ramverkets anti-forgery tokenkontroll och AntiXSS Library. 

> När det gäller optimeringen så innehåller den senaste .NET-versionen, .NET 4.5, en hel del prestandaoptimeringar
> såsom en förbättrad Garbage Collector som kan hantera stora mängder av data vilket frigör minnet i applikationen
> mer effektivt. En annan optimering i ramverket är användandet av s k bundles för script -och CSS-filer, detta gör 
> att antalet anrop minskas när de ligger samlande i en bundle istället. 

## Offline-first

> När det gäller offline-first har jag implementerat application cache där manifestfilen hanterar vilka dokument som ska
> cachas. På detta sätt kan en användare surfa runt på besökta sidor även i offlineläge. 

## Reflektion kring projektet

> Mitt största problem som jag stötte på i projektet var att få yr.no och geonames att integrera med varandra. Detta gjorde
> att jag hamnade efter i tidsplaneringen men lyckades ändå få klart kraven för ASP.NET MVC-kursen inom avtalad tid. På grund
> av detta hann jag inte få klart kraven för denna kurs i tid och släpade då efter med projektet. I samband med detta började
> givetvis två nya kurser och det gjorde att jag inte helt kunde lägga 100% på att färdigställa de krav som jag hade i min
> planering. Hade gärna fortsatt att implementera localstorage för att få applikationen mer dynamisk vid offlineläge men
> detta hann jag dessvärre inte med. Denna del vill jag självklart implementera men det får bli vid ett senare tillfälle när
> jag får mer tid över. Jag skulle även vilja se över lite kod i min applikation då det finns bättre sätt att skriva de på. 

## Risker med applikationen

> Den enda risk jag kan identifiera är just att känslig information som skickas över nätverket vid inloggning inte är 
> krypterad i dagsläget. Detta bör åtgärdas med hjälp av HTTPS protokollet genom att införskaffa ett SSL certifikat. 
