using IrcDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;

namespace TechBot {
static class IRC {
  public static TwitchIrcClient client;

  public static List<TechBot.Objects.Channel> ChannelList =
      new List<TechBot.Objects.Channel>();

  public static TechBot.Objects.Channel FindChannel(string ChannelName) {
    foreach (TechBot.Objects.Channel Channel in ChannelList) {
      if (Channel.Name == ChannelName) {
        return Channel;
      }
    }
    return null;
  }

  public static void Init(string[] args) {
    if (args.Length < 2) {
      Log.Logger.OutputToConsole("Usage: twitchirc <username> <oauth>");
      Log.Logger.OutputToConsole(
          "Use http://twitchapps.com/tmi/ to generate an <oauth> token!");
      return;
    }

    var server = "irc.twitch.tv";
    var username = args[0];
    var password = args[1];
    Log.Logger.OutputToConsole("Starting to connect to twitch as {0}.",
                               username);

    using(client = new TwitchIrcClient()) {
      client.FloodPreventer = new IrcStandardFloodPreventer(4, 2000);
      client.Disconnected += IrcClient_Disconnected;
      client.Registered += IrcClient_Registered;
      // Wait until connection has succeeded or timed out.
      using(var registeredEvent = new ManualResetEventSlim(false)) {
        using(var connectedEvent = new ManualResetEventSlim(false)) {
          client.Connected += (sender2, e2) => connectedEvent.Set();
          client.Registered += (sender2, e2) => registeredEvent.Set();
          client.Connect(server, false,
                         new IrcUserRegistrationInfo(){
                             NickName = username.ToLower(), Password = password,
                             UserName = username});
          if (!connectedEvent.Wait(10000)) {
            Log.Logger.OutputToConsole("Connection to '{0}' timed out.",
                                       server);
            return;
          }
        }
        Console.Out.WriteLine("Now connected to '{0}'.", server);
        if (!registeredEvent.Wait(10000)) {
          Log.Logger.OutputToConsole("Could not register to '{0}'.", server);
          return;
        }
      }

      Console.Out.WriteLine("Now registered to '{0}' as '{1}'.", server,
                            username);
      client.SendRawMessage(
          "CAP REQ :twitch.tv/membership twitch.tv/commands twitch.tv/tags");
      HandleEventLoop(client);
    }
  }

  private static void HandleEventLoop(IrcClient client) {
    bool isExit = false;
    while (!isExit) {
      Console.Write("> ");
      var command = Console.ReadLine();
      switch (command) {
      case "exit":
        isExit = true;
        break;
      case "join":
        if (!string.IsNullOrEmpty(command))
          client.SendRawMessage("JOIN #" + command);
        break;
      default:
        if (!string.IsNullOrEmpty(command)) {
          if (command.StartsWith("/") && command.Length > 1) {
            client.SendRawMessage(command.Substring(1));
          } else {
            Log.Logger.OutputToConsole("unknown command '{0}'", command);
          }
        }
        break;
      }
    }
    client.Disconnect();
  }

  private static void IrcClient_Registered(object sender, EventArgs e) {
    var client = (IrcClient) sender;

    client.LocalUser.JoinedChannel += IrcClient_LocalUser_JoinedChannel;
    client.LocalUser.LeftChannel += IrcClient_LocalUser_LeftChannel;
    client.RawMessageReceived += IrcClient_Process;
  }

  private static void IrcClient_LocalUser_LeftChannel(object sender,
                                                      IrcChannelEventArgs e) {
    var localUser = (IrcLocalUser) sender;

    e.Channel.UserJoined -= IrcClient_Channel_UserJoined;
    e.Channel.UserLeft -= IrcClient_Channel_UserLeft;

    TechBot.Objects.Channel Channel = FindChannel(e.Channel.Name.Substring(1));
    if (ChannelList.Contains(Channel))
      ChannelList.Remove(Channel);

    Log.Logger.OutputToConsole("You left the channel {0}.", e.Channel.Name);
  }

  private static void IrcClient_LocalUser_JoinedChannel(object sender,
                                                        IrcChannelEventArgs e) {
    var localUser = (IrcLocalUser) sender;

    e.Channel.UserJoined += IrcClient_Channel_UserJoined;
    e.Channel.UserLeft += IrcClient_Channel_UserLeft;

    TechBot.Objects.Channel newChannel = new TechBot.Objects.Channel(e.Channel);
    ChannelList.Add(newChannel);

    Log.Logger.OutputToConsole("You joined the channel {0}.", e.Channel.Name);
  }

  private static void IrcClient_Channel_NoticeReceived(object sender,
                                                       IrcMessageEventArgs e) {
    var channel = (IrcChannel) sender;

    Log.Logger.OutputToConsole("[{0}] Notice: {1}.", channel.Name, e.Text);
  }

  private static void IrcClient_Channel_UserLeft(object sender,
                                                 IrcChannelUserEventArgs e) {
    var channel = (IrcChannel) sender;

    TechBot.Objects.Channel Channel =
        FindChannel(e.ChannelUser.Channel.Name.Substring(1));
    TechBot.Objects.User tempUser =
        Channel.FindUser(e.ChannelUser.User.NickName);

    Channel.UserLeft(tempUser);

    Log.Logger.OutputToConsole("[{0}] User {1} left the channel.", channel.Name,
                               e.ChannelUser.User.NickName);
  }

  private static void IrcClient_Channel_UserJoined(object sender,
                                                   IrcChannelUserEventArgs e) {
    var channel = (IrcChannel) sender;

    Log.Logger.OutputToConsole("[{0}] User {1} joined the channel.",
                               channel.Name, e.ChannelUser.User.NickName);
  }

  private static void IrcClient_Process(object sender,
                                        IrcRawMessageEventArgs e) {
    ThreadPool.QueueUserWorkItem(
        new WaitCallback(delegate(object state) {
          try {
            string[] strlist = e.RawContent.Split(" ");
            if (strlist[2] == "PRIVMSG") {
              string[] modes = strlist [0]
                                   .Split(";");
              int i = 0;
              bool isMod = false;
              string username = "";
              foreach (string r in modes) {
                if (r.Contains("mod=")) {
                  string IsMod = r.Split("=")[1];
                  // IsMod = IsMod.Remove(IsMod.Length - 1);
                  if (IsMod == "1")
                    isMod = true;
                } else if (r.Contains("display-name=")) {
                  username = r.Split("=")[1];
                }
                i++;
              }

              // Channel      = String    channelName
              // Username     = String    username
              // Message      = String    message
              // Is User mod? = Boolean   isMod

              string channelName = strlist [3]
                                       .Substring(1);
              string message = strlist [4]
                                   .Substring(1);

              if (username.ToLower() == channelName.ToLower())
                isMod = true;

              Log.Logger.OutputToConsole("[{0}] {1}: {2}", strlist[3], username,
                                         message);

              TechBot.Objects.Channel Channel = FindChannel(channelName);
              TechBot.Objects.User tempUser = Channel.FindUser(username);

              if (tempUser == null) {
                tempUser = new TechBot.Objects.User(username, channelName);
              }

              Channel.UserJoined(tempUser);

              Channel.ChatMessageReceived(tempUser, isMod, message);
            } else {
              // Log.Logger.OutputToConsole(e.RawContent);
            }
          } catch {
          }
        }),
        null);
  }

  private static void IrcClient_Disconnected(object sender, EventArgs e) {
    var client = (IrcClient) sender;
  }

  private static void IrcClient_Connected(object sender, EventArgs e) {
    var client = (IrcClient) sender;
  }
}
}
