"c:\program files (x86)\ncover\NCover.Console.exe" "c:\program files (x86)\nunit 2.5.3\bin\net-2.0\nunit-console-x86.exe" %1 /xml=nunit-result.xml /config=test.config /labels //x Coverage.xml //h coverage.html //l Coverage.log //a %2 //ea Habanero.Base.CoverageExcludeAttribute
"c:\program files (x86)\ncoverexplorer\ncoverexplorer.console" coverage.xml /html /r:4 /p:Habanero.Core