# Önálló labor

Témakiírás:
https://www.aut.bme.hu/Task/20-21-osz/Docker-kontenerizalt-ASP

Fejlesztőkörnyezet:
Manjaro Linux (Arch)
Visual Studo Code
Docker
Docker Compose

## Mi az a Docker
A Docker egy olyan virtualizációs technológia, amely segítségével egyszerűen lehet deployolni alkalmazásokat. Az egyes szolgáltatások konténerben futtatható docker image-ek formájában egyszerűen telepíthető és futtatható számos platformon a keretrendszer segítségével. Előnye, hogy minden függőség jól definiáltan enkapszulálható azokban az image fájlokban, amelyek az adott alkalmazást, vagy annak egy szolgáltatását futtatják.

## Mi az a Docker Compose
Ennek segítségével több konténert is definiálhatunk az alkalmazásunkban.

## Előkészítés

- Visual Studio Code
- Remote Development extension
- Remote Containers extension

https://code.visualstudio.com/docs/remote/containers


https://wiki.archlinux.org/index.php/docker

A Docker service szolgáltatásainak használata felhasználóként (root jogosultság nélkül) lényegében root jogosultságot ad a felhasználó kezébe. Ezt elkerülendő érdemes elkerülni a jogosultság adást, vagy egyéb megoldást keresni rá (pl. podman, docker rootless mode):
https://docs.docker.com/engine/security/security/#docker-daemon-attack-surface

Docker rootless mode (Docker v19.03+ funkcionalitás):
https://docs.docker.com/engine/security/rootless/

Konténerizált példa .Net Core projekt:
https://github.com/Microsoft/vscode-remote-try-dotnetcore

```shell
$ # install vscode
$ git clone https://aur.archlinux.org/visual-studio-code-bin.git
$ cd visual-studio-code-bin
$ makepkg -si
$ cd ..
```

```shell
$ # install docker & docker-compose
# pacman -Syu
# pacman -S docker
# pacman -S docker-compose
```

```shell
$ # [test]
# systemctl start docker
$ docker --version
$ # ASSERTION: version is 18.06+
$ docker-compose --version
$ # ASSERTION: version is 1.21+
# docker run -it --rm archlinux bash -c "echo hello world"
$ # ASSERTION: echoes "hello world"
# systemctl stop docker
```

```shell
$ # install docker rootless script
$ git clone https://aur.archlinux.org/docker-rootless-bin.git
$ cd docker-rootless-bin
$ makepkg -si
$ cd ..
$ # additional settings, see: 
$ # https://docs.docker.com/engine/security/rootless/
# echo "kernel.unprivileged_userns_clone=1" > /etc/sysctl.d/99-docker-rootless.conf
# sysctl --system
$ # testuser is host machine's username
$ echo "testuser:231072:65536" > /etc/subuid
$ echo "testuser:231072:65536" > /etc/subgid
$ echo "export DOCKER_HOST=unix://$XDG_RUNTIME_DIR/docker.sock" > ~/.bashrc
$ . ~/.bashrc
$ systemctl --user enable docker
$ systemctl --user start docker
$ # auto-start after booting
# loginctl enable-linger $USER
$ systemctl --user status docker
$ # ASSERTION: docker is loaded, enabled and active(running)
```

VSCode workspace-ben:
```shell
$ git clone https://github.com/microsoft/vscode-remote-try-dotnetcore.git
$ echo $DOCKER_HOST  | xclip -sel clip
$ code vscode-remote-try-dotnetcore/.vscode/launch.json
$ # EDIT: add "DOCKER_HOST": <value of your clipboard> to env dictionary
```

- VS Code-ban:
1. > F1 
1. > Remote-Containers: Open Folder in Container...
1. > select <workspace>/vscode-remote-try-dotnetcore
1. > open new terminal
1. > in terminal: `chown -R vscode /workspaces/vscode-remote-try-dotnetcore`
1. > F5 on Program.cs
1. > Open http://localhost:5000 in your browser
1. > `ASSERTION: echoing message`

## Hasznos parancsok

```shell
# run a single command then remove container
docker run -it --rm archlinux bash [-c <some_command>]
# open running container's shell
docker exec -it <container> bash
# list containers
docker ps [-a]
# pull image
docker pull <image>
# handle running container
docker stop|kill|rm <container>
# delete image
docker rmi <image>
# purge stuff
docker system prune [-a]
```

## Github remote szerver elérése VSCode-ban

lásd:
https://github.com/MicrosoftDocs/live-share/issues/224

## Continuous Integration

Github Actions CI workflow template script hozzáadása a projekthez:
https://docs.github.com/en/actions/guides/setting-up-continuous-integration-using-workflow-templates

Github Actions script készítése repositoryhoz:
https://docs.github.com/en/actions/quickstart
