dotnet publish -c Release 

docker build -t grouper-8team-api "src\\Grouper.Api.Web\bin\\Release\net5.0\\publish"
docker tag grouper-8team-api registry.heroku.com/grouper-8team-api/web
docker push registry.heroku.com/grouper-8team-api/web
heroku container:release web --app grouper-8team-api