/*
public enum Status
{
    Valid = 0,
    NotSupported =1,
    NoPermission = 2,
    Unavailable = 3,
    TimeOut = 4,
    UnknownError = 5
}

*/

window.getUserLocation = (obj, id) => {
  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(
      (pos) => {
        sendReply(obj, id, 0, pos.coords.latitude, pos.coords.longitude);
      },
      (error) => {
        var code = 0;
        switch (error.code) {
          case error.PERMISSION_DENIED:
            code = 2;
            break;
          case error.POSITION_UNAVAILABLE:
            code = 3;
            break;
          case error.TIMEOUT:
            code = 4;
            break;
          case error.UNKNOWN_ERROR:
            code = 5;
            break;
        }
        sendReply(obj, id, code, 0, 0);
      }
    );
  }
  else {
    sendReply(obj, id, 1, 0, 0);
  }
}

function sendReply(obj, id, code, lat, long) {
  obj.invokeMethodAsync("Reply", id, code, lat, long);
}