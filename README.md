# Conference system for Zamecke Navrsi, p.o.

## Demo

Demo is available on web page: https://konferencnisystem.azurewebsites.net/

### Credentials to demo

Username  | Password
--------- | --------------------
admin     | KonfSysAdmin123
poradatel | KonfSysPoradatel123
uzivatel  | KonfSysUzivatel123

## Deploy to your own server

1. Open solution (_ConferenceSystem.sln_) in Visual Studio
2. Set connection strings to database in files:
  1. _web.config_ in project _ConferencySystem.DAL_
  2. _web.config_ in project _ConferencySystemApp_
3. If tables in database already exist, it is necessary to comment database initialization in source code.
Comment line _Database.SetInitializer(new DbInitializer());_ in the file _ConferencySystem.DAL/Data/DbContext.cs_.
4. Build and run application (keyboard shorcut: _Ctrl_ + _F5_)
5. Application is ready. Default accounts were created.