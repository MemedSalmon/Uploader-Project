Innan du kör programmet måste göra en setup.

1: 	Gå in på appsettings.json och länka din egna SQL server.

	Vid "Data Source=" skriv in namnet på din SQL server.


2: 	Skapa en map på datorn (Server) som innehåller 3 sub folders (Image, Text, Video). 

	Vid Server i appsettings skriv in din path till foldern. 


3: 	I visual studio Packe Manager Console. (Sök "Package Manager" i Visual Studio)

	Skriv "Add-Migration" + Valfritt namn. Vänta tills den kört klart.

	Skriv "Update-Database". Vänta tills den kört klart.


4:	Starta programmet.
