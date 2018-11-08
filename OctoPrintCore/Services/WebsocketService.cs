using MvvmCross.Plugin.Messenger;
using Newtonsoft.Json;
using OctoPrintCore.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OctoPrintCore.Services
{
    public class WebsocketService : IWebsocketService
    {
        private readonly IMvxMessenger _messenger;

        private static WebsocketService _instance;

        public WebsocketService(IMvxMessenger messenger)
        {
            _instance = this;
            _messenger = messenger;
            client = new ClientWebSocket();
            cts = new CancellationTokenSource();
        }

        public async Task Connect()
        {

            try
            {
                await client.ConnectAsync(new Uri("ws://" + UserProperties.GetLocalIP() + "/sockjs/websocket"), cts.Token);
            } catch(Exception e)
            {
                _instance._messenger.Publish(new OctoMessage(_instance, OctoMessage.DisconnectedFromOctoprint));
            }

            UpdateClientState();

            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    WebSocketReceiveResult result;
                    var message = new ArraySegment<byte>(new byte[4096]);
                    var serialisedMessae = "";
                    do
                    {
                        result = await client.ReceiveAsync(message, cts.Token);
                        var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                        serialisedMessae += Encoding.UTF8.GetString(messageBytes);

                        if (result.EndOfMessage)
                        {
                            try
                            {
                                var msg = JsonConvert.DeserializeObject<WebSocketMessage>(serialisedMessae);
                                Console.WriteLine(msg);
                                if (msg != null)
                                {
                                    if (msg.connected != null)
                                    {
                                        _instance._messenger.Publish(new ConnectMessage(this, msg.connected));
                                    }
                                    if (msg.current != null)
                                    {
                                        _instance._messenger.Publish(new CurrentMessage(this, msg.current));
                                    }
                                    if (msg.events != null)
                                    {
                                        _instance._messenger.Publish(new EventMessage(this, msg.events));
                                    }
                                    if (msg.slicingProgress != null)
                                    {
                                        _instance._messenger.Publish(new SlicingProgressMessage(this, msg.slicingProgress));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Invalide message format. {ex.Message}");
                            }

                            serialisedMessae = "";
                        }

                    } while (result.MessageType != WebSocketMessageType.Close);
                    _instance._messenger.Publish(new OctoMessage(_instance, OctoMessage.DisconnectedFromOctoprint));
                }
            }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            void UpdateClientState()
            {
                if (client.State == WebSocketState.Open)
                {
                    _instance._messenger.Publish(new OctoMessage(_instance, OctoMessage.ConnectedToOctoprint));
                }
                else
                {
                    _instance._messenger.Publish(new OctoMessage(_instance, OctoMessage.DisconnectedFromOctoprint));
                }
            }
        }

        readonly ClientWebSocket client;
        readonly CancellationTokenSource cts;
    }
}
