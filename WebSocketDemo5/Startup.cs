﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebSocketDemo5
{
    public class Startup
    {
        async Task ReceiveAsync(ConnectionManager cm, ILogger log, WebSocket socket, string socketId,
            Func<ConnectionManager, string, Task> responseHandlerAsync)
        {
            var bufferSize = new byte[4];
            var receiveBuffer = new ArraySegment<byte>(bufferSize);
            WebSocketReceiveResult result;

            while (socket.State == WebSocketState.Open)
            {
                using (var ms = new MemoryStream())
                {
                    do
                    {
                        result = await socket.ReceiveAsync(receiveBuffer, CancellationToken.None);
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            log.LogDebug($"Socket Id {socketId} : Receive closing message.");
                            var removalStatus = cm.RemoveSocket(socketId);
                            log.LogDebug($"Socket Id {socket} removal status {removalStatus}");
                            break;
                        }

                        if (result.MessageType != WebSocketMessageType.Text)
                            throw new Exception("Unexpected Message");

                        ms.Write(receiveBuffer.Array, receiveBuffer.Offset, result.Count);

                    } while (!result.EndOfMessage && !result.CloseStatus.HasValue);


                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        ms.Seek(0, SeekOrigin.Begin);

                        string clientRequest = string.Empty;
                        using (var reader = new StreamReader(ms, Encoding.UTF8))
                        {
                            clientRequest = reader.ReadToEnd();
                        }

                        log.LogDebug($"Socket Id {socketId}: Receive: {clientRequest}");

                        await responseHandlerAsync(cm, clientRequest);
                    }

                    if (result.CloseStatus.HasValue)
                        break;
                }
            }
        }

        public ArraySegment<byte> Reply(string content) => new ArraySegment<byte>(Encoding.UTF8.GetBytes(content));

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            logger.AddConsole((str, level) => !str.Contains("Microsoft.AspNetCore") && level >= LogLevel.Trace);

            var log = logger.CreateLogger("");

            app.UseWebSockets();

            var cm = new ConnectionManager();

            app.Use(async (context, next) =>
            {
                if (!context.WebSockets.IsWebSocketRequest)
                {
                    await next();
                    return;
                }

                var socket = await context.WebSockets.AcceptWebSocketAsync();
                var socketId = cm.AddSocket(socket);

                var cmdHandler = new CommandHandler();
                await ReceiveAsync(cm, log, socket, socketId, async (connectionManager, clientRequest) =>
                    {
                        var (isOK, cmd) = cmdHandler.Parse(clientRequest);

                        if (isOK)
                        {
                            switch (cmd.Type)
                            {
                                case CommandType.List:
                                {
                                    var others = connectionManager.Other(socketId)
                                        .Select(x => string.IsNullOrWhiteSpace(x.nickname) ? "NoNick" : x.nickname).ToList();
                                    if (others.Count > 0)
                                        await socket.SendAsync(Reply(string.Join(",", others)),
                                            WebSocketMessageType.Text, true, CancellationToken.None);
                                    else
                                        await socket.SendAsync(Reply("No other user on this channel"),
                                            WebSocketMessageType.Text, true, CancellationToken.None);

                                    break;
                                }

                                case CommandType.Nick:
                                {
                                    var isOk = connectionManager.SetNickName(socketId, cmd.Data.Item1);
                                    if (isOK)
                                        await socket.SendAsync(Reply($"Nickname now {cmd.Data.Item1}"), WebSocketMessageType.Text, true, CancellationToken.None);
                                    else
                                        await socket.SendAsync(Reply($"#nick fails"), WebSocketMessageType.Text, true, CancellationToken.None);

                                    break;
                                }
                                case CommandType.Send:
                                {
                                    var (isFound, sck) = connectionManager.GetByNick(cmd.Data.Item1);
                                    if (isFound)
                                    {
                                        var (isOk, sender) = connectionManager.GetNickNameById(socketId);
                                        if (isOK)
                                        {
                                            await sck.SendAsync(Reply($"From {sender}: {cmd.Data.Item2}"),
                                                WebSocketMessageType.Text, true, CancellationToken.None);
                                        }
                                        else
                                        {
                                            await sck.SendAsync(Reply($"From Unknown:{cmd.Data.Item2}"),
                                                WebSocketMessageType.Text, true, CancellationToken.None);
                                        }

                                        await socket.SendAsync(Reply($"Message sent to {cmd.Data.Item1}"),
                                            WebSocketMessageType.Text, true, CancellationToken.None);
                                    }

                                    else
                                        await socket.SendAsync(Reply($"{cmd.Data.Item1} not found"),
                                            WebSocketMessageType.Text, true, CancellationToken.None);

                                    break;
                                }
                                case CommandType.Quit:
                                {
                                    connectionManager.RemoveSocket(socketId);
                                    await socket.SendAsync(Reply("Quitting chat"), WebSocketMessageType.Text, true,
                                        CancellationToken.None);
                                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "",
                                        CancellationToken.None);
                                    break;
                                }
                                default:
                                {
                                    await socket.SendAsync(Reply("Command not understood"), WebSocketMessageType.Text,
                                        true, CancellationToken.None);

                                    break;
                                }
                            }
                        }
                        else
                        {
                            await socket.SendAsync(Reply("Command not understood"), WebSocketMessageType.Text, true,
                                CancellationToken.None);
                        }
                    });

                if (socket.State != WebSocketState.Open)
                {
                    log.LogDebug($"Socket Id {socketId} with status {socket.State}");
                }

            });

            app.Run(async context =>
            {
                context.Response.Headers.Add("content-type", "text/html");
                await context.Response.WriteAsync(@"
<html>                
    <head>
        <script src=""https://code.jquery.com/jquery-3.2.1.min.js"" integrity=""sha256-hwg4gsxgFZhOsEEamdOYGBf13FyQuiTwlAQgxVSNgt4="" crossorigin=""anonymous""></script>
    </head>
    <body>
        <h1>Web Socket (please open this page at 2 tabs at least)</h1>
        <p>Commands<p>
        <ul>
            <li>#list</li> 
            <li>#nick <i>nickname</i></li>
            <li>#talk <i>nickname</i> <i>text</i></li>
            <li>#quit</li>
        </ul>
        <input type=""text"" length=""50"" id=""msg"" value=""#nick anne""/> 
        <button type=""button"" id=""send"">Send</button>
        <button type=""button"" id=""close"">Close</button>
        <br/>
        <ul id=""responses""></ul>
        <script>
            $(function(){
                var url = ""ws://localhost:5000"";
                var socket = new WebSocket(url);
                var send = $(""#send"");
                var close = $(""#close"");
                var msg = $(""#msg"");
                var responses = $(""#responses"");
                socket.onopen = function(e){
                    responses.append(`<li>Socket opened</li>`);
                    send.click(function(){
                        if (socket.readyState !== WebSocket.OPEN){
                            alert('Socket is closed. Cannot send message.');
                            return;
                        }
                        socket.send(msg.val()); 
                    });
                };
                close.click(function(){
                    if (socket.readyState !== WebSocket.OPEN){
                        alert('You cannot close this connection because it is already closed');
                        return;
                    }
                    socket.close();    
                });
                socket.onmessage = function(e){
                    var response = e.data;
                    responses.append(`<li>${e.data.trim()}</li>`);
                };
                socket.onclose = function(e){
                    responses.append(`<li>Socket closed</li>`);
                };
            });
        </script>
    </body>
</html>");
            });
        }
    }
}
