# Get Started

```docker run --detach=true --publish 443:443 --env NUGET_API_KEY=my-secret --volume /srv/docker/nuget/database:/var/www/db --volume /srv/docker/nuget/packages:/var/www/packagefiles --name nuget-server sunside/simple-nuget-server```
```nuget setapikey NUGET-SERVER-API-KEY -Source local-nuget```