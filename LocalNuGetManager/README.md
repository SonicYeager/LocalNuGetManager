# Get Started

https://loic-sharma.github.io/BaGet/installation/docker/

## Setup the NuGet Server
```docker run -d --name nuget-server -p 5555:80 --env-file baget.env -v "$(pwd)/baget-data:/var/baget" loicsharma/baget:latest```

## Add Server to NuGetFeed and add Key
```nuget setapikey NUGET-SERVER-API-KEY -Source local-nuget```