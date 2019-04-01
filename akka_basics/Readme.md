# Akka_Basics

## /slidedeck
Slides from brown bag.

__Run__
```bash
yarn start
```

__Build__
```bash
yarn build
```

## /code
Example code for brown bag.

__Build__
```bash
dotnet build
```

__Run__
```bash
dotnet run
```

### Challenges:
_Solutions available __soon__ in /challenges/1, /challenges/2, etc_
1. Can you make the crawl non-blocking?
2. Can you implement a crawl manager with a pool of workers?
3. Can you make the crawl recurse through internal links?
4. After 3, can you make the recursive crawl report frequently to SignalR so the front-end doesn't look broken?
5. Can you make the crawl back off if it starts getting 404 or other erorrs?
