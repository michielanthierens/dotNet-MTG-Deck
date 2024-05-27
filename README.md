# database
## database for the cards
in order for the application to run, make sure that the connection between the database and the project is correctly set up. You can download the database with the .pak file.

for this project, i used azure data studio (and mongoDB).
If you want to connect your database to the project, use this command:
replace the server, port, name of the database, your username and your password. The ouput should be ok if you imported my project.

`Scaffold-DbContext “Server=localhost,1433;Database=NameOfDatabase;User Id=sa;Password=YourSecretPassword;TrustServerCertificate=true” Microsoft.EntityFrameworkCore.SqlServer -outputdir Models -Project DataAccessLib`

## database for the deck

this was done in mongoDB. Just make sure the ConnectionString in Howest.MagicCards.MinimalAPI.appsettings.json is set to your mongoDB url.
