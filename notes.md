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

## Dockeren futó webszerver elérése HTTPS-en keresztül

https://devblogs.microsoft.com/aspnet/configuring-https-in-asp-net-core-across-different-platforms/

## MongoDB adatbázos használata

* VSCode-hoz elérhető MongoDB extension a könnyebb kezelhetőség érdekében.
* Alternatíva lehet a shell felület vagy a Robo 3T felület használata.

https://code.visualstudio.com/docs/azure/mongodb

Docker image dokumentáció:
https://hub.docker.com/_/mongo

mongo shell parancsok:
https://www.tutorialspoint.com/mongodb/index.htm

mongo config paraméterek:
https://docs.mongodb.com/manual/reference/configuration-options/
https://docs.mongodb.com/manual/reference/program/mongod/#bin.mongod

### X.509 certificate authentikáció
* Azonos CA-val aláírt kliens- és szerveroldali certificate
* Kliens oldali certificate tartalmazza a következő mezőket:
    >keyUsage = digitalSignature
    extendedKeyUsage = clientAuth
* Egyedi kliens-certek
* Kliens oldali cert CN-je vagy az egyik SAN-ja  
---TODO---

### Mirror/failover szerver eállítása

https://docs.mongodb.com/manual/replication/

https://docs.mongodb.com/manual/tutorial/convert-standalone-to-replica-set/

https://docs.mongodb.com/manual/tutorial/expand-replica-set/

https://docs.mongodb.com/manual/tutorial/deploy-replica-set/

---TODO---

### Konfiguráció

```yaml
# wiredTigerCacheSizeGB
storage:
   wiredTiger:
      engineConfig:
         cacheSizeGB: 1.5
```

```shell
$ # create mongodb server
$ docker pull mongo
$ docker rm mongo_onlab; docker run --name mongo_onlab -v /home/testuser/project/db/:/data/db -v /home//testuser/project/mongo:/etc/mongo -it -p -v  27017:27017 mongo --config /etc/mongo/mongod.conf

```

# TODO:
## MongoDB
* Client X.509 auth
* Replica
* Replica X.509 member auth
* GridFS

## Razor Pages

https://docs.microsoft.com/en-us/aspnet/core/razor-pages
https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/razor-pages-start
https://www.learnrazorpages.com/razor-pages/tutorial/bakery
https://www.learnrazorpages.com/razor-pages/model-binding
https://weblog.west-wind.com/posts/2019/May/18/Live-Reloading-Server-Side-ASPNET-Core-Apps
https://www.learnrazorpages.com/razor-pages/files/layout
https://docs.microsoft.com/en-us/aspnet/web-pages/overview/ui-layouts-and-themes/3-creating-a-consistent-look
https://www.learnrazorpages.com/razor-pages/files/layout

A Razor Pages egy keretrendszer és egy tervezési modell, amely webes felületek egyszerű készítését teszi lehetővé.
Alapértelmezetten a Pages könyvtárban találhatóak a HTML template-ek (.cshtml kiterjesztéssel), illetve a hozzájuk tartozó modellek (.cs kiterjesztéssel). A HTML template-ek HTML kódot, a modell elemeinek bindolását, illetve programozott logikát tartalmazhat.
Az oldalak a @page direktívával kezdődnek.
Ismerniük kell az oldalaknak a hozzájuk tartozó PageModell megvalósítást, illetve bindolni kell  a modellt a @model direktívával.
A programozott részek, paraméterek {} blokkokba kerülnek.
A modell elemeit a @Model elemen keresztül érhetjük el.
Alapértelmezetten / és /index a nyitóoldalunkra, az Index-re irányít át.
A routing névegyezés alapján történik. Lehetőségünk van többszintű routingra is. Ehhez csak annyit kell tennünk, hogy új könyvtárat hozunk létre, majd abban helyezzük el a kívánt oldal fájljait.
Definiálhatunk úgynevezett layoutokat is, amelyek önmagukban nem generálható oldalaknak felelnek meg. Ezek a content page-eket kiegészítő, újrahasznosítható elemek, mint például egy HTML váz, header, footer, scriptek stb. Magasfokú rugalmasságot tesz lehetővé, mivel a content page-eknél válogathatunk ezek közül, igény esetén akár felül is írhatjuk a layoutok bizonyos részeit (pl. section) 1-1 content page esetén.
A section szolgálja a layoutok testreszabhatóságát content page oldalról.
Tehát nem csak a content page tud különböző layoutokat használni, hanem maguk a layoutok is testreszabatóak, specializálhatóak a content page-ekben definiált section blokkok segítségével.
A projektben speciális fájlok a _Layout.cshtml (az oldalak alapvető HTML váza), a _ViewStart.cshtml (az adott könyvtárban vagy a gyermekeiben definiált content page-ek lehívásakor hajtódik végre a fájl tartalma), illetve a _ViewImports.cshtl (ez tartalmazza a page-ek által közösen elérhető névtereket, segédkönyvtárakat).

https://docs.microsoft.com/en-us/aspnet/core/security/authorization/secure-data?view=aspnetcore-3.1
https://docs.microsoft.com/en-us/visualstudio/modeling/structure-your-modeling-solution?view=vs-2019

Middleware

Routing policy-k

MVVM:
* Model: Ez reprezentálja a dinamikusan betötött adatot, az adatmodellt kapcsoljuk hozzá egy forráshoz (pl. mongodb), és csatornázzuk be a HTML kódgenerálásba
* View: .cshtml fájl: megjelenítés, HTML template, data binding a viewmodellen keresztül
* ViewModel: controller + 

https://stackify.com/asp-net-razor-pages-vs-mvc/

https://www.mikesdotnetting.com/article/324/areas-in-razor-pages

https://www.learnrazorpages.com/razor-pages/routing

Routingom:
GET         Anyone         /, /Index
GET, POST   Login          /Users/Profile/Edit
GET         Anyone         /Users/Profile?userid=<user_id>
GET, POST   Anyone         /Users/Login
GET         Login          /Users/Logout
GET, POST   Login, Admin   /Users/Invite
GET, POST   Login, Admin   /Users/Ban
GET, POST   Anyone         /Users/Register?token=<register_token>
GET         Anyone         /About
--/Error
GET, POST   Login          /Media/Upload
GET         Anyone         /Media/Albums
GET         Anyone         /Media/Albums/View?albumid=<album_id>
GET         Anyone         /Media/Videos
GET, POST   Login, Admin   /Media/Moderate

Hookok:
Admin policy:
   adminPrivilegeRequired() ->
      redirect to /Index

Login policy:
   loginRequired(timeout=60, navigate_back_url=origin) -> 
      login check
      redirect to /Users/Login
      Error message kitöltése az oldalon sárgával
      tryLogin(navigate_to_url=origin)
      timeout -> nem vár tovább, nem irányít vissza

/Users/Login POST:
   tryLogin(navigate_to_url=/Index) ->
      authentikáció  ->
         wait 5 sec
         siker: átirányít navigate_to_url-re
         különben: Error message kitöltése az oldalon pirossal

/Users/Invite POST:
   trySendInvite(retry=2) ->
      generate token
      send email ->
         siker: Error message kitöltése az oldalon zölddel
               DB-be lementeni a tokent + lejárat
         különben: Error message kitöltése az oldalon pirossal

/Users/Profile/Edit POST:
   editProfile() ->
      validation ->
         siker: redirect to /Users/Profile?userid=<id>
         különben: Error message kitöltése az oldalon pirossal


/Users/Ban POST:
   banEditor() ->
      get from DB ->
         delete from DB ->
            siker: Error message az oldalon zölddel
            különben: Error piros, exception, alert rendszergazda
         különben: Error piros


/Users/Register POST:
   register() ->
      token check ->
         validáció ->
            DB-be írás ->
               redirect to /Users/Login
      különben: Error message piros

/Media/Upload POST:
   upload(videoOrPhotos, fájlok, késleltetett) ->
      form validation
      datatype validation
      db feltöltés
      logolás
      késleltetett -> 
         Error message sárgán
         task elindítása: resource-hoz x időn keresztül más nem fér hozzá, moderate oldalhoz hozzáfér
      különben: redirect /Media/Albums/View?albumid=<album_id> vagy /Media/Videos

/Media/Moderate POST:
   moderate() ->
      db törlések ha van
      Error message zöld féjlok listájával
      logolás

/Media/Albums/View?albumid=<album_id> GET:
   checkAlbumId ->
      rossz: redirect to /Media/Albums

/Users/Profile?userid=<user_id> GET:
   checkUserId ->
      rossz: redirect to /About
