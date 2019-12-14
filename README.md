# m3u-parser

m3u-parser is free package to parse data from m3u files in .net

## Getting started

### Install:

`Install-Package m3u-parser`

### Parse data:

```C#
M3uPlaylist M3uParser.GetFromStream(Stream stream)
```

### Create stream from object:

```C#
Stream M3uParser.CreateStream(M3uPlaylist playlistData)
```
