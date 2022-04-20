/**
 * Plugin for working with SignalR
 */

/**
 * SinglaR hubs
 */
export const HUBS = {
  notification: "../hubs/notification",
  /*process.env.NODE_ENV === "production"
      ? "http://10.33.0.203:8089/hubs/notification"
      : "../hubs/notification",*/
};

const signalR = require("@microsoft/signalr");

export default {
  /**
   * checks if SignalR has been disabled in local storage
   * @returns true if signalR has been disabled, false otherwise
   */
  isDisabled: () => {
    var disableSignalR = false;
    if (localStorage.disableSignalR)
      disableSignalR = localStorage.disableSignalR == "true";
    return disableSignalR;
  },

  /**
   * Builds a new SignalR hub connection
   * @returns new SignalR hub connection
   */
  build: () => {
    return new signalR.HubConnectionBuilder()
      .withUrl(HUBS.notification)
      .configureLogging(signalR.LogLevel.Error)
      .build();
  },
};
