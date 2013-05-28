properties {
  $src = "..\src"
  $solution = $src + "\yatodo.sln"
}

task default -depends build 

task build {
  msbuild $solution /v:m /m
}