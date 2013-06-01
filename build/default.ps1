properties {
  $srcDir = "..\src"
  $solution = $srcDir + "\yatodo.sln"
  $buildconfiguration = "Debug"
  
}

function Get-TestDlls (){
  return ls "$srcDir\*\bin\$buildConfiguration" -rec `
    | where { $_.Name.EndsWith(".Tests.dll") } `
    | foreach { $_.FullName }
}

task default -depends run-unit-tests 

task build -depends clean {
  exec { & msbuild $solution /v:m /m  /p:configuration=$buildConfiguration }
}

task clean {
  exec { & msbuild $solution /v:m /m  /p:configuration=$buildConfiguration /p:tagret=Clean }
}


task run-unit-tests -depends build {
  $xunitRunner = Join-Path $srcDir "packages\xunit.runners.1.9.1\tools\xunit.console.clr4.exe"
  $testDlls = Get-TestDlls
  exec { & $xunitRunner $testDlls }
}

task run-acceptance-tests -depends run-acceptance-tests-in-progress, run-acceptance-tests-regression

task run-acceptance-tests-in-progress -depends build {
    $mspecExePath = Join-Path $srcDir "packages\Machine.Specifications.0.5.12\tools\mspec-clr4.exe"
    $testDlls = Get-TestDlls
    exec { & $mspecExePath $testDlls -i "InProgress" }
}

task run-acceptance-tests-regression -depends build {
    $mspecExePath = Join-Path $srcDir "packages\Machine.Specifications.0.5.12\tools\mspec-clr4.exe"
    $testDlls = Get-TestDlls
    exec { & $mspecExePath $testDlls -x "InProgress"}
}