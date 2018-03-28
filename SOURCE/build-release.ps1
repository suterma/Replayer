# -----------------------------------------------------
# Builds and signs a release of Replayer.exe
# Run from the Developer Command Prompt for VS 2017
# -----------------------------------------------------

#go to project
#cd \GitHub\Replayer

# creates a release build, ready for deployment
msbuild \SOURCE\Replayer.Application.sln /t:Build /p:Configuration=Release /p:TargetFramework=v4.0

# Copy the binary over to release
xcopy .\SOURCE\Replayer.WinForms.Ui\bin\Release\Replayer.exe .\RELEASE\ /V /F /R /Y 

# signing
signtool sign /a \SOURCE\Release\Replayer.exe
