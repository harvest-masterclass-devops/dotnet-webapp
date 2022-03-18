Basic approach on running some code in a container

## Steps

```
docker build . -t simple-container
docker run --name simple-instance -p 9006:80 simple-container testje
```

For some reason I can't exit the docker process with `ctrl+c` in my windows terminal, I'm killing the process from the GUI.

## Building really locally

You will need the dotnet stuff to really build locally.

* `dotnet --version` should return at least `6.0.201`
  * You can install by `choco install dotnet-sdk` (or [download](https://dotnet.microsoft.com/en-us/download) it)
  * `choco list --localonly` to show your locally installed stuff
* Make sure you switch your docker to 'linux containers' mode (in the context menu).

# Resources

* https://dev.to/mcklmt/build-and-deploy-net-5-app-with-github-actions-1de
* https://dev.to/berviantoleo/web-api-in-net-6-docker-41d5
