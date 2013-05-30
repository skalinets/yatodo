properties {
  $srcDir = "..\src"
  $solution = $srcDir + "\yatodo.sln"
  $buildconfiguration = "Debug"
}

task default -depends run-acceptance-tests 

task build {
  exec { & msbuild $solution /v:m /m  /p:configuration=$buildConfiguration }
}

task run-acceptance-tests -depends build {
    $testDlls = ls "$srcDir\*\bin\$buildConfiguration" -rec `
    | where { $_.Name.EndsWith(".Tests.dll") } `
    | where { (Test-Path ([System.IO.Path]::GetDirectoryName($_.FullName) + "\Machine.Specifications.dll")) -eq $True } `
    | foreach { $_.FullName }

    $mspecExePath = Join-Path $srcDir "packages\Machine.Specifications.0.5.12\tools\mspec-clr4.exe"
    exec { & $mspecExePath $testDlls }
}