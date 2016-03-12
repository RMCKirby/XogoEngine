#r @"packages/build/FAKE/tools/FakeLib.dll"

open Fake
open Fake.OpenCoverHelper
open Fake.ReportGeneratorHelper

let buildDir     = "./build/"
let testDir      = "./test/"
let reportDir    = "./reports/"
let historyDir   = "./history/"
let coverageDir  = reportDir + "coverage/"
let coverageFile = coverageDir + "code-coverage.xml"

let xogoGlProject     = "src/XogoEngine.OpenGL/XogoEngine.OpenGL.csproj"
let xogoGlTestProject = "src/XogoEngine.OpenGL.Test/XogoEngine.OpenGL.Test.csproj"
let xogoProject       = "src/XogoEngine/XogoEngine.csproj"
let xogoTestProject   = "src/XogoEngine.Test/XogoEngine.Test.csproj"

let coreProjects = [xogoGlProject; xogoProject]
let testProjects = [xogoGlTestProject; xogoTestProject]

Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir; reportDir; coverageDir]
)

Target "CreateDirs" (fun _ ->
    CreateDir reportDir
    CreateDir coverageDir
)

Target "Build_Core_Assemblies" (fun _ ->
    MSBuildDebug buildDir "Build" coreProjects
        |> Log "Build Core Assemblies Ouput:"
)

Target "Build_Test_Assemblies" (fun _ ->
    MSBuildDebug testDir "Build" testProjects
        |> Log "Build Test Assemblies Output:"
)

Target "UnitTest" (fun _ ->
    !! (testDir + "XogoEngine.*Test.dll")
        |> NUnit (fun p ->
            {p with
                DisableShadowCopy = true
                ShowLabels = false
            }
        )
)

Target "UnitTest_WithCoverage" (fun _ ->
    "/noshadow " + testDir + "XogoEngine.OpenGL.Test.dll " + testDir + "XogoEngine.Test.dll"
        |> OpenCover (fun p ->
            {p with
                ExePath = "packages/tools/OpenCover/tools/OpenCover.Console.exe"
                TestRunnerExePath = "packages/tools/NUnit.Runners/tools/nunit-console.exe"
                Output = coverageFile
                Filter = "+[XogoEngine*]* -[*.Test*]*"
                Register = RegisterUser
            }
        )
)

Target "GenerateCoverageReport" (fun _ ->
    [coverageFile]
        |> ReportGenerator (fun p ->
            {p with
                ExePath = "packages/tools/ReportGenerator/tools/ReportGenerator.exe"
                TargetDir = reportDir
                HistoryDir = historyDir
                ReportTypes =
                [
                    ReportGeneratorReportType.Html;
                    ReportGeneratorReportType.Latex;
                    ReportGeneratorReportType.Badges;
                ]
            }
        )
)

Target "Default" DoNothing

"Clean"
    ==> "CreateDirs"
    ==> "Build_Core_Assemblies"
    ==> "Build_Test_Assemblies"
    =?> ("UnitTest", isMono)
    =?> ("UnitTest_WithCoverage", not isMono)
    =?> ("GenerateCoverageReport", not isMono)
    ==> "Default"

RunTargetOrDefault "Default"
