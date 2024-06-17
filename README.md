![Deploy](https://github.com/Scrawach/SnakeMultiplayer/actions/workflows/deploy.yaml/badge.svg)

## Snake Multiplayer

A multiplayer snake-inspired arcade game on Unity.

## Stack

- [Reflex](https://github.com/gustavopsantos/Reflex): minimal dependency injection framework for Unity.
- [Colyseus](https://github.com/colyseus/colyseus): multiplayer framework for Node.js.
- Unity UI Toolkit: screen space and world space UI.
- A little bit [UniTask](https://github.com/Cysharp/UniTask).
- Github Actions: [deploy](/.github/workflows/deploy.yaml) server as docker container on every push in main.

## Gameplay
[SnakeMultiplayer.webm](https://github.com/Scrawach/SnakeMultiplayer/assets/40476180/3d72fa94-c793-498b-9116-b63cb46e1543)

## Deploy

### Docker

> [!WARNING]
> Required [docker](https://docker-docs.uclv.cu/engine/install/) and [docker-compose](https://docker-docs.uclv.cu/compose/install/).

1. Go to the server folder:

```
cd server
```

2. Start container:

```
docker-compose up
```

That's all. Now you have a container running on your machine with a server for the game on port `2567`. If this port is busy, there will be problems, so you can change it in the corresponding [docker compose file](https://github.com/Scrawach/SnakeMultiplayer/blob/main/server/docker-compose.yml).

### NPM

> [!WARNING]
> Required [NPM](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm).

1. Go to server folder with source code:

```
cd server/SnakeMultiplayerServer
```

2. Install dependecies:

```
npm install
```

3. Start server:

```
npm start
```

That's all. Now you can start the game client and connect to the game.
