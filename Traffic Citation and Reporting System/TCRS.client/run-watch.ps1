#To use install:
#1. run in powershell
#Set-ExecutionPolicy RemoteSigned
#2. install nodejs from website
#3. run in powershell
#npm install -g browser-sync
#4. install web compiler in visual studio extensions up at the top
#5. restore nuget packages by right-clicking main solution
#6. rebuild solution from main solution
#7. close VS and reopen

# now the script does something
# this script just outputs this

# Start-Job -Name DotNetWatch -ScriptBlock{dotnet watch run};
Start-Job  -Name BrowserSync -ScriptBlock{browser-sync start `
            --proxy http://localhost:5000/ `
            --files '**/*.cshtml, **/*.css, **/*.scss, **/*.js, **/*.htm*' `
            }

dotnet watch run