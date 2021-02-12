#To use install:
#1. run in powershell
#Set-ExecutionPolicy RemoteSigned
#2. install nodejs from website
#3. run in powershell
#npm install -g browser-sync

param($Work)

# restart PowerShell with -noexit, the same script, and 1
if (!$Work) {
    powershell -noexit -file $MyInvocation.MyCommand.Path 1
    return
}

# now the script does something
# this script just outputs this

dotnet run watch ; browser-sync start `
            --proxy http://localhost:5000/ `
            --files '**/*.cshtml, **/*.css, **/*.js, **/*.htm*'
             