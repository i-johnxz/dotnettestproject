using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;

namespace WebSocketDemo5
{
    public class ConnectionManager
    {
        ConcurrentDictionary<string, (WebSocket socket, string nickName)> _sockets = new ConcurrentDictionary<string, (WebSocket socket, string nickName)>();

        public string AddSocket(WebSocket socket)
        {
            var id = Guid.NewGuid().ToString();

            if(!_sockets.TryAdd(id, (socket, string.Empty)))
                throw new Exception($"Problem in adding socket with Id {id}");

            return id;
        }

        public bool SetNickName(string id, string nickname)
        {
            if (_sockets.TryGetValue(id, out var x))
            {
                _sockets[id] = (x.socket, nickname);
                return true;
            }

            return false;
        }

        public (bool, string) GetNickNameById(string id)
        {
            if (_sockets.TryGetValue(id, out var x))
                return (true, x.nickName);
            else
                return (false, null);
        }

        public (bool, WebSocket socket) GetByNick(string nickname)
        {
            var found = _sockets
                .Where(x => x.Value.nickName.Equals(nickname, StringComparison.CurrentCultureIgnoreCase)).Take(1)
                .ToList();

            if (found.Count == 0)
                return (false, null);
            else
                return (true, found[0].Value.socket);
        }

        public bool RemoveSocket(string id) => _sockets.TryRemove(id, out (WebSocket, string) _);

        public List<(WebSocket socket, string id, string nickname)> Other(string id) => _sockets.Where(x => x.Key != id)
            .Select(x => (x.Value.socket, x.Key, x.Value.nickName)).ToList();


    }


}
