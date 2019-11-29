pushd ..\src\Houston.Web.App

dotnet publish --self-contained -r win-x86 -o \\wohnzimmer-box\nas\Netzwerk\Deployments\Houston /p:PublishSingleFile=true

popd

